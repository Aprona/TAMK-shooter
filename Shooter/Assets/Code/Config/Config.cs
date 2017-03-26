using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKShooter.Configs
{
	public static class Config
	{
		public const string MenuSceneName = "Menu";

		public static readonly Dictionary<int, string> LevelNames =
			new Dictionary<int, string> ()
			{
				{ 1, "Level1" },
				{ 2, "Level2" }
			};

		public const string PlayerProjectileLayerName = "PlayerProjectile";
		public const string EnemyProjectileLayerName = "EnemyProjectile";

		public const string HorizontalWASDName = "HorizontalWASD";
		public const string VerticalWASDName = "VerticalWASD";
		public const string HorizontalArrowsName = "HorizontalArrows";
		public const string VerticalArrowsName = "VerticalArrows";
		public const string HorizontalGamepad1Name = "HorizontalGamepad1";
		public const string HorizontalGamepad2Name = "HorizontalGamepad2";
		public const string VerticalGamepad1Name = "VerticalGamepad1";
		public const string VerticalGamepad2Name = "VerticalGamepad2";
		public const string ShootWASDName = "ShootWASD";
		public const string ShootArrowsName = "ShootArrows";
		public const string ShootGamepad1Name = "ShootGamepad1";
		public const string ShootGamepad2Name = "ShootGamepad2";

		public const float DeadZone = 0.3f;
	}
}
