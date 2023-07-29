using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ar6yZuK.Habitica.Response.User;
public class BuyHealthPotion
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
		[JsonPropertyName("buffs")]
		public Buffs Buffs { get; set; }

		[JsonPropertyName("training")]
		public Training Training { get; set; }

		[JsonPropertyName("hp")]
		public int? Hp { get; set; }

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

	public class Notification
	{
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }

		[JsonPropertyName("seen")]
		public bool? Seen { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }
	}

	public class Root
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("notifications")]
		public List<Notification> Notifications { get; set; }

		[JsonPropertyName("userV")]
		public int? UserV { get; set; }

		[JsonPropertyName("appVersion")]
		public string AppVersion { get; set; }
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
