using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWSTController : MonoBehaviour
{
    public int planetId;
    public string planetName;
    public string planetDescription;

    public float minDistancefromPlayerToPlanet = 55.0f;

    public void OnClickMint()
    {
        PlanetModel model = new PlanetModel();
        model.planetId = planetId;
        model.name = planetName;
        model.description = planetDescription;

        GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().isClickMintNFT = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().mintNFT(model);

    }

    public void OnClickGetNFT()
    {
        PlanetModel model = new PlanetModel();
        model.planetId = planetId;
        model.name = planetName;
        model.description = planetDescription;

        GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().isClickGetNFT = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<VRLookMove>().GetNFTS(model);

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

        VRLookMove vRLookMove = GameObject.FindWithTag("Player").GetComponent<VRLookMove>();

        if (distance <= minDistancefromPlayerToPlanet + 5.0f)
            vRLookMove.visitedPlanet[8] = true;
    }
}
