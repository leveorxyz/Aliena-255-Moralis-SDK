using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int MoveSpeed = 2;
    public int MoveSpeedUpDown = 10;


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        float xAxisValue = Input.GetAxis("Horizontal") * MoveSpeed;
        float zAxisValue = Input.GetAxis("Vertical") * MoveSpeed;


        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);

        if (Input.GetKey(KeyCode.Q))
            transform.position += Vector3.up * MoveSpeedUpDown * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E))
            transform.position += -Vector3.up * MoveSpeedUpDown * Time.deltaTime;
    }
}
