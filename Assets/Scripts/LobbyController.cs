using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public Text WalletAddress;
    public Text StatusMessage;
    public InputField RoomName;
    public GameObject[] buttons;

    void Start()
    {
        WalletAddress.text = "Your Wallet Address is: " + AuthController.walletAddress;

        setDisableButon(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        setDisableButon(true);
        StatusMessage.text = "Connected";
        //do something
    }

    public void CreateRoom()
    {
        if(RoomName.text != "") PhotonNetwork.CreateRoom(RoomName.text);
    }

    public void JoinRoom()
    {
        if (RoomName.text != "") PhotonNetwork.JoinRoom(RoomName.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Solar System v.2");
    }

    private void setDisableButon(bool isActive)
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(isActive);
    }
}
