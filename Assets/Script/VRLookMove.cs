using System;
using System.Collections;
using System.Text;
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
    Random rnd = new Random(DateTime.Now.Millisecond);

    public bool isClickMintNFT = false;
    public bool isClickGetNFT = false;

    void Start()
    {
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
       
      
    }

    public void onClickMintNFTButton()
    {
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
        isClickGetNFT = !isClickGetNFT;
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