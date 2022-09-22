using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Starter Assets
using StarterAssets;

//Moralis
using MoralisWeb3ApiSdk;
using Moralis.Platform.Objects;

//Wallet Connect
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
            Debug.LogError("MoralisController not found.");
        }

        StartCoroutine(DebugDelay());
    }

    IEnumerator DebugDelay()
    {
        yield return new WaitForSeconds(45);

        AuthController.walletAddress = "0x0F846d2Bc649eeb246eEF5BfA154aa38f5A01455";
        SceneManager.LoadScene("LobbyScene");

    }

    private void OnDisable()
    {
        LogOut();
    }

    public async void WalletConnectHandler(WCSessionData data)
    {
        Debug.Log("Hello Called");
        // Extract wallet address from the Wallet Connect Session data object.
        string address = data.accounts[0].ToLower();
        string appId = MoralisInterface.GetClient().ApplicationId;
        long serverTime = 0;

        // Retrieve server time from Moralis Server for message signature
        Dictionary<string, object> serverTimeResponse = await MoralisInterface.GetClient().Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

        if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
            !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
        {
            Debug.Log("Hello Failed to retrieve server time from Moralis Server!");
        }

        Debug.Log($"Hello Sending sign request for {address} ...");

        string signMessage = $"Moralis Authentication\n\nId: {appId}:{serverTime}";
        string response = await walletConnect.Session.EthPersonalSign(address, signMessage);

        Debug.Log($"Hello Signature {response} for {address} was returned.");

        // Create moralis auth data from message signing response.
        Dictionary<string, object> authData = new Dictionary<string, object> { { "id", address }, { "signature", response }, { "data", signMessage } }; 

        Debug.Log("Hello Logging in user.");

        // Attempt to login user.
        MoralisUser user = await MoralisInterface.LogInAsync(authData);

        if (user != null)
        {
            UserLoggedInHandler();
            Debug.Log($"Hello User {user.username} logged in successfully. ");
        }
        else
        {
            Debug.Log("Hello User login failed.");
        }
    }

    private async void UserLoggedInHandler()
    {
        //"Activate" game mode
        qrPanel.SetActive(false);
        
        //Check if user is logged in
        var user = await MoralisInterface.GetUserAsync();
        Debug.Log("Hello" + user);

        if (user != null)
        {
            Debug.Log("Hello not null");
            
            string addr = user.authData["moralisEth"]["id"].ToString();
            AuthController.walletAddress = addr;
            SceneManager.LoadScene("LobbyScene");
            return;
            playerWalletAddress.text = string.Format("{0}...{1}", addr.Substring(0, 6), addr.Substring(addr.Length - 3, 3));
            playerWalletAddress.gameObject.SetActive(true);
        }
        Debug.Log("Hello null");
    }
    
    private async void LogOut()
    {
        await walletConnect.Session.Disconnect();
        walletConnect.CLearSession();

        await MoralisInterface.LogOutAsync();
    }
}