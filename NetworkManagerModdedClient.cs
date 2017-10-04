using System;
using System.Linq;
using UnityEngine.Networking;

namespace RFMultipMod
{
    class NetworkManagerModdedClient : NetworkManager
    {
        public bool NetworkConnectionActive;
        
        public NetworkManagerModdedClient()
        {
            dontDestroyOnLoad = true;
        }

        //Called from client when connected to a server
        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            Log("[Client] Connected to " + conn.address);
        }

        //Called from client when disconnected.
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            Log("[Client] Disconnected from " + conn.address);
        }

        //Called when the client starts
        public override void OnStartClient(NetworkClient networkClient)
        {
            base.OnStartClient(networkClient);
            Log("[Client] Starting!");
            NetworkConnectionActive = true;
        }

        public override void OnStartHost()
        {
            base.OnStartHost();
            Log("[Host] Starting!");
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            Log("[Server] Incoming connection from " + conn.address);
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            Log("[Server] Starting!");
            NetworkConnectionActive = true;
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);
            Log("[Server] Ready!");
            FpsActorController player = FindObjectsOfType<FpsActorController>().First(p =>
                p.gameObject.GetComponentInChildren<NetworkTransform>().netId.Value == 0);
            Destroy(player.gameObject);
            
            ActorManager.instance.actors.Remove(player.actor);
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            base.OnServerAddPlayer(conn, playerControllerId);
            Log("[Server] Adding player from " + conn.address + " with ID " + playerControllerId);

            FpsActorController justAdded = FindObjectsOfType<FpsActorController>().First(ctrl =>
                ctrl.gameObject.GetComponentInChildren<NetworkTransform>().playerControllerId == playerControllerId);
            
            Log("[Server] Player known to actormanager? " + ActorManager.instance.actors.Contains(justAdded.actor));
            ActorManager.instance.actors.Add(justAdded.actor);
            ActorManager.instance.player = justAdded.actor;
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);
            Log("[Server] Changing map...");
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            Log("[Client] Changing map...");
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            Log("[Server] " + conn.address + " lost connection.");
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            Log("[Server] Stopping!");
            NetworkConnectionActive = false;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            Log("[Client] Stopping!");
            NetworkConnectionActive = false;
        }

        public override void OnStopHost()
        {
            base.OnStopHost();
            Log("[Host] Stopping!");
        }

        private void Log(string msg)
        {
            Utils.Log("[Network Manager] " + msg);
        }
    }
}