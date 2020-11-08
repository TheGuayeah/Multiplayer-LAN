using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Discovery
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkDiscoveryHUD")]
    [HelpURL("https://mirror-networking.com/docs/Components/NetworkDiscovery.html")]
    //[RequireComponent(typeof(NetworkDiscovery))]
    public class NetworkDiscoveryHUD : MonoBehaviour
    {
        public readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;

        public NetworkDiscovery networkDiscovery;
        public InputField m_playerName;
        public GameObject serverItem;
        public GameObject serverItemsParent;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GameObject.FindObjectOfType<NetworkDiscovery>().GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (NetworkServer.active || NetworkClient.active)
                return;

            //if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
                //DrawGUI();
        }

        void DrawGUI()
        {
            // show list of found server

            GUILayout.Label($"Discovered Servers [{discoveredServers.Count}]:");

            // servers
            scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

            foreach (ServerResponse info in discoveredServers.Values)
                if (GUILayout.Button(info.EndPoint.Address.ToString()))
                    Connect(info);

            GUILayout.EndScrollView();
        }

        public void FindServer()
        {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
        }

        public void StartHost()
        {
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }

        public void StartServer()
        {
            discoveredServers.Clear();
            NetworkManager.singleton.StartServer();
            networkDiscovery.AdvertiseServer();
        }

        public void Connect(ServerResponse info)
        {
            PlayerPrefs.SetString("PlayerName", m_playerName.text);
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;

            if(serverItemsParent != null)
            {
                for (int i = 0; i < serverItemsParent.transform.childCount; i++)
                {
                    Destroy(serverItemsParent.transform.GetChild(i).gameObject);
                }

                foreach (ServerResponse server in discoveredServers.Values)
                {
                    GameObject item = Instantiate(serverItem, serverItemsParent.transform);
                    item.transform.GetChild(2).gameObject.GetComponent<Text>().text = server.EndPoint.Address.ToString();
                    item.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => Connect(server));
                }
            }
        }
    }
}
