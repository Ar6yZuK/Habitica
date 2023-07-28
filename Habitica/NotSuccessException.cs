using Ar6yZuK.Habitica.Response;

namespace Ar6yZuK.Habitica;

public class NotSuccessException : Exception
{
	public NotSuccess.Root NotSuccessObject { get; }

	public NotSuccessException(NotSuccess.Root notSuccessObject)
	{
		NotSuccessObject = notSuccessObject;
	}
}