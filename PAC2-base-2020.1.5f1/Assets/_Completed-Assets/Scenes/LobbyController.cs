using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public TextMeshProUGUI m_connectedPlayers;
    public TMP_InputField m_playerName;

    private NetworkManager manager;

    void Start()
    {
        manager = FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();
    }

    void Update()
    {
        m_connectedPlayers.text = "Localhost players: " + manager.numPlayers+" / "+manager.maxConnections;
    }

    public void CreateGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.StartHost();
            }
        }
    }

    public void JoinGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.networkAddress = "localhost";
                manager.StartClient();
            }
        }
    }

    public void StartServer()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.StartServer();
            }
        }
    }
}
