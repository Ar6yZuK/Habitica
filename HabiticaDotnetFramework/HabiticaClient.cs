using Ar6yZuK.Habitica.Response;
using Ar6yZuK.Habitica.Response.Tasks;
using Ar6yZuK.Habitica.Response.User;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ar6yZuK.Habitica;

public class HabiticaClient : IDisposable
{
	public const string BaseAddressString = "https://habitica.com/api/v3/";
	public readonly Uri _baseAddress = new(BaseAddressString);

	public const string AuthorId = "e3e6945e-a32f-4d6f-a833-45f11a678d23";
	public const string ApiName = "Ar6yZuK_hu";
	public static readonly (string Name, string Value) xClientHeader = ("x-client", $"{AuthorId} - {ApiName}");

	private readonly HttpClient _httpClient;

	public HabiticaClient(Credentials credentials)
	{
		if (credentials is null) throw new ArgumentNullException(nameof(credentials));

		_httpClient = new HttpClient();
		_httpClient.BaseAddress = _baseAddress;
		_httpClient.DefaultRequestHeaders.Add("x-api-key", credentials.ApiKey);
		_httpClient.DefaultRequestHeaders.Add("x-api-user", credentials.UserId);

		_httpClient.DefaultRequestHeaders.Add(xClientHeader.Name, xClientHeader.Value);
	}

	#region Request
	/// <exception cref="NotSuccessException"></exception>
	/// <exception cref="HttpRequestException"></exception>
	private Task<TData?> UserRequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken = default)
		=> RequestAsync<TData>($"user/{additionForResource}", method, cancellationToken);

	/// <exception cref="NotSuccessException"></exception>
	/// <exception cref="HttpRequestException"></exception>
	private Task<TData?> TasksRequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken = default)
		=> RequestAsync<TData>($"tasks/{additionForResource}", method, cancellationToken);

	/// <summary>
	/// return null if TData not deserialized
	/// </summary>
	/// <exception cref="NotSuccessException"></exception>
	/// <exception cref="HttpRequestException"></exception>
	private async Task<TData?> RequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken)
	{
		var request = new HttpRequestMessage(method, additionForResource);
		var responseMessage = await _httpClient.SendAsync(request, cancellationToken: cancellationToken);

		TData? data = await EnsureSuccess<TData>(responseMessage);
		return data;
	}
	#endregion

	#region EnsureSuccess
	private static async Task<TData?> EnsureSuccess<TData>(HttpResponseMessage responseMessage)
	{
		responseMessage.EnsureSuccessStatusCode();
		string json = await responseMessage.Content.ReadAsStringAsync();
		var successJson = JsonSerializer.Deserialize<NotSuccess.Root>(json); // may TData : ISuccess. Deserialize TData and check success. For not deserialize twice
																			 // if successJson is null maybe habitica error.
		if (!successJson.Success)
			throw new NotSuccessException(successJson);

		return JsonSerializer.Deserialize<TData>(json);
	}
	#endregion

	#region BuyHealthPotion
	/// <exception cref="NotSuccessException"></exception>
	/// <exception cref="HttpRequestException"></exception>
	public async Task<BuyHealthPotion.Root> BuyHealthPotion(CancellationToken cancellationToken = default)
	{
		var result = await UserRequestAsync<BuyHealthPotion.Root>("buy-health-potion", HttpMethod.Post, cancellationToken);
		return result!;
	}
	#endregion

	#region GetAllTasks
	/// <exception cref="NotSuccessException"></exception>
	/// <exception cref="HttpRequestException"></exception>
	public async Task<GetAllTasks.Root> GetAllTasksAsync(CancellationToken cancellationToken = default)
	{
		var response = await TasksRequestAsync<GetAllTasks.Root>("user", HttpMethod.Get, cancellationToken);

		return response!;
	}
	#endregion

	#region Score
	public Task<TaskScore.Root> ScoreUp(Guid taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreUp(taskIdOrAlias.ToString(), cancellationToken);
	public Task<TaskScore.Root> ScoreUp(string taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreInternal(taskIdOrAlias, Score.up, cancellationToken);

	public Task<TaskScore.Root> ScoreDown(string taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreInternal(taskIdOrAlias, Score.down, cancellationToken);

	private async Task<TaskScore.Root> ScoreInternal(string taskIdOrAlias, Score score, CancellationToken cancellationToken)
	{
		var requestAddition = $"{taskIdOrAlias}/score/{score}";
		var response = await TasksRequestAsync<TaskScore.Root>(requestAddition, HttpMethod.Post, cancellationToken);

		return response!;
	}

	private enum Score
	{
		up,
		down
	}
	#endregion

	public void Dispose()
	{
		_httpClient.Dispose();
	}
}