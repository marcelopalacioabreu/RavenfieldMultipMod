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
        NetworkScriptandHUD()
        {
            HUDManager = gameObject.AddComponent<NetworkManagerHUD>();
            ManagerNet = gameObject.AddComponent<NetworkManager>();
        }
    }

    // Code for NetworkManagerHUD: https://pastebin.com/EbVQNpsS
    // Code for NetworkManager: https://pastebin.com/HzdPGR3N
}
