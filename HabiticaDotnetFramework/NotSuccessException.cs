using Ar6yZuK.Habitica.Response;
using System;

namespace Ar6yZuK.Habitica;

public class NotSuccessException : Exception
{
	public NotSuccess.Root NotSuccessResponse { get; }

	public NotSuccessException(NotSuccess.Root notSuccessResponse)
	{
		NotSuccessResponse = notSuccessResponse;
	}
}