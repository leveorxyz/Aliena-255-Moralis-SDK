using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject eventManager;
    public GameObject[] infoPanels;

    public float minX, maxX, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        GameObject player = PhotonNetwork.Instantiate(PlayerPrefab.name, randomPosition, Quaternion.Euler(0, 270, 0));

        eventManager.GetComponent<OVRInputModule>().rayTransform = player.transform.GetChild(0).transform.GetChild(5);

        for(int i=0; i<infoPanels.Length; i++)
        {
            infoPanels[i].GetComponent<Canvas>().worldCamera = player.transform.GetChild(0).transform.GetChild(1).GetComponent<Camera>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
