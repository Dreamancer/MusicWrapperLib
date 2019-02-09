using Microsoft.Extensions.Configuration;
using MusicWrapper.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicWrapper.Wrappers
{
	public class SpotifyWrapper : MusicApiWrapper
	{
		public override ApiName ApiName { get { return ApiName.Spotify; } }

		public SpotifyWrapper(IConfiguration configuration) : base(configuration)
		{

		}

		public SpotifyWrapper(string appId, string appSecret, string url = null) : base(url ?? "https://api.spotify.com/v1/", appId, appSecret)
		{

		}

		public override async Task<ApiResponse> GetAlbumInfo(string id)
		{
			return await GetResponse($"{_url}albums/{id} ", ResponseType.Album);
		}

		public override Task<ApiResponse> GetArtistEvents(string artistId)
		{
			throw new NotImplementedException();
		}

		public override async Task<ApiResponse> GetArtistInfo(string id)
		{
			return await GetResponse($"{_url}artists/{id} ", ResponseType.Artist);
		}

		public override async Task<ApiResponse> GetSongInfo(string id)
		{
			return await GetResponse($"{_url}tracks/{id} ", ResponseType.Song);
		}

		public override async Task<ApiResponse> SearchArtists(string name)
		{
			return await GetResponse($"{_url}search?q={name.Replace(' ', '+')}&type=artist", ResponseType.Artist);
		}

		public override async Task<ApiResponse> GetArtistAlbums(string id)
		{
			return await GetResponse($"{_url}artists/{id}/albums ", ResponseType.Album);
		}

		#region private helpers
		private async Task<string> GetAuthToken()
		{
			using (HttpClient client = new HttpClient())
			{
				byte[] authBytes = Encoding.UTF8.GetBytes($"{_appId}:{_appSecret}");
				string authBase64 = Convert.ToBase64String(authBytes);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authBase64);

				Dictionary<string, string> data = new Dictionary<string, string> { ["grant_type"] = "client_credentials" };
				HttpResponseMessage response;
				do
				{
					response = await client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(data));
				} while (response.StatusCode != HttpStatusCode.OK);

				string responseString = await response.Content.ReadAsStringAsync();
				return JObject.Parse(responseString)["access_token"].Value<string>();
			}
		}

		protected override async Task<ApiResponse> GetResponse(string url, ResponseType type)
		{
			string token = await GetAuthToken();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync(url);
				return await ParseResponse(response, type);
			}
		}
		#endregion
	}
}
