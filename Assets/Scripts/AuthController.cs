using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FusedVR.Web3;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Text;


public class AuthController : MonoBehaviour
{
    public InputField emailField;
    public GameObject infoPanel;
    public static string walletAddress;

    async public void SendMagicLink()
    {

        if (emailField.text == "") return;

        infoPanel.SetActive(true);

        if (await Web3Manager.Login(emailField.text, "Aliena-255-Oculus-Quest2"))
        {
            walletAddress = await Web3Manager.GetAddress();

            SceneManager.LoadScene("LobbyScene");
        }

    }

	[Serializable]
	class TestModel
    {
		public String name;
		public int age;

    }

	[Serializable]
	class TestRes
	{
		public String id;
		public String createdAt;

	}

	public void doPost()
	{
		TestModel model = new TestModel();
		model.name = "hellllo";
		model.age = 23;
		String json = JsonUtility.ToJson(model);


		string URL = "https://reqres.in/api/users";

		StartCoroutine(Post(URL, json));
	}


	IEnumerator Post(string url, string bodyJsonString)
	{
		var request = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes("");
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();

		TestRes model = JsonUtility.FromJson<TestRes>(request.downloadHandler.text);

		Debug.Log("Status Code: " + model.id + model.createdAt);
	}



}
