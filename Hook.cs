using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RFMultipMod
{
    public class Hook : MonoBehaviour
    {
        private static HarmonyInstance _harmony;

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
            //InstantActionMaps.cs - the instant action section of the play menu.
            //  Has Method StartGame()
            //GameManager.cs - handles a lot of stuff
            //  Methods such as IsInCustomLevel(), IsIngame(), StartLevel(), StartCustomLevel()
            //SceneManager
            //  Has a list, SceneManager.sceneLoaded, of UnityAction<Scene, LoadSceneMode>, to call when a level is loaded.


            //The current player can be accessed through ActorManager.instance.player

            _harmony = HarmonyInstance.Create("rfmultipmod.multipmodharmonyhooker");

            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(InstantActionMaps))]
        [HarmonyPatch("StartGame")]
        [UsedImplicitly]
        private class GameStartPatch
        {
            static void Prefix()
            {
                Utils.Log("A game started! Injecting network script and HUD...");
                //Application.Quit();
                SceneManager.GetActiveScene().GetRootGameObjects()[0].AddComponent<NetworkScriptandHUD>();
            }
        }
    }
}