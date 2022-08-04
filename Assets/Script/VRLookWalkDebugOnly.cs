using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using Photon.Pun;

public class VRLookWalkDebugOnly : MonoBehaviour
{
    public Transform camera;
    public Transform movingCamera;
    public int Speed = 50;
    public float moveSpeed = 10;


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    PhotonView view;
    public GameObject playerView;


    void Start()
    {
        view = playerView.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!view.IsMine) return;


        if (Input.GetMouseButton(0))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        float xAxisValue = Input.GetAxis("Vertical") * -Speed;
        float zAxisValue = Input.GetAxis("Horizontal") * Speed;

        movingCamera.transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);

        if (Input.GetKey(KeyCode.Q))
            movingCamera.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E))
            movingCamera.transform.position += -Vector3.up * moveSpeed * Time.deltaTime;
    }

 
}
