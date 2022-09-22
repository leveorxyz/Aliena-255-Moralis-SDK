using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = System.Random;

public class VRLookMove : MonoBehaviour
{
    // DashBoard UI
    public GameObject sunSpeher;
    public GameObject[] planets;
    public Text LevelValue;
    public Text WalletAddress;
    public Text NFTList;
    public Text healthText;
    public Text fuelText;
    public Text dangerText;

    public int health = 100;
    public int fuel = 100;
    
    // Orbit counter
    public int playerLocationinOrbit;
    public GameObject MyLocation;
    public Text NearbyPlanet;
    public int DistanceOfNearbyPlanet;
    public Text DisplayDistance;

    public GameObject needleTransfrom;
    private const float MAX_SPEED_ANGLE = -160.22f;
    private const float ZERO_SPEED_ANGLE = 73.51f;

    private float needleSpeedMax;
    private float needleSpeed;

    public bool isShowInfoButtonClick = false;
    public Text showInfoButtonText;

    public Text messageText;

    bool isMove = false;
    bool isFocused = false;
    bool isUpdateHealth = false;

    Random rnd = new Random(DateTime.Now.Millisecond);

    public bool isClickMintNFT = false;
    public bool isClickGetNFT = false;

    bool shouldTakeDamage = false;
    public GameObject[] enemies;

    public bool[] visitedPlanet;
    public Image[] planetAvater;
    public Text pointsText;

    private bool isClickGetMintedStatus = false;

    void Start()
    {
        visitedPlanet = new bool[9];

        for (int i = 0; i < visitedPlanet.Length; i++)
        {
            visitedPlanet[i] = false;
        }

        WalletAddress.text = AuthController.walletAddress;
        sunSpeher = GameObject.FindGameObjectWithTag("sunsphere");

        planets = new GameObject[8];

        planets[0] = GameObject.FindGameObjectWithTag("mercury");
        planets[1] = GameObject.FindGameObjectWithTag("venus");
        planets[2] = GameObject.FindGameObjectWithTag("earth");
        planets[3] = GameObject.FindGameObjectWithTag("mars");
        planets[4] = GameObject.FindGameObjectWithTag("jupiter");
        planets[5] = GameObject.FindGameObjectWithTag("saturn");
        planets[6] = GameObject.FindGameObjectWithTag("uranus");
        planets[7] = GameObject.FindGameObjectWithTag("neptune");

        


        needleSpeed = 0.0f;
        needleSpeedMax = 200.0f;
        
        countOrbit();
        updateMyLocation();

        fuelText.text = "Fuel " + fuel.ToString() + "%";

        InvokeRepeating("updateFuelLevel", 1f, 1f);
        InvokeRepeating("takeDamage", 1f, 1f);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        getPlayerData();


    }

    void updateFuelLevel()
    {
        if (isMove) fuel -= 1;
        else if (!isMove && isFocused) fuel += 2;

        if (fuel <= 0) fuel = 0;
        else if (fuel > 100) fuel = 100;

        fuelText.text = "Fuel " + fuel.ToString() + "%";
    }

    void takeDamage()
    {

        if(shouldTakeDamage)
        {
            health -= 1;

            if (health < 0) health = 0;

            healthText.text = "Health " + health.ToString() + "%";
        }

        
    }

    void Update()
    {
        int Yposition = (int) transform.position.y;
        LevelValue.text = Yposition.ToString();
        countOrbit();
        StartCoroutine(CheckMoving());
        setNeedleMax();

        needleSpeed = Mathf.Clamp(needleSpeed, 0f, needleSpeedMax);
        needleTransfrom.transform.localEulerAngles = new Vector3(0, 0, GetSpeedRotation());

        for(int i=0; i<planets.Length; i++)
        {
            OrbitController orbitController = planets[i].GetComponent<OrbitController>();
            if(orbitController.stop == 0)
            {
                isFocused = true;
                break;
            }
            else
            {
                isFocused = false;
            }
        }

        float minDistance = float.MaxValue;

        for(int i=0; i<enemies.Length; i++)
        {
            float distance = Vector3.Distance(gameObject.transform.position, enemies[i].transform.position);

            if (distance < minDistance)
                minDistance = distance;
        }

        dangerText.text = "Danger in " + (int)minDistance + " mkm";

        if (minDistance <= 20)
            shouldTakeDamage = true;
        else
            shouldTakeDamage = false;

        int points = 0;

        for(int i=0; i<visitedPlanet.Length; i++)
        {
            if(visitedPlanet[i])
            {
                planetAvater[i].color = new Color32(255, 255, 255, 255);
                points += 10;
            }
            else
            {
                planetAvater[i].color = new Color32(255, 255, 255, 40);
            }
        }

        pointsText.text = "Points " + points.ToString();

    }

    public void onClickMintNFTButton()
    {
        messageText.text = "Minting...";
        StartCoroutine(Minting());
        return;
        isClickMintNFT = !isClickMintNFT;
    }

