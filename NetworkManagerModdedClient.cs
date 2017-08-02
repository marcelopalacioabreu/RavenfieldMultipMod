using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace RFMultipMod
{
    class NetworkManagerModdedClient : NetworkManager
    {
        public NetworkManagerModdedClient()
        {
            dontDestroyOnLoad = true;
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            Utils.Log("Connected to a server located at " + conn.address);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            Utils.Log("Disconnected from a server located at " + conn.address);
        }

        public override void OnStartClient(NetworkClient client)
        {
            base.OnStartClient(client);
            Utils.Log("Starting client!");
        }
    }
}
