using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicWrapper.ApiWrappers
{
    interface IUserLibraryWrapper
    {
		Task<ApiResponse> GetLibraryArtists(string userName, int? limit = null, int? page = null);
	}
}
