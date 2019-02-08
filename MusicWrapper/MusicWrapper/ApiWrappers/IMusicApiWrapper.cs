using System.Threading.Tasks;

namespace MusicWrapper.Wrappers
{
	interface IMusicApiWrapper
  {
    Task<ApiResponse> SearchArtists(string name);

    Task<ApiResponse> GetArtistInfo(string id);

    Task<ApiResponse> GetArtistAlbums(string id);

    Task<ApiResponse> GetAlbumInfo(string id);

    //ApiResponse SearchSongs(string name);

    Task<ApiResponse> GetSongInfo(string id);

    Task<ApiResponse> GetArtistEvents(string artistId);
  }
}
