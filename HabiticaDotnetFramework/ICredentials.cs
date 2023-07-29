namespace Ar6yZuK.Habitica;

public class Credentials : ICredentials
{
	public string UserId { get; }
	public string ApiKey { get; }

	public Credentials(string userId, string apiKey)
	{
		ApiKey = apiKey;
		UserId = userId;
	}
}
public interface ICredentials
{
	public string UserId { get; }
	public string ApiKey { get; }
}