using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Match;

namespace RFMultipMod
{
    class NetworkScriptandHUD : MonoBehaviour
    {
        NetworkManagerHUD HUDManager;
        NetworkManager ManagerNet;

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
