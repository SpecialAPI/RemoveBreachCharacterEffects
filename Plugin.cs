using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RemoveBreachCharacterEffects
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "spapi.etg.removebreachcharactereffects";
        public const string NAME = "Remove Base-Specific Breach CC Effects";
        public const string VERSION = "1.0.0";
        public static List<string> NameShortsRemoved = new List<string>();

        public void Awake()
        {
            foreach(string file in Directory.GetFiles(Paths.PluginPath, "*-ccremove.spapi", SearchOption.AllDirectories))
            {
                NameShortsRemoved.AddRange(File.ReadAllLines(file).Select(x => $"player{x.ToLowerInvariant()}"));
            }
            new Harmony(GUID).PatchAll();
        }

        [HarmonyPatch(typeof(FoyerCharacterSelectFlag), nameof(FoyerCharacterSelectFlag.Start))]
        [HarmonyPostfix]
        public static void GetRidOfStuff(FoyerCharacterSelectFlag __instance)
        {
            if (NameShortsRemoved.Contains(__instance.CharacterPrefabPath))
            {
                __instance.IsGunslinger = false;
                if(__instance.OverheadElement != null && __instance.OverheadElement.GetComponent<FoyerInfoPanelController>() != null && __instance.OverheadElement.GetComponent<FoyerInfoPanelController>().characterIdentity == 
                    PlayableCharacters.Gunslinger)
                {
                    __instance.OverheadElement.GetComponent<FoyerInfoPanelController>().characterIdentity = PlayableCharacters.Pilot;
                }
                var idledoer = __instance.GetComponent<CharacterSelectIdleDoer>();
                if(idledoer != null)
                {
                    idledoer.phases.Do(x => x.vfxTrigger = CharacterSelectIdlePhase.VFXPhaseTrigger.NONE);
                }
                var dog = __instance.transform.Find("Doggy");
                if(dog != null)
                {
                    dog.gameObject.SetActive(false);
                }
            }
        }
    }
}
