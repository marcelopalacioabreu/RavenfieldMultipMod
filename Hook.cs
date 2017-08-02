using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RFMultipMod
{
    public class Hook
    {
        public static void StartMultiplayerMod()
        {
            Utils.Log("");
            Utils.Log("");
            Utils.Log("----------------------------------------------------------------------------------------------------------");
            Utils.Log("This installation is MODDED, with the Ravenfield Multiplayer Mod, created by the RFMpMod team.");
            Utils.Log("");
            Utils.Log("If you encounter issues, tell them, not SteelRaven7!");
            Utils.Log("----------------------------------------------------------------------------------------------------------\n");
            Utils.Log("Ravenfield Multiplayer Mod is GO for launch.\n"); //This will all be written to output.log.



            //----------------------------------------------------------------
            //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
            //TODO: Get the installer to copy Injection32 and Injection64 DLLs
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //----------------------------------------------------------------


            //TODO: Get some runtime level hooks injected here, see https://www.codeproject.com/articles/463508/net-clr-injection-modify-il-code-on-run-time

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

            InjectionHelper.Initialize();

            Type type = typeof(InstantActionMaps);
            MethodInfo info = type.GetMethod("StartGame", BindingFlags.Public | BindingFlags.Instance);
            MethodInfo ourOnGameStart = typeof(Hook).GetMethod("OnGameStart", BindingFlags.Public | BindingFlags.Static);
            byte[] il = info.GetMethodBody().GetILAsByteArray();

            List<Byte> ilList = new List<byte>(il)
            {
                (byte)OpCodes.Call.Value,
                (byte)(ourOnGameStart.MetadataToken & 0xFF),
                (byte)(ourOnGameStart.MetadataToken >> 8 & 0xFF),
                (byte)(ourOnGameStart.MetadataToken >> 16 & 0xFF),
                (byte)(ourOnGameStart.MetadataToken >> 24 & 0xFF)
            };

            InjectionHelper.UpdateILCodes(info, ilList.ToArray());
        }

        public static void OnGameStart()
        {
            Utils.Log("A game started!");
        }
    }
}
