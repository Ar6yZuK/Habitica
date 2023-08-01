using Ar6yZuK.Habitica;
using Ar6yZuK.Habitica.Response.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

			var result = await client.GetAllTasksAsync();

			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsT0);
			Assert.IsTrue(result.AsT0.Success);
		}

		// Can be test
		[TestMethod]
		public async Task ScoreUp()
		{
			using HabiticaClient client = GetClient();

			GetAllTasks.Root allTasks = (await client.GetAllTasksAsync()).AsT0;
			GetAllTasks.TaskData testTask = GetTestTask(allTasks);

			var scoreUpResult = await client.ScoreUp(testTask.Id);

			Assert.IsTrue(scoreUpResult.IsT0);
			Assert.IsNotNull(testTask, $"Please create task with name starts with:{ScoreUpTestTemplate}");
			Assert.IsTrue(scoreUpResult.AsT0.Success);
			static GetAllTasks.TaskData GetTestTask(GetAllTasks.Root allTasks)
				=> allTasks.Data.First(x => x.Text.TrimStart().StartsWith(ScoreUpTestTemplate));
		}
		[TestMethod]
		public async Task ScoreUpInvalidTaskID_Test_ShouldReturnNotSuccess()
		{
			using HabiticaClient client = GetClient();

			var result = await client.ScoreUp("test");

			Assert.IsTrue(result.IsT1);
			Assert.IsTrue(!result.AsT1.Success);
		}
		[TestMethod]
		public async Task ScoreUpInvalidTaskID_Empty_ShouldThrow()
		{
			using HabiticaClient client = GetClient();

			await Assert.ThrowsExceptionAsync<ArgumentException>(async() => await client.ScoreUp(string.Empty));
		}
		[TestMethod]
		public async Task InvalidCredentials_TwoEmpty()
		{
			using HabiticaClient client = new HabiticaClient(new Credentials("", ""));

			var result = await client.GetAllTasksAsync();

			Assert.IsTrue(result.IsT1);
			Assert.IsTrue(!result.AsT1.Success);
		}
		[TestMethod]
		public async Task InvalidCredentials_NoEmpty()
		{
			using HabiticaClient client = new HabiticaClient(new Credentials("no", "no"));

			var result = await client.GetAllTasksAsync();

			Assert.IsTrue(result.IsT1);
			Assert.IsTrue(!result.AsT1.Success);
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
