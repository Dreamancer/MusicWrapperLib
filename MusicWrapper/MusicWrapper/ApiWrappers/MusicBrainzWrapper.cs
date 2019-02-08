using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MusicWrapper.Enums;

namespace MusicWrapper.Wrappers
{
	public class MusicBrainzWrapper : MusicApiWrapper
	{
		public override ApiName ApiName { get { return ApiName.MusicBrainz; } }

		public MusicBrainzWrapper(IConfiguration configuration) : base(configuration)
		{

		}

		public MusicBrainzWrapper(string url = null) : base(url ?? "https://musicbrainz.org/ws/2/", null, null)
		{

		}

		public override async Task<ApiResponse> GetAlbumInfo(string id)
		{
			return await GetResponse($"{_url}release-group/{id}?inc=artists", ResponseType.Album);
		}

		public override async Task<ApiResponse> GetArtistAlbums(string id)
		{
			return await GetResponse($"{_url}release-group?artist={id}", ResponseType.Album);
		}

		public override async Task<ApiResponse> GetArtistEvents(string artistId)
		{
			return await GetResponse($"{_url}event??artist={artistId}", ResponseType.Event);
		}

		public override async Task<ApiResponse> GetArtistInfo(string id)
		{
			return await GetResponse($"{_url}artist/{id}?inc=tags", ResponseType.Artist);
		}

		public override Task<ApiResponse> GetSongInfo(string id)
		{
			throw new NotImplementedException();
		}

		public override async Task<ApiResponse> SearchArtists(string name)
		{
			return await GetResponse($"{_url}artist/?query={name}", ResponseType.Artist);
		}
	}
}
