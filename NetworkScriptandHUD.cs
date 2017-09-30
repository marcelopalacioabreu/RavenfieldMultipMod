using UnityEngine;
using UnityEngine.Networking;

namespace RFMultipMod
{
    class NetworkScriptandHUD : NetworkBehaviour
    {
        public NetworkManagerHUD HUDManager;
        public NetworkManager ManagerNet;
        public NetworkIdentity MyNetworkIdentity;

        public void Start()
        {
            Utils.Log("Starting NetworkScriptAndHUD.");
            ManagerNet = gameObject.AddComponent<NetworkManager>();
            HUDManager = gameObject.AddComponent<NetworkManagerHUD>();
            HUDManager.manager = ManagerNet;
            HUDManager.showGUI = true;
        }
    }

    // Code for NetworkManagerHUD: https://pastebin.com/EbVQNpsS
    // Code for NetworkManager: https://pastebin.com/HzdPGR3N
}
