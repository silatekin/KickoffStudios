using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviourPunCallbacks
{
    
    public InputField createRoomTF;
    public InputField joinRoomTF;
    public GameObject mapSelection;
    
    
    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions{ MaxPlayers = 2}, null,null);
    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Sucess");
        mapSelection.SetActive(true);
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Room Joined Failed " + returnCode + " Message " + message);
    }
    
    public void OnClick_HeartMap()
    {
        PhotonNetwork.LoadLevel(2);
    }
    
    public void OnClick_StomachMap()
    {
        PhotonNetwork.LoadLevel(3);
    }
    
}
