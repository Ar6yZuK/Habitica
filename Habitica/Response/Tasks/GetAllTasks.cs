using System.Text.Json;
using System.Text.Json.Serialization;
#pragma warning disable CS8618

namespace Ar6yZuK.Habitica.Response.Tasks;

public class GetAllTasks
{
	public class Challenge
	{
	}

	public class Checklist
	{
		[JsonPropertyName("completed")]
		public bool? Completed { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("id")]
		public Guid Id { get; set; }
	}

	public class CompletedBy
	{
	}

	public class TaskData
	{
		public Task<TaskScore.Root> ScoreUp(HabiticaClient client, CancellationToken cancellationToken = default)
			=> client.ScoreUp(Id, cancellationToken);

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("challenge")]
		public Challenge Challenge { get; set; }

		[JsonPropertyName("group")]
		public Group Group { get; set; }

		[JsonPropertyName("up")]
		public bool? Up { get; set; }

		[JsonPropertyName("down")]
		public bool? Down { get; set; }

		[JsonPropertyName("counterUp")]
		public long? CounterUp { get; set; }

		[JsonPropertyName("counterDown")]
		public long? CounterDown { get; set; }

		[JsonPropertyName("frequency")]
		public string Frequency { get; set; }

		[JsonPropertyName("history")]
		public List<History> History { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("notes")]
		public string Notes { get; set; }

		[JsonPropertyName("tags")]
		public List<string> Tags { get; set; }

		[JsonPropertyName("value")]
		public double? Value { get; set; }

		[JsonPropertyName("priority")]
		public double? Priority { get; set; }

		[JsonPropertyName("attribute")]
		public string Attribute { get; set; }

		[JsonPropertyName("byHabitica")]
		public bool? ByHabitica { get; set; }

		[JsonPropertyName("reminders")]
		public List<object> Reminders { get; set; }

		[System.Text.Json.Serialization.JsonConverter(typeof(DateTimeOffsetToLocalJsonConverter))]
		[JsonPropertyName("createdAt")]
		public DateTimeOffset? CreatedAt { get; set; }

		[System.Text.Json.Serialization.JsonConverter(typeof(DateTimeOffsetToLocalJsonConverter))]
		[JsonPropertyName("updatedAt")]
		public DateTimeOffset? UpdatedAt { get; set; }

		[JsonPropertyName("_id")]
		public Guid _Id { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("userId")]
		public string UserId { get; set; }

		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("repeat")]
		public Repeat Repeat { get; set; }

		[JsonPropertyName("everyX")]
		public long? EveryX { get; set; }

		[JsonPropertyName("streak")]
		public long? Streak { get; set; }

		[JsonPropertyName("nextDue")]
		public List<string> NextDue { get; set; }

		[JsonPropertyName("yesterDaily")]
		public bool? YesterDaily { get; set; }

		[JsonPropertyName("completed")]
		public bool? Completed { get; set; }

		[JsonPropertyName("collapseChecklist")]
		public bool? CollapseChecklist { get; set; }

		[JsonPropertyName("startDate")]
		public DateTimeOffset? StartDate { get; set; }

		[JsonPropertyName("daysOfMonth")]
		public List<object> DaysOfMonth { get; set; }

		[JsonPropertyName("weeksOfMonth")]
		public List<object> WeeksOfMonth { get; set; }

		[JsonPropertyName("checklist")]
		public List<Checklist> Checklist { get; set; }

		[JsonPropertyName("isDue")]
		public bool? IsDue { get; set; }
	}

	public class Group
	{
		[JsonPropertyName("completedBy")]
		public CompletedBy CompletedBy { get; set; }

		[JsonPropertyName("assignedUsers")]
		public List<object> AssignedUsers { get; set; }
	}

	public class History
	{
		public class DateTimeOffsetUnixMillisecondConverter : System.Text.Json.Serialization.JsonConverter<DateTimeOffset>
		{
			public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var read = reader.GetInt64();
				var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(read).ToLocalTime();
				return dateTimeOffset;
			}

			public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
			{
				writer.WriteStringValue(value);
			}
		}

		[System.Text.Json.Serialization.JsonConverter(typeof(DateTimeOffsetUnixMillisecondConverter))]
		[JsonPropertyName("date")]
		public DateTimeOffset Date { get; set; }

		[JsonPropertyName("value")]
		public double? Value { get; set; }

		[JsonPropertyName("scoredUp")]
		public long? ScoredUp { get; set; }

		[JsonPropertyName("scoredDown")]
		public long? ScoredDown { get; set; }

		[JsonPropertyName("isDue")]
		public bool? IsDue { get; set; }

		[JsonPropertyName("completed")]
		public bool? Completed { get; set; }
	}

	public class Notification
	{
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("data")]
		public TaskData Data { get; set; }

		[JsonPropertyName("seen")]
		public bool? Seen { get; set; }

		[JsonPropertyName("id")]
		public Guid Id { get; set; }
	}

	public class Repeat
	{
		[JsonPropertyName("m")]
		public bool? Monday { get; set; }

		[JsonPropertyName("t")]
		public bool? Tuesday { get; set; }

		[JsonPropertyName("w")]
		public bool? Wednesday { get; set; }

		[JsonPropertyName("th")]
		public bool? Thursday { get; set; }

		[JsonPropertyName("f")]
		public bool? Friday { get; set; }

		[JsonPropertyName("s")]
		public bool? Saturday { get; set; }

		[JsonPropertyName("su")]
		public bool? Sunday { get; set; }
	}

	public class Root
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("data")]
		public List<TaskData> Data { get; set; }

		[JsonPropertyName("notifications")]
		public List<Notification> Notifications { get; set; }

		[JsonPropertyName("appVersion")]
		public string AppVersion { get; set; }
	}

	public class DateTimeOffsetToLocalJsonConverter : System.Text.Json.Serialization.JsonConverter<DateTimeOffset>
	{
		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dateTime = reader.GetDateTime();

			var dateTimeOffset = new DateTimeOffset(dateTime).ToLocalTime();
			return dateTimeOffset;
		}

		public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}
	}
}