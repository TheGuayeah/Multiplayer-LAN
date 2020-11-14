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

    void Start()
    {
        var hexColor = "#" + ColorUtility.ToHtmlStringRGBA(m_currentColor.color);
        PlayerPrefs.SetString("PlayerColor", hexColor);
        InvokeRepeating("SetServerList", 1f, 5f);
    }

    void Update()
    {

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
