using System.Reflection;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RFMultipMod
{
    public class Hook
    {
        private static HarmonyInstance _harmony;
        private static bool _hasinsertedyet = false;
        static NetworkScriptAndHud injectedNetworkStuff;

        public static void StartMultiplayerMod()
        {
            Utils.Log("");
            Utils.Log("");
            Utils.Log(
                "----------------------------------------------------------------------------------------------------------");
            Utils.Log("This installation is MODDED, with the Ravenfield Multiplayer Mod, created by the RFMpMod team.");
            Utils.Log("");
            Utils.Log("If you encounter issues, tell them, not SteelRaven7!");
            Utils.Log(
                "----------------------------------------------------------------------------------------------------------\n");
            Utils.Log("Ravenfield Multiplayer Mod is GO for launch.\n"); //This will all be written to output.log.

            //Possibly create a ModInformation - reflection is likely required, or we could potentially extend it, and add it with ModPage.AddPanelForMod

            //Useful stuff for server side - can start game with -map <mapname> or -custommap <workshop id?>

            //Useful Classes:
            //GameManager.cs - handles a lot of stuff
            //  Methods such as IsInCustomLevel(), IsIngame(), StartLevel(), StartCustomLevel()


            //The current player can be accessed through ActorManager.instance.player

            _harmony = HarmonyInstance.Create("rfmultipmod.multipmodharmonyhooker");

            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            Utils.Log("Injection setup successful. Helloooooo, world!");
        }

        [HarmonyPatch(typeof(InstantActionMaps))]
        [HarmonyPatch("Awake")]
        [UsedImplicitly]
        private class GameStartPatch
        {
            [UsedImplicitly]
            static void Postfix()
            {
                if (!_hasinsertedyet)
                {
                    Utils.Log("GameManager is ready. Inserting networking script...");
                    injectedNetworkStuff = SceneManager.GetActiveScene().GetRootGameObjects()[0]
                        .AddComponent<NetworkScriptAndHud>();
                    Utils.Log("Inserted networking script.");
                    _hasinsertedyet = true;
                }
            }
        }

        [HarmonyPatch(typeof(FpsActorController))]
        [HarmonyPatch("Update")]
        [UsedImplicitly]
        private class PlayerUpdatePatch
        {
            // ReSharper disable once InconsistentNaming,ArrangeTypeMemberModifiers
            [UsedImplicitly]
            static bool Prefix(FpsActorController __instance)
            {
                Actor actor = __instance.actor;
                GameObject gameObject = actor.gameObject;
                NetworkTransform transform = gameObject.transform.parent.GetComponentInChildren<NetworkTransform>();
                var result = transform.isLocalPlayer; //Doing it the long way to hopefully be able to catch errors easier. DO NOT SIMPLIFY.
                return result;
            }
        }
    }
}