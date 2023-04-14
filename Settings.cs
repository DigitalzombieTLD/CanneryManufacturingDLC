using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;
using ModSettings;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CanneryManufacturingDLC
{
	internal class CannerySettings : JsonModSettings
	{	

		[Section("Gameplay Settings")]
		[Name("Gunpowder Crafting Location")]
		[Description("The default location is at the Ammo Workbench.")]
		[Choice("Anywhere", "Workbench", "Forge", "Ammo Workbench")]
		public int gunpowderLocationIndex = 2;

		[Name("Unlock cannery door")]
		[Description("Removes the need to find the code sheet.")]
		public bool startGameWithCanneryCode = false;

		[Name("Revolvers start ruined")]
		[Description("All the rifles you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine.")]
		public bool revolversStartRuined = false;

		[Name("Rifles start ruined")]
		[Description("All the rifles you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine.")]
		public bool riflesStartRuined = false;

		[Name("Distress Pistols start ruined")]
		[Description("All the distress pistols you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine. WARNING: ruined distress pistols get automatically deleted when you place them in a container.")]
		public bool flareGunsStartRuined = false;

		[Section("Spawn Settings")]
		[Name("Pilgram / Very High Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float pilgramSpawnProbability = 70f;

		[Name("Voyager / High Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float voyagerSpawnProbability = 40f;

		[Name("Stalker / Medium Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float stalkerSpawnProbability = 20f;

		[Name("Interloper / Low Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float interloperSpawnProbability = 8f;

		[Name("Wintermute")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float storySpawnProbability = 70f;

		[Name("Challenges")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float challengeSpawnProbability = 50f;

        protected override void OnConfirm()
        {
            base.OnConfirm();
			Settings.Apply();
        }
    }

    internal static class Settings
    {
        public static CannerySettings options;

        public static void OnLoad()
        {
            options = new CannerySettings();
            options.AddToModSettings("Cannery Manufacturing");
			
        }
    
		public static void Apply()
		{
			bool inSandbox = false;

            Scene[] scenes = UnityEngine.SceneManagement.SceneManager.GetAllScenes();

			foreach (Scene sc in scenes)
			{
				if(sc.name.Contains("SANDBOX"))
				{
					inSandbox = true;
				}
			}

			if (inSandbox && Settings.options.startGameWithCanneryCode)
			{
				CanneryManufacturingDLCMain.Unlock();
			}
        }
    }
}