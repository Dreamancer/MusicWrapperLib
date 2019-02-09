using Microsoft.Extensions.Configuration;
using MusicWrapper.Enums;
using System;
using System.Threading.Tasks;

namespace MusicWrapper.ApiWrappers
{
	public class LastFmWrapper : MusicApiWrapper, IUserLibraryWrapper
	{
		public override ApiName ApiName { get { return ApiName.LastFM; } }

		public LastFmWrapper(IConfiguration configuration) : base(configuration)
		{

		}

		public LastFmWrapper(string appId, string appSecret, string url = null) : base(url ?? "http://ws.audioscrobbler.com/2.0/", appId, appSecret)
		{

		}

		public override async Task<ApiResponse> GetAlbumInfo(string id)
		{
			return await GetResponse($"{_url}?method=album.getinfo&mbid={id}&api_key={_appId}&format=json", ResponseType.Album);
		}

		public override async Task<ApiResponse> GetArtistAlbums(string id)
		{
			return await GetResponse($"{_url}?method=artist.gettopalbums&mbid={id}&api_key={_appId}&format=json", ResponseType.Album);
		}

		public override Task<ApiResponse> GetArtistEvents(string artistId)
		{
			throw new NotImplementedException();
		}

		public override async Task<ApiResponse> GetArtistInfo(string id)
		{
			return await GetResponse($"{_url}?method=artist.getinfo&mbid={id}&api_key={_appId}&format=json", ResponseType.Artist);
		}

		public override async Task<ApiResponse> GetSongInfo(string id)
		{
			return await GetResponse($"{_url}?method=track.getinfo&mbid={id}&api_key={_appId}&format=json", ResponseType.Song);
		}

		public override async Task<ApiResponse> SearchArtists(string name)
		{
			return await GetResponse($"{_url}?method=artist.search&artist={name}&api_key={_appId}&format=json", ResponseType.Artist);
		}

		public async Task<ApiResponse> GetUserLibraryArtists(string userName, int? limit = null, int? page = null)
		{
			return await GetPaginatedResponse($"?method=library.getartists&api_key={_appSecret}&user={userName}&format=json", ResponseType.Song, limit, page);
		}

		public async Task<ApiResponse> GetUserArtistTracks(string userName, string artist, DateTime? startDate = null, DateTime? endDate = null, int? page = null)
		{
			string request = $"?method=user.getartisttracks&user={userName}&artist={artist}&api_key={_appSecret}&format=json";
			if (startDate.HasValue)
			{
				request += "&startTimestamp=" + ((DateTimeOffset)startDate).ToUnixTimeSeconds();
			}
			if (endDate.HasValue)
			{
				request += "&endTimestamp=" + ((DateTimeOffset)endDate).ToUnixTimeSeconds();
			}

			return await GetPaginatedResponse(request, ResponseType.Song, null, page);
		}

		public async Task<ApiResponse> GetUserLovedTracks(string user, int? limit = null, int? page = null)
		{
			return await GetPaginatedResponse($"?method=user.getlovedtracks&user={user}&api_key={_appSecret}&format=json", ResponseType.Song, limit, page);
		}

		public Task<ApiResponse> GetUserTopArtists(string user, int? period, int? limit, int? page)
		{
			throw new NotImplementedException();
		}


		#region private helpers
		private async Task<ApiResponse> GetPaginatedResponse(string request, ResponseType type, int? limit = null, int? page = null)
		{
			if (limit.HasValue)
			{
				request += "&limit=" + limit;
			}
			if (page.HasValue)
			{
				request += "&page=" + page;
			}

			return await GetResponse(request, ResponseType.Song);
		}

		private async Task<ApiResponse> GetPeriodResponse(string request, ResponseType type, int? period)
		{
			if (period.HasValue)
			{
				request += "&period=" period;
			}
		}
		#endregion
	}
}
