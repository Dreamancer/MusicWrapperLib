using MusicWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicWrapper.ApiWrappers
{
    interface IUserLibraryWrapper
    {
		Task<ApiResponse> GetUserLibraryArtists(string user, int? limit = null, int? page = null);

		Task<ApiResponse> GetUserArtistTracks(string user, string artist, DateTime? startDate = null, DateTime? endDate = null, int? page = null);

		Task<ApiResponse> GetUserLovedTracks(string user, int? limit = null, int? page = null);

		Task<ApiResponse> GetUserTopArtists(string user, TopPeriod? period, int? limit, int? page);
	}
}
