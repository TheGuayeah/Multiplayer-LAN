using Complete;
using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : NetworkDiscoveryHUD
{
    public Image m_currentColor;

    private NetworkManager manager;

    void Start()
    {
        manager = FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();
        var hexColor = "#" + ColorUtility.ToHtmlStringRGBA(m_currentColor.color);
        PlayerPrefs.SetString("PlayerColor", hexColor);
        InvokeRepeating("SetServerList", 1f, 5f);
    }

    void Update()
    {

    }

    public void CreateGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                PlayerPrefs.SetString("PlayerName", m_playerName.text);
                manager.StartHost();
            }
        }
    }

    public void SetPlayerColor(Image colorImage)
    {
        m_currentColor.color = colorImage.color;
        var hexColor = "#" + ColorUtility.ToHtmlStringRGBA(colorImage.color);
        PlayerPrefs.SetString("PlayerColor", hexColor);
    }

    public void SetServerList()
    {
        FindServer();
    }
}
