using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#pragma warning disable 1591

namespace Ar6yZuK.Habitica.Response.Tasks;
public class TaskScore
{
	public class Buffs
	{
		[JsonPropertyName("str")]
		public int? Str { get; set; }

		[JsonPropertyName("int")]
		public int? Int { get; set; }

		[JsonPropertyName("per")]
		public int? Per { get; set; }

		[JsonPropertyName("con")]
		public int? Con { get; set; }

		[JsonPropertyName("stealth")]
		public int? Stealth { get; set; }

		[JsonPropertyName("streaks")]
		public bool? Streaks { get; set; }

		[JsonPropertyName("snowball")]
		public bool? Snowball { get; set; }

		[JsonPropertyName("spookySparkles")]
		public bool? SpookySparkles { get; set; }

		[JsonPropertyName("shinySeed")]
		public bool? ShinySeed { get; set; }

		[JsonPropertyName("seafoam")]
		public bool? Seafoam { get; set; }
	}

	public class Data
	{
		[JsonPropertyName("delta")]
		public double? Delta { get; set; }

		[JsonPropertyName("_tmp")]
		public Tmp? Tmp { get; set; }

		[JsonPropertyName("buffs")]
		public Buffs Buffs { get; set; }

		[JsonPropertyName("training")]
		public Training Training { get; set; }

		[JsonPropertyName("hp")]
		public double? Hp { get; set; }

		[JsonPropertyName("mp")]
		public double? Mp { get; set; }

		[JsonPropertyName("exp")]
		public int? Exp { get; set; }

		[JsonPropertyName("gp")]
		public double? Gp { get; set; }

		[JsonPropertyName("lvl")]
		public int? Lvl { get; set; }

		[JsonPropertyName("class")]
		public string Class { get; set; }

		[JsonPropertyName("points")]
		public int? Points { get; set; }

		[JsonPropertyName("str")]
		public int? Str { get; set; }

		[JsonPropertyName("con")]
		public int? Con { get; set; }

		[JsonPropertyName("int")]
		public int? Int { get; set; }

		[JsonPropertyName("per")]
		public int? Per { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }
	}

	public class Drop
	{
		[JsonPropertyName("value")]
		public int? Value { get; set; }

		[JsonPropertyName("key")]
		public string Key { get; set; }

		[JsonPropertyName("premium")]
		public bool? Premium { get; set; }

		[JsonPropertyName("limited")]
		public bool? Limited { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("dialog")]
		public string Dialog { get; set; }
	}

	public class Notification
	{
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }

		[JsonPropertyName("seen")]
		public bool? Seen { get; set; }

		[JsonPropertyName("id")]
		public Guid Id { get; set; }
	}

	public class Quest
	{
		[JsonPropertyName("progressDelta")]
		public double? ProgressDelta { get; set; }
	}

	public class Root
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }

		[JsonPropertyName("notifications")]
		public List<Notification> Notifications { get; set; }

		[JsonPropertyName("userV")]
		public int? UserV { get; set; }

		[JsonPropertyName("appVersion")]
		public string AppVersion { get; set; }
	}

	public class Tmp
	{
		[JsonPropertyName("quest")]
		public Quest Quest { get; set; }

		[JsonPropertyName("drop")]
		public Drop Drop { get; set; }
	}

	public class Training
	{
		[JsonPropertyName("int")]
		public int? Int { get; set; }

		[JsonPropertyName("per")]
		public int? Per { get; set; }

		[JsonPropertyName("str")]
		public int? Str { get; set; }

		[JsonPropertyName("con")]
		public int? Con { get; set; }
	}
}
