using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectedScreen;

    public GameObject disconnectedScreen;

    public void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        PhotonNetwork.JoinLobby();
        
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
        {
            disconnectedScreen.SetActive(false);
        }
        
        connectedScreen.SetActive(true);
    }
    
     
}
