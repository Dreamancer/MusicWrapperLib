using MusicWrapper.Enums;
using Newtonsoft.Json.Linq;

namespace MusicWrapper
{
	public class ApiResponse
	{
		public ApiName ApiName { get; set; }
		public ResponseType ResponseType { get; set; }
		public JObject Response { get; set; }
		public string Message { get; set; }
	}
}
