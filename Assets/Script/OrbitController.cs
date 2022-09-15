using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Random2 = System.Random;


public class OrbitController : MonoBehaviour
{
    [SerializeField]
    public int planetId;
    [SerializeField]
    public string planetName;
    [SerializeField]
    public string planetDescription;
   

    // for backup
    public float ORBITXAXIS;
    public float ORBITYAXIS;

    // Control Planet on Orbit
    public Transform orbitingObject;
    public Ellipse orbitPath;
    [Range(0f, 1f)] 
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;
    public float stop = 1.0f;
    public float runTimeSpeed = 1.0f;

    // Rotate planet itself
    public float minSpinSpeed = 1f;
    public float maxSpinSpeed = 5f;
    private float spinSpeed;


    // Focused
    public float minDistancefromPlayerToPlanet = 55.0f; //35.0f;
    public GameObject info;
    public float infoX, infoY, infoZ;


    void Start()
    {
        spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);

        if (orbitingObject == null)
        {
            orbitActive = false;
           
        }
        else
        {
            SetOrbitingObjectPosition();
            StartCoroutine(AnimateOrbit());
            ORBITXAXIS = orbitPath.xAxis;
            ORBITYAXIS = orbitPath.yAxis;

        }
       
    }
    
    void Update()
    {
        if (stop == 0.0f && GameObject.FindWithTag("Player").GetComponent<VRLookMove>().isShowInfoButtonClick)
        {
            info.SetActive(true);
        }

        else
        {
            info.SetActive(false);
        }

        if(stop == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().isClickMintNFT)
        {
            PlanetModel model = new PlanetModel();
            model.planetId = planetId;
            model.name = planetName;
            model.description = planetDescription;

            GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().mintNFT(model);
        }

        if (stop == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().isClickGetNFT)
        {
            PlanetModel model = new PlanetModel();
            model.planetId = planetId;
            model.name = planetName;
            model.description = planetDescription;

            GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().GetNFTS(model);
        }
        
        info.transform.position = new Vector3(transform.position.x + infoX ,  transform.position.y+infoY, transform.position.z + infoZ);
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        focusPlanet();
    }


    public void focusPlanet()
    {
        if (stop == 0.0f)
        {
            float focusedDistance =
                Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

            if (focusedDistance <= minDistancefromPlayerToPlanet + 5.0f)
                return;
            else
                stop = 1.0f;


        }

        float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);
        int orbitNumber = GameObject.FindWithTag("Player").GetComponent<VRLookMove>().playerLocationinOrbit;

        VRLookMove vRLookMove = GameObject.FindWithTag("Player").GetComponent<VRLookMove>();

        if (distance <= minDistancefromPlayerToPlanet)
        {
            if (gameObject.tag == "mercury" && orbitNumber == 1)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[0] = true;
            }
            else if (gameObject.tag == "venus" && orbitNumber == 2)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[1] = true;
            }
            else if (gameObject.tag == "earth" && orbitNumber == 3)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[2] = true;
            }

            else if (gameObject.tag == "mars" && orbitNumber == 4)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[3] = true;
            }
            else if (gameObject.tag == "jupiter" && orbitNumber == 5)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[4] = true;
            }
            else if (gameObject.tag == "saturn" && orbitNumber == 6)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[5] = true;
            }
            else if (gameObject.tag == "uranus" && orbitNumber == 7)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[6] = true;
            }
            else if (gameObject.tag == "neptune" && orbitNumber == 8)
            {
                stop = 0.0f;
                vRLookMove.visitedPlanet[7] = true;
            }
            else stop = 1.0f;
        }
        else
            stop = 1.0f;

        

    }


    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }
    
    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }

        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed * stop * runTimeSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;

        }
    }
    
}
