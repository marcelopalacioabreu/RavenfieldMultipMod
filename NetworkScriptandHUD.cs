using UnityEngine;
using UnityEngine.Networking;

namespace RFMultipMod
{
    internal class NetworkScriptAndHud : NetworkBehaviour
    {
        public NetworkManagerHUD HudManager;
        public NetworkManagerModdedClient NetworkManager;
        public NetworkIdentity MyNetworkIdentity;

        public void Start()
        {
            Utils.Log("Starting NetworkScriptAndHUD.");
            Utils.Log("Adding client and hud...");
            NetworkManager = gameObject.AddComponent<NetworkManagerModdedClient>();
            NetworkManager.networkAddress = "0.0.0.0";
            HudManager = gameObject.AddComponent<NetworkManagerHUD>();
            HudManager.manager = NetworkManager;
            HudManager.showGUI = true;
            
            Utils.Log("Beginning injection into player prefab...");
            GameObject playerPrefab = GameManager.instance.playerPrefab;

            NetworkIdentity nId = playerPrefab.gameObject.AddComponent<NetworkIdentity>();
            nId.localPlayerAuthority = true;
            Utils.Log("Injected NetworkIdentity into player prefab");
            
            playerPrefab.gameObject.AddComponent<NetworkTransform>();
            Utils.Log("Injected NetworkTransform into player prefab");

            NetworkManager.playerPrefab = playerPrefab;
            
            NetworkManager.spawnPrefabs.Add(playerPrefab);
            DontDestroyOnLoad(this);
        }

        public void Update()
        {
            //GameObject playerPrefab = GameManager.instance.playerPrefab;
            //Utils.LogAllChildren(playerPrefab);
            if (Input.GetKeyDown(KeyCode.F1))
            {
                HudManager.showGUI = !HudManager.showGUI;
            }
        }
    }

    // Code for NetworkManagerHUD: https://pastebin.com/EbVQNpsS
    // Code for NetworkManager: https://pastebin.com/HzdPGR3N
}
