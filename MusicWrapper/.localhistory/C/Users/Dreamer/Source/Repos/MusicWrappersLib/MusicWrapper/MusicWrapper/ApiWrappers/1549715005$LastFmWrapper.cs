﻿using Microsoft.Extensions.Configuration;
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
			throw new NotImplementedException();

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
			if (page.HasValue)
			{
				request += "&page=" + page;
			}

			return await GetResponse(request, ResponseType.Song);
		}

		public async Task<ApiResponse> GetUserLovedTracks(string user, int? limit = null, int? page = null)
		{
			string request = $"?method=user.getlovedtracks&user={user}&api_key={_appSecret}&format=json";
			if (limit.HasValue)
			{
				request += "&limit=" + limit;
			}
			if (page.HasValue)
			{
				request += "&page" + page;
			}
			return await GetResponse(request, ResponseType.Song);
		}
	}
}
