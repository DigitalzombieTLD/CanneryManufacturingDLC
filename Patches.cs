using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;
using MelonLoader;
using ModComponent.Utils;

using UnityEngine;

namespace CanneryManufacturingDLC
{

	internal static class Patches
	{		
        [HarmonyPatch(typeof(Panel_Crafting), "Enable", new Type[] { typeof(bool), typeof(bool) })]
        private static class WorkbenchLocationPatch
        {
            internal static void Postfix(ref Panel_Crafting __instance, bool enable, bool fromPanel)
            {
                foreach (BlueprintData singlePrint in __instance.m_Blueprints)
                {
                    if (singlePrint.name == "BLUEPRINT_GEAR_GunpowderCan_A")
                    {
                        singlePrint.m_RequiredCraftingLocation = (CraftingLocation)Settings.options.gunpowderLocationIndex; // [("Anywhere", "Workbench", "Forge", "Ammo Workbench")]
                    }
                }

                __instance.RefreshBlueprintDisplayList();
            }
        }
		
        //This patch handles ruining firearms on start
        [HarmonyPatch(typeof(GearItem), "Awake")]
		private static class GearItem_Awake
		{
			private const string SCRAP_METAL_NAME = "GEAR_ScrapMetal";

			internal static void Postfix(GearItem __instance)
			{
				if (Utils.NormalizeName(__instance.name) == "GEAR_Crampons")
				{
					__instance.m_Millable = Utils.GetOrCreateComponent<Millable>(__instance.gameObject);

					__instance.m_Millable.m_CanRestoreFromWornOut = true;
					__instance.m_Millable.m_RecoveryDurationMinutes = 210;
					__instance.m_Millable.m_RepairDurationMinutes = 30;
					__instance.m_Millable.m_RepairRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RepairRequiredGearUnits = new int[] { 1 };
					__instance.m_Millable.m_RestoreRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RestoreRequiredGearUnits = new int[] { 4 };
					__instance.m_Millable.m_Skill = SkillType.None;
				}
				else if (Utils.NormalizeName(__instance.name) == "GEAR_Flaregun")
				{
					__instance.m_Millable = Utils.GetOrCreateComponent<Millable>(__instance.gameObject);

					__instance.m_Millable.m_CanRestoreFromWornOut = true;
					__instance.m_Millable.m_RecoveryDurationMinutes = 180;
					__instance.m_Millable.m_RepairDurationMinutes = 30;
					__instance.m_Millable.m_RepairRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RepairRequiredGearUnits = new int[] { 1 };
					__instance.m_Millable.m_RestoreRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RestoreRequiredGearUnits = new int[] { 3 };
					__instance.m_Millable.m_Skill = SkillType.None;
				}
				else if (__instance.m_BeenInspected)
				{
					return;
				}
				else if (Settings.options.flareGunsStartRuined && Utils.NormalizeName(__instance.name) == "GEAR_FlareGun")
				{
					__instance.ForceWornOut();
				}
				else if (Settings.options.revolversStartRuined && Utils.NormalizeName(__instance.name) == "GEAR_Revolver")
				{
					__instance.ForceWornOut();
				}
				else if (Settings.options.riflesStartRuined && Utils.NormalizeName(__instance.name) == "GEAR_Rifle")
				{
					__instance.ForceWornOut();
				}
			}

			private static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<GearItem>();
		}


		
	}
}
