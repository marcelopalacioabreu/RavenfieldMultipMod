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
        static NetworkScriptandHUD injectedNetworkStuff;

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
        [HarmonyPatch("StartGame")]
        [UsedImplicitly]
        private class GameStartPatch
        {
            [UsedImplicitly]
            static void Prefix()
            {
                Utils.Log("A game started! Injecting network script and HUD...");
                injectedNetworkStuff = SceneManager.GetActiveScene().GetRootGameObjects()[0].AddComponent<NetworkScriptandHUD>();
            }
        }

        [HarmonyPatch(typeof(FpsActorController))]
        [HarmonyPatch("Awake")]
        private class PlayerCreatePatch
        {
            static void Prefix()
            {
                Actor playerPrefab = Object.FindObjectOfType<Actor>(); //There is only one AFAICT when this is called.
                Utils.Log("Player prefab: " + playerPrefab);

                NetworkIdentity nId = playerPrefab.gameObject.AddComponent<NetworkIdentity>();
                nId.localPlayerAuthority = true;
                Utils.Log("Injected NetworkIdentity into player prefab");
            
                playerPrefab.gameObject.AddComponent<NetworkTransform>();
                Utils.Log("Injected NetworkTransform into player prefab");

                injectedNetworkStuff.ManagerNet.playerPrefab = playerPrefab.gameObject;
            }
        }

        [HarmonyPatch(typeof(FpsActorController))]
        [HarmonyPatch("Update")]
        [UsedImplicitly]
        private class PlayerUpdatePatch
        {
            // ReSharper disable once InconsistentNaming
            [UsedImplicitly]
            static bool Prefix(FpsActorController __instance)
            {
                return __instance.actor.gameObject.GetComponent<NetworkTransform>()
                    .isLocalPlayer; //If not local player, do not execute Update() in FpsActorController
            }
        }
    }
}