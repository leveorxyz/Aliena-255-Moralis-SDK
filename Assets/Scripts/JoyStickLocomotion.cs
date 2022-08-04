using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoyStickLocomotion : MonoBehaviour
{
    public Rigidbody player;
    public float speed;

    PhotonView view;
    public GameObject playerView;

    void Start()
    {
        view = playerView.GetComponent<PhotonView>();
    }


    void Update()
    {
        if (!view.IsMine) return;

        var JoyStickRight = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch);
        var JoyStickLeft = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);

        player.position += speed * Time.deltaTime * (transform.right * JoyStickRight.x + transform.up * JoyStickLeft.y + transform.forward * JoyStickRight.y);

    }
}
