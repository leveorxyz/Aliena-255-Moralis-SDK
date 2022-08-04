using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FusedVR.Web3;

public class tempScript : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async public void toggleVisibilityAsync()
    {
        if(await Web3Manager.Login("femini9405@runfons.com", ""))
        {
            obj.SetActive(false);
           
        }

    }
}
