using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Starter Assets

//Moralis
using MoralisWeb3ApiSdk;
using Moralis.Platform.Objects;

//Wallet Connect
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;
using UnityEngine.UI;

public class AuthControllerMoralis : MonoBehaviour
{

    [Header("WEB3")]
    [SerializeField] private MoralisController moralisController;
    [SerializeField] private WalletConnect walletConnect;

    [Header("UI")]
    [SerializeField] private GameObject qrPanel;
    [SerializeField] Text playerWalletAddress;

    private async void Start()
    {
        if (moralisController != null)
        {
            await moralisController.Initialize();
        }
        else
        {
            Debug.Log("Aliena-255-Log: MoralisController not found.");
        }
    }

    private void OnDisable()
    {
        LogOut();
    }

    public async void WalletConnectHandler(WCSessionData data)
    {
        Debug.Log(data);
        Debug.Log("Aliena-255-Log: 1. Connected To Metamask");
        // Extract wallet address from the Wallet Connect Session data object.
        string address = data.accounts[0].ToLower();
        string appId = MoralisInterface.GetClient().ApplicationId;
        long serverTime = 0;

        // Retrieve server time from Moralis Server for message signature
        Dictionary<string, object> serverTimeResponse = await MoralisInterface.GetClient().Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

        if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
            !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
        {
            Debug.Log("Aliena-255-Log: 2");
            Debug.Log("Aliena-255-Log: Failed to retrieve server time from Moralis Server!");
        }
        Debug.Log("Aliena-255-Log: 3");

        Debug.Log($"Aliena-255-Log: Sending sign request for {address} ...");

        string signMessage = $"Moralis Authentication\n\nId: {appId}:{serverTime}";
        string response = await walletConnect.Session.EthPersonalSign(address, signMessage);
        Debug.Log("Aliena-255-Log: 4");

        Debug.Log($"Aliena-255-Log: Signature {response} for {address} was returned.");

        // Create moralis auth data from message signing response.
        Dictionary<string, object> authData = new Dictionary<string, object> { { "id", address }, { "signature", response }, { "data", signMessage } };

        Debug.Log("Aliena-255-Log: Logging in user.");

        // Attempt to login user.
        MoralisUser user = await MoralisInterface.LogInAsync(authData);

        if (user != null)
        {
            Debug.Log("Aliena-255-Log: 5");
            UserLoggedInHandler();
            Debug.Log($"Aliena-255-Log: User {user.username} logged in successfully. ");
        }
        else
        {
            Debug.Log("Aliena-255-Log: 6");
            Debug.Log("Aliena-255-Log: User login failed.");
        }
        Debug.Log("Aliena-255-Log: 7");
    }

    private async void UserLoggedInHandler()
    {
        //"Activate" game mode
        qrPanel.SetActive(false);

        //Check if user is logged in
        var user = await MoralisInterface.GetUserAsync();

        if (user != null)
        {
            string addr = user.authData["moralisEth"]["id"].ToString();
            playerWalletAddress.text = string.Format("{0}...{1}", addr.Substring(0, 6), addr.Substring(addr.Length - 3, 3));
            playerWalletAddress.gameObject.SetActive(true);
        }
    }

    private async void LogOut()
    {
        await walletConnect.Session.Disconnect();
        walletConnect.CLearSession();

        await MoralisInterface.LogOutAsync();
    }
}