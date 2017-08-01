namespace RFMultipMod
{
    public class Hook
    {
        public static void StartMultiplayerMod()
        {
            Utils.Log("\n\n----------------------------------------------------------------------------------------------------------");
            Utils.Log("This installation is MODDED, with the Ravenfield Multiplayer Mod, created by the RFMpMod team.\n");
            Utils.Log("If you encounter issues, tell them, not SteelRaven7!");
            Utils.Log("----------------------------------------------------------------------------------------------------------\n\n");
            Utils.Log("Ravenfield Multiplayer Mod is GO for launch.\n"); //This will all be written to output.log.

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
        }
    }
}
