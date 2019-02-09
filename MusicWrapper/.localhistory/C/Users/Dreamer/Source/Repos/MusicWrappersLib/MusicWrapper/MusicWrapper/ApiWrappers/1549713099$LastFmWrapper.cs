using Microsoft.Extensions.Configuration;
using MusicWrapper.Enums;
using System;
using System.Threading.Tasks;

namespace MusicWrapper.ApiWrappers
{
	public class LastFmWrapper : MusicApiWrapper
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
	}
}
