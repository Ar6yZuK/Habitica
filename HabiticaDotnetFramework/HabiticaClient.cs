using Ar6yZuK.Habitica.Response;
using Ar6yZuK.Habitica.Response.Tasks;
using Ar6yZuK.Habitica.Response.User;
using OneOf;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#pragma warning disable 1591
#nullable enable

namespace Ar6yZuK.Habitica;

public class HabiticaClient : IDisposable
{
	public const string BaseAddressString = "https://habitica.com/api/v3/";
	public readonly Uri BaseAddress = new(BaseAddressString);

	public const string AuthorId = "e3e6945e-a32f-4d6f-a833-45f11a678d23";
	public const string ApiName = "Ar6yZuK_hu";
	public static readonly (string Name, string Value) xClientHeader = ("x-client", $"{AuthorId} - {ApiName}");

	private readonly HttpClient _httpClient;

	public HabiticaClient(ICredentials credentials)
	{
		if (credentials is null) throw new ArgumentNullException(nameof(credentials));

		_httpClient = new HttpClient();
		_httpClient.BaseAddress = BaseAddress;
		_httpClient.DefaultRequestHeaders.Add("x-api-key", credentials.ApiKey);
		_httpClient.DefaultRequestHeaders.Add("x-api-user", credentials.UserId);

		_httpClient.DefaultRequestHeaders.Add(xClientHeader.Name, xClientHeader.Value);
	}

	#region Request
	/// <exception cref="HttpRequestException"></exception>
	private Task<OneOf<TData?, NotSuccess.Root>> UserRequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken = default)
		=> RequestAsync<TData>($"user/{additionForResource}", method, cancellationToken);

	/// <exception cref="HttpRequestException"></exception>
	private Task<OneOf<TData?, NotSuccess.Root>> TasksRequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken = default)
		=> RequestAsync<TData>($"tasks/{additionForResource}", method, cancellationToken);

	/// <exception cref="HttpRequestException"></exception>
	private async Task<OneOf<TData?, NotSuccess.Root>> RequestAsync<TData>(string additionForResource, HttpMethod method, CancellationToken cancellationToken)
	{
		var request = new HttpRequestMessage(method, additionForResource);
		var responseMessage = await _httpClient.SendAsync(request, cancellationToken: cancellationToken);

		var data = await EnsureSuccess<TData>(responseMessage);
		return data;
	}
	#endregion

	#region EnsureSuccess
	private static async Task<OneOf<TData?, NotSuccess.Root>> EnsureSuccess<TData>(HttpResponseMessage responseMessage)
	{
		string json = await responseMessage.Content.ReadAsStringAsync();
		var notSuccessJson = JsonSerializer.Deserialize<NotSuccess.Root>(json);

		if(notSuccessJson is null)
			responseMessage.EnsureSuccessStatusCode();

		if (!notSuccessJson.Success)
			return notSuccessJson;

		return JsonSerializer.Deserialize<TData>(json);
	}
	#endregion

	#region BuyHealthPotion
	/// <summary>
	/// Use <see cref="BuyHealthPotion(CancellationToken)"/> once per 30 seconds
	/// </summary>
	/// <exception cref="HttpRequestException"></exception>
	public async Task<OneOf<BuyHealthPotion.Root?, NotSuccess.Root>> BuyHealthPotion(CancellationToken cancellationToken = default)
	{
		var result = await UserRequestAsync<BuyHealthPotion.Root>("buy-health-potion", HttpMethod.Post, cancellationToken);
		return result;
	}
	#endregion

	#region GetAllTasks
	/// <summary>
	/// Use <see cref="GetAllTasksAsync(CancellationToken)"/> once per few seconds
	/// </summary>
	/// <exception cref="HttpRequestException"></exception>
	public async Task<OneOf<GetAllTasks.Root?, NotSuccess.Root>> GetAllTasksAsync(CancellationToken cancellationToken = default)
	{
		var response = await TasksRequestAsync<GetAllTasks.Root>("user", HttpMethod.Get, cancellationToken);

		return response;
	}
	#endregion

	#region Score
	/// <summary>
	/// Use <see cref="ScoreUp(Guid, CancellationToken)"/> once per 30 seconds
	/// </summary>
	/// <exception cref="HttpRequestException"></exception>
	public Task<OneOf<TaskScore.Root?, NotSuccess.Root>> ScoreUp(Guid taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreUp(taskIdOrAlias.ToString(), cancellationToken);

	/// <summary>
	/// Use <see cref="ScoreUp(string, CancellationToken)"/> once per 30 seconds
	/// </summary>
	/// <exception cref="HttpRequestException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public Task<OneOf<TaskScore.Root?, NotSuccess.Root>> ScoreUp(string taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreInternal(taskIdOrAlias, Score.up, cancellationToken);

	/// <summary>
	/// Use <see cref="ScoreDown(string, CancellationToken)"/> once per 30 seconds
	/// </summary>
	/// <exception cref="HttpRequestException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public Task<OneOf<TaskScore.Root?, NotSuccess.Root>> ScoreDown(string taskIdOrAlias, CancellationToken cancellationToken = default)
		=> ScoreInternal(taskIdOrAlias, Score.down, cancellationToken);

	private async Task<OneOf<TaskScore.Root?, NotSuccess.Root>> ScoreInternal(string taskIdOrAlias, Score score, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(taskIdOrAlias)) 
			throw new ArgumentException($"\"{nameof(taskIdOrAlias)}\" не может быть пустым или содержать только пробел.", nameof(taskIdOrAlias));

		var requestAddition = $"{taskIdOrAlias}/score/{score}";
		var response = await TasksRequestAsync<TaskScore.Root>(requestAddition, HttpMethod.Post, cancellationToken);

		return response;
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