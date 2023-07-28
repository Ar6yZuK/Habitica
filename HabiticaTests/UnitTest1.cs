using Habitica;
using Habitica.Response.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestProject;

public class Tests
{
	private const string ScoreUpTestTemplate = $"{HabiticaClient.ApiName}.{nameof(HabiticaClient.ScoreUp)}";

	[Test]
	[Order(-1)]
	public async Task GetAllTasksAsync()
	{
		using HabiticaClient client = GetClient();

		GetAllTasks.Root allTasks = await client.GetAllTasksAsync();

		Assert.That(allTasks, Is.Not.Null);
		Assert.That(allTasks.Success);
	}

	// Can be test
	//[Test]
	public async Task ScoreUp()
	{
		using HabiticaClient client = GetClient();

		GetAllTasks.Root allTasks = await client.GetAllTasksAsync();
		GetAllTasks.TaskData testTask = GetTestTask(allTasks);

		TaskScore.Root scoreUpResult = await client.ScoreUp(testTask.Id);

		Assert.Multiple(() =>
		{
			Assert.That(testTask, Is.Not.Null, $"Please create task with name starts with:{ScoreUpTestTemplate}");
			Assert.That(scoreUpResult.Success);
		});

		static GetAllTasks.TaskData GetTestTask(GetAllTasks.Root allTasks)
			=> allTasks.Data.First(x => x.Text.TrimStart().StartsWith(ScoreUpTestTemplate));
	}
	#region ClientProvider
	private static (string? UserAPIKey, string? UserId) GetSecrets()
	{
		const string UserAPIKey = "UserAPIKey";
		const string UserId = "UserID";

		var credentialsProvider = new ConfigurationBuilder()
			.AddUserSecrets<Tests>()
			.Build();

		return (credentialsProvider.GetSection(UserAPIKey).Value, credentialsProvider.GetSection(UserId).Value);
	}
	private HabiticaClient GetClient()
	{
		var (userAPIKey, userId) = GetSecrets();
		var client = new HabiticaClient(new Credentials(userId!, userAPIKey!));
		return client;
	}
	#endregion
}