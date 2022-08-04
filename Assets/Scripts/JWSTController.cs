using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWSTController : MonoBehaviour
{
    public int planetId;
    public string planetName;
    public string planetDescription;

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
}
