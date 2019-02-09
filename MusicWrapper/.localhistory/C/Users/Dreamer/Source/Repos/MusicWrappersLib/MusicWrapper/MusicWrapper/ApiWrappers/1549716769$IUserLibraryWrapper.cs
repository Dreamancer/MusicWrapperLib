using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicWrapper.ApiWrappers
{
    interface IUserLibraryWrapper
    {
		Task<ApiResponse> GetUserLibraryArtists(string userName, int? limit = null, int? page = null);

		Task<ApiResponse> GetUserArtistTracks(string user, string artist, DateTime? startDate = null, DateTime? endDate = null, int? page = null);

		Task<ApiResponse> GetUserLovedTracks(string user, int? limit = null, int? page = null);

		Task<ApiResponse> GetUserTopArtists(string user)
	}
}
