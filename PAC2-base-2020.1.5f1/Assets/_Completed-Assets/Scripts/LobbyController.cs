using Complete;
using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public TextMeshProUGUI m_connectedPlayers;
    public TMP_InputField m_playerName;
    public Image m_currentColor;
    public GameObject serverItem;
    public GameObject serverItemsParent;

    private NetworkManager manager;

    void Start()
    {
        manager = FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();
        var hexColor = "#" + ColorUtility.ToHtmlStringRGBA(m_currentColor.color);
        PlayerPrefs.SetString("PlayerColor", hexColor);
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
}
