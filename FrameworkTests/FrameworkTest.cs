using Ar6yZuK.Habitica;
using Ar6yZuK.Habitica.Response.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkTests
{
	[TestClass]
	public class FrameworkTest
	{
		private const string ScoreUpTestTemplate = $"{HabiticaClient.ApiName}.{nameof(HabiticaClient.ScoreUp)}";

		[TestMethod]
		public async Task GetAllTasksAsync()
		{
			using HabiticaClient client = GetClient();

			GetAllTasks.Root allTasks = await client.GetAllTasksAsync();

			Assert.IsNotNull(allTasks);
			Assert.IsTrue(allTasks.Success);
		}

		// Can be test
		[TestMethod]
		public async Task ScoreUp()
		{
			using HabiticaClient client = GetClient();

			GetAllTasks.Root allTasks = await client.GetAllTasksAsync();
			GetAllTasks.TaskData testTask = GetTestTask(allTasks);

			TaskScore.Root scoreUpResult = await client.ScoreUp(testTask.Id);

			Assert.IsNotNull(testTask, $"Please create task with name starts with:{ScoreUpTestTemplate}");
			Assert.IsTrue(scoreUpResult.Success);
			static GetAllTasks.TaskData GetTestTask(GetAllTasks.Root allTasks)
				=> allTasks.Data.First(x => x.Text.TrimStart().StartsWith(ScoreUpTestTemplate));
		}

		private static (string? UserAPIKey, string? UserId) GetSecrets()
		{
			const string UserAPIKey = "UserAPIKey";
			const string UserId = "UserID";

			var credentialsProvider = new ConfigurationBuilder()
				.AddUserSecrets<FrameworkTest>()
				.Build();

			string userAPIKey = credentialsProvider.GetSection(UserAPIKey).Value;
			string userID = credentialsProvider.GetSection(UserId).Value;

			Assert.IsFalse(string.IsNullOrWhiteSpace(userAPIKey), $"{nameof(UserAPIKey)} was null or empty");
			Assert.IsFalse(string.IsNullOrWhiteSpace(userID), $"{nameof(UserId)} was null or empty");

			return (userAPIKey, userID);
		}
		private HabiticaClient GetClient()
		{
			var (userAPIKey, userId) = GetSecrets();
			var client = new HabiticaClient(new Credentials(userId!, userAPIKey!));
			return client;
		}
	}
}
