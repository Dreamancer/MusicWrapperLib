using Microsoft.Extensions.Configuration;
using MusicWrapper.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MusicWrapper.Wrappers
{
	public abstract class MusicApiWrapper : IMusicApiWrapper
	{
		protected readonly string _url;
		protected readonly string _appId;
		protected readonly string _appSecret;
		public abstract ApiName ApiName { get; }

		protected MusicApiWrapper(IConfiguration configuration)
		{
			_url = configuration[$"Apis:{ApiName}:Url"];
			_appId = configuration[$"Apis:{ApiName}:ClientID"];
			_appSecret = configuration[$"Apis:{ApiName}:ClientSecret"];
		}

		protected MusicApiWrapper(string url, string appId, string appSecret)
		{
			_url = url;
			_appId = appId;
			_appSecret = appSecret;
		}

		public abstract Task<ApiResponse> GetAlbumInfo(string id);
		public abstract Task<ApiResponse> GetArtistAlbums(string id);
		public abstract Task<ApiResponse> GetArtistEvents(string artistId);
		public abstract Task<ApiResponse> GetArtistInfo(string id);
		public abstract Task<ApiResponse> GetSongInfo(string id);
		public abstract Task<ApiResponse> SearchArtists(string name);

		protected virtual async Task<ApiResponse> GetResponse(string url, ResponseType type)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync(url);
				return await ParseResponse(response, type);
			}
		}

		protected async Task<ApiResponse> ParseResponse(HttpResponseMessage response, ResponseType type)
		{
			JObject responseJson = null;
			string stringResponse = await response.Content.ReadAsStringAsync();
			try
			{
				responseJson = JObject.Parse(stringResponse);
			}
			catch (JsonReaderException ex)
			{
				//TODO log
			}
			if (response.IsSuccessStatusCode)
			{
				return new ApiResponse
				{
					ApiName = this.ApiName,
					ResponseType = responseJson == null ? ResponseType.Error : type,
					Response = responseJson,
					Message = stringResponse
				};
			}
			else
			{
				return new ApiResponse
				{
					ApiName = this.ApiName,
					ResponseType = ResponseType.Error,
					Response = responseJson,
					Message = stringResponse
				};
			}
		}
	}
}
