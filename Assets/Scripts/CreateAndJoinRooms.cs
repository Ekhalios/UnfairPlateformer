using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public MapSelector mapSelector;
    void Start()
    {
        
    }

    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        string fileSelected = mapSelector.getfileSelected();
        PlayerPrefs.SetString("MapFileName", "/CustomMaps/" + fileSelected);
        PhotonNetwork.LoadLevel("MultiScene");
    }
}
