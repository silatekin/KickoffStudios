using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Manager : MonoBehaviourPunCallbacks
{
    public GameObject playerSelectionImage;
    public GameObject MicrobePrefab;
    public GameObject WBCPrefab;
    
    public Transform microbeStartingPoint;
    public Transform WBCStartingPoint;

    

    public void OnClick_MicrobeButton()
    {
        cursorManagement();
        playerSelectionImage.SetActive(false);
        GameObject microbe =
            PhotonNetwork.Instantiate("Microbe",microbeStartingPoint.position, MicrobePrefab.transform.rotation);
        Debug.Log("Microbe");
       
    }
    
    public void OnClick_WBCButton()
    {
        cursorManagement();
        playerSelectionImage.SetActive(false);
        GameObject WBC =
            PhotonNetwork.Instantiate("White Blood Cell",WBCStartingPoint.position, WBCPrefab.transform.rotation);
        Debug.Log("WBC");
        
    }

    private void cursorManagement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
