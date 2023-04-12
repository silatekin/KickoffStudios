using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _roomName;


    public void OnClickCreateRoom()
    {
        RoomOptions options = new RoomOptions();

        PhotonNetwork.JoinOrCreateRoom("basics", options, TypedLobby.Default);
    }

 

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }
}
