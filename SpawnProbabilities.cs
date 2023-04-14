using GearSpawner;

namespace CanneryManufacturingDLC
{
	public static class SpawnProbabilities
	{
		public static void AddToModComponent()
		{
			SpawnTagManager.AddFunction("CanneryManufacturing", GetProbability);
		}
	
		public static float GetProbability(DifficultyLevel difficultyLevel, FirearmAvailability firearmAvailability, GearSpawnInfo gearSpawnInfo)
		{
			if (firearmAvailability == FirearmAvailability.None && gearSpawnInfo.PrefabName != "gear_smallgunpowdercan")
			{
				return 0f;
			}

			if (firearmAvailability == FirearmAvailability.Revolver && gearSpawnInfo.PrefabName == "gear_riflereloadingbox")
			{
				return 0f;
			}

			if (firearmAvailability == FirearmAvailability.Rifle && gearSpawnInfo.PrefabName == "gear_revolverreloadingbox")
			{
				return 0f;
			}

			return difficultyLevel switch
			{
				DifficultyLevel.Pilgram => Settings.options.pilgramSpawnProbability,
				DifficultyLevel.Voyager => Settings.options.voyagerSpawnProbability,
				DifficultyLevel.Stalker => Settings.options.stalkerSpawnProbability,
				DifficultyLevel.Interloper => Settings.options.interloperSpawnProbability,
				DifficultyLevel.Challenge => Settings.options.challengeSpawnProbability,
				DifficultyLevel.Storymode => Settings.options.storySpawnProbability,
				_ => 0f,
			};
		}
	}
}
