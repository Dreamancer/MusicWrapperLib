namespace MusicWrapper.Enums
{
	public enum ResponseType
	{
		Artist, Album, Song, Event, Error
	}

	public enum ApiName
	{
		Spotify, MusicBrainz, LastFM, UNDEFINED = -1
	}

	public enum TopPeriod
	{
		SevenDays, OneMonth, ThreeMonths, SixMonths, TwelveMonths, Overall
	}
	public static class EnumExtensions
	{
		public static string GetFriendlyString(this TopPeriod period)
		{
			switch (period)
			{
				case TopPeriod.SevenDays:
					return "7day";
				case TopPeriod.OneMonth:
					return "1month";
				case TopPeriod.ThreeMonths:
					return "3month";
				case TopPeriod.SixMonths:
					return "6month";
				case TopPeriod.TwelveMonths:
					return "12month";
				case TopPeriod.Overall:
				default:
					return "overall";
			}
		}
	}
}