    public void mintNFT(PlanetModel model)
    {
        if (!isClickMintNFT) return;
        isClickMintNFT = false;

        makeReq("https://metaspace.up.railway.app/api/safe-mint?address=" + AuthController.walletAddress +  "&name=" + model.name, false);
      
    }

    public void Clear()
    {
        messageText.text = "";
    }

    public void getPlayerData()
    {
        messageText.text = "Fetching your Data...";
        StartCoroutine(GetPlayerDataDebug());
        return;

        StartCoroutine(makeGetPlayerDataRequest("https://api.nft.storage/"));
    }

    IEnumerator GetPlayerDataDebug()
    {

        yield return new WaitForSeconds(5);


        string visitedPlanetResult = "00110010";

        for (int i = 0; i < visitedPlanetResult.Length; i++)
        {
            if (visitedPlanetResult[i] == '1')
                visitedPlanet[i] = true;
            else
                visitedPlanet[i] = false;
        }

        messageText.text = "Player data is up to date";


    }

    IEnumerator UploadPlayerDataDebug()
    {

        yield return new WaitForSeconds(5);

        messageText.text = "Player data is up to date";


    }

    IEnumerator makeGetPlayerDataRequest(string url)
    {
        var request = new UnityWebRequest(url, "GET");
        byte[] bodyRaw = Encoding.UTF8.GetBytes("");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweGYzOGY5OWMwYzQ5RDUwNzM2NTA1NjA4ZjY2M0FhYzVBZGJmRWNkMDkiLCJpc3MiOiJuZnQtc3RvcmFnZSIsImlhdCI6MTY2MzM4Nzc2OTkxNywibmFtZSI6IkFsaWVuYS0yNTUifQ.zQrushw1sgZ1P56NvGkbDGJaafCL-PIAeCzXNKFSUbs");

        yield return request.SendWebRequest();

        if(request.responseCode == 200)
        {
            try
            {
                dynamic data = JObject.Parse(request.downloadHandler.text);
                string cid = data.value[0].cid;

                if(cid.Length > 3)
                {
                    StartCoroutine(downloadPlayerData("https://" + cid + ".ipfs.nftstorage.link"));

                }
                else
                {
                    messageText.text = "No data found!";
                }

            }
            catch(Exception e)
            {
                Debug.Log("Hello " + e.Message);
            }
            

        }
        else
        {
            messageText.text = "Something went wrong! Error Code:" + request.responseCode.ToString();
            Debug.Log("Hello 3 " + request.responseCode);
        }

    }

    IEnumerator downloadPlayerData(string url)
    {
        var request = new UnityWebRequest(url, "GET");
        byte[] bodyRaw = Encoding.UTF8.GetBytes("");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweGYzOGY5OWMwYzQ5RDUwNzM2NTA1NjA4ZjY2M0FhYzVBZGJmRWNkMDkiLCJpc3MiOiJuZnQtc3RvcmFnZSIsImlhdCI6MTY2MzM4Nzc2OTkxNywibmFtZSI6IkFsaWVuYS0yNTUifQ.zQrushw1sgZ1P56NvGkbDGJaafCL-PIAeCzXNKFSUbs");

        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {
            dynamic data = JObject.Parse(request.downloadHandler.text);

            string visitedPlanetResult = data.visitedPlanet;

            for (int i = 0; i < visitedPlanetResult.Length; i++)
            {
                if (visitedPlanetResult[i] == '1')
                    visitedPlanet[i] = true;
                else
                    visitedPlanet[i] = false;
            }

            messageText.text = "Player data is up to date";

        }
        else
            messageText.text = "No data found!";


    }

    public void uploadPlayerData()
    {
        messageText.text = "Uploading Player Data";
        StartCoroutine(UploadPlayerDataDebug());
        return;

        string visitedPlanetString = "";
        int playerPoint = 0;

        for(int i=0; i<visitedPlanet.Length; i++)
        {
            string val = "0";

            if (visitedPlanet[i])
            {
                val = "1";
                playerPoint += 10;
            }

            visitedPlanetString += val;
        }

        var myData = new
        {
            walletAddress = AuthController.walletAddress,
            visitedPlanet = visitedPlanetString,
            points = playerPoint,
        };

        string data = JsonConvert.SerializeObject(myData);

        messageText.text = "Uploading Player Data";


        StartCoroutine(makeUploadRequest("https://api.nft.storage/upload", data));

    }

    IEnumerator makeUploadRequest(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweGYzOGY5OWMwYzQ5RDUwNzM2NTA1NjA4ZjY2M0FhYzVBZGJmRWNkMDkiLCJpc3MiOiJuZnQtc3RvcmFnZSIsImlhdCI6MTY2MzM4Nzc2OTkxNywibmFtZSI6IkFsaWVuYS0yNTUifQ.zQrushw1sgZ1P56NvGkbDGJaafCL-PIAeCzXNKFSUbs");

        yield return request.SendWebRequest();

        if (request.responseCode == 200)
            messageText.text = "Uploaded";
        else
            messageText.text = "Something went wrong! Error Code:" + request.responseCode.ToString();


    }

    public void makeReq(string URL, bool isGetMintStatusAction)
    {
        if (isGetMintStatusAction)
            messageText.text = "Getting Mint Status...";
        else
            messageText.text = "Minting...";

        StartCoroutine(Post(URL, isGetMintStatusAction));
    }


    IEnumerator Post(string url, bool isGetMintStatusAction)
    {
        var request = new UnityWebRequest(url, "GET");
        byte[] bodyRaw = Encoding.UTF8.GetBytes("");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if(isGetMintStatusAction)
        {
            if (request.responseCode == 200)
            {
                if (request.downloadHandler.text.ToLower().Contains("true"))
                    messageText.text = "Already Minted";
                else
                    messageText.text = "Not Minted";

            }
            else
                messageText.text = "Something went wrong!";
        }
        else
        {
            if (request.responseCode == 200)
                messageText.text = "Minted";
            else
                messageText.text = "Something went wrong or Already Minted";
        }

    }

    public void onClickGetNFT()
    {
        messageText.text = "Getting Minted Status...";

        StartCoroutine(ChangeMintedStatusText());
      
        return;
        isClickGetNFT = !isClickGetNFT;
    }

    IEnumerator Minting()
    {

        yield return new WaitForSeconds(5);

        messageText.text = "Minted";


    }

    IEnumerator ChangeMintedStatusText()
    {
        
        yield return new WaitForSeconds(5);
        if(isClickGetMintedStatus)
        {
            messageText.text = "Already Minted";
            
        }
        else
        {
            messageText.text = "Not Minted";
        }
        isClickGetMintedStatus = !isClickGetMintedStatus;


    }

    public void GetNFTS(PlanetModel model)
    {
        if (!isClickGetNFT) return;
        isClickGetNFT = false;

        makeReq("https://metaspace.up.railway.app/api/get-mint-status?name=" + model.name, true);
    }
    

    public void LateUpdate()
    {
        updateMyLocation();
    }

    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.2f);
        Vector3 finalPos = transform.position;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
            isMove = true;
        else
            isMove = false;

    }

    private float GetSpeedRotation() {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = needleSpeed / needleSpeedMax;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

    private void setNeedleMax()
    {
        if (isMove) needleSpeed = rnd.Next(130,140);
        else needleSpeed = rnd.Next(70, 80);
    }

    public void setShowInfoButton()
    {
        isShowInfoButtonClick = !isShowInfoButtonClick;

        if (isShowInfoButtonClick)
            showInfoButtonText.text = "Hide Info";
        else
            showInfoButtonText.text = "Show Info";
    }

    public void updateMyLocation()
    {
        if (playerLocationinOrbit == 1)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -11.8f, 0.0f);
            NearbyPlanet.text = "MERCURY";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[0].transform.position, transform.position);
        }
            
        else if (playerLocationinOrbit == 2)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -21.5f, 0.0f);
            NearbyPlanet.text = "VENUS";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[1].transform.position, transform.position);
        }
            
        else if (playerLocationinOrbit == 3)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -31.2f, 0.0f);
            NearbyPlanet.text = "EARTH";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[2].transform.position, transform.position);
        }
            
        else if (playerLocationinOrbit == 4)
        {
             MyLocation.transform.localPosition = new Vector3(-6.06f, -42.5f, 0.0f);
             NearbyPlanet.text = "MARS";
             DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[3].transform.position, transform.position);
        }
           
        else if (playerLocationinOrbit == 5)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -54.1f, 0.0f);
            NearbyPlanet.text = "JUPITER";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[4].transform.position, transform.position);
        }
            
        else if (playerLocationinOrbit == 6)
        {
             MyLocation.transform.localPosition = new Vector3(-6.06f, -65.8f, 0.0f);
             NearbyPlanet.text = "SATURN";
             DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[5].transform.position, transform.position);
        }
           
        else if (playerLocationinOrbit == 7)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -82.2f, 0.0f);
            NearbyPlanet.text = "URANUS";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[6].transform.position, transform.position);
        }
            
        else if (playerLocationinOrbit == 8)
        {
            MyLocation.transform.localPosition = new Vector3(-6.06f, -95.6f, 0.0f);
            NearbyPlanet.text = "NEPTUNE";
            DistanceOfNearbyPlanet = (int) Vector3.Distance(planets[7].transform.position, transform.position);
        }

        DisplayDistance.text = DistanceOfNearbyPlanet.ToString();

    }

    public void countOrbit()
    {
        float distance = Vector3.Distance(sunSpeher.transform.position, transform.position);

        if (distance <= 155.0f)
            playerLocationinOrbit = 1;
        else if (distance <= 180.0f)
            playerLocationinOrbit = 2;
        else if (distance <= 210.0f)
            playerLocationinOrbit = 3;
        else if (distance <= 261.0f)
            playerLocationinOrbit = 4;
        else if (distance <= 341.0f)
            playerLocationinOrbit = 5;
        else if (distance <= 419.0f)
            playerLocationinOrbit = 6;
        else if (distance <= 474.0f)
            playerLocationinOrbit = 7;
        else if (distance <= 524.0f)
            playerLocationinOrbit = 8;
        else
            playerLocationinOrbit = 9;
    }


    
    
}