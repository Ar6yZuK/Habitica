namespace Ar6yZuK.Habitica;
public class Credentials
{
    public string UserId { get; }

    public string ApiKey { get; }
    public Credentials(string userId, string apiKey)
    {
        ApiKey = apiKey;
        UserId = userId;
    }
}