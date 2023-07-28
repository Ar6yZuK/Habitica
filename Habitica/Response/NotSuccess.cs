using System.Text.Json.Serialization;
#pragma warning disable CS8618

namespace Ar6yZuK.Habitica.Response;
public class NotSuccess
{
	public class Root
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("error")]
		public string Error { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
