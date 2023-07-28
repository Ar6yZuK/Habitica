using Habitica.Response;

namespace Habitica;

public class NotSuccessException : Exception
{
	public NotSuccess.Root NotSuccessObject { get; }

	public NotSuccessException(NotSuccess.Root notSuccessObject)
	{
		NotSuccessObject = notSuccessObject;
	}
}