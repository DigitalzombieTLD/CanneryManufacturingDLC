using MelonLoader;
using UnityEngine;
using Il2Cpp;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2CppTLD.Gear;

namespace CanneryManufacturingDLC
{
	public class CanneryManufacturingDLCMain : MelonMod
	{
		public override void OnInitializeMelon()
		{
            Settings.OnLoad();
            SpawnProbabilities.AddToModComponent();            
        }

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
            if(Settings.options.startGameWithCanneryCode && sceneName == "CanneryRegion_SANDBOX")
			{
                MelonCoroutines.Start(UnlockRoutine());
			}
        }

        public static IEnumerator UnlockRoutine()
        {
            for (float t = 0f; t < 2f; t += Time.deltaTime) yield return null;

            Unlock();
        }

        public static void Unlock()
        {
            if (Settings.options.startGameWithCanneryCode)
            {
                Lock[] allLocks = GameObject.FindObjectsOfType<Lock>();

                foreach (Lock singleLock in allLocks)
                {
                    if (singleLock.gameObject.name.Contains("WorkshopDoor"))
                    {
                        singleLock.m_LockState = LockState.Unlocked;
                        MelonLogger.Msg("Cannery door unlocked");
                    }
                }
            }
        }
    }
}