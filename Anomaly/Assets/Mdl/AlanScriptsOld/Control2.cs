using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Control2 : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float StickX;
    public float StickY;
    public float Direction;
    public float StickCenterDist;
    public float HorizontalSpeed;
    private float todegrees = Convert.ToSingle(180 / Math.PI);


    private void Start()
    {
        controller = GetComponent<CharacterController>() as CharacterController;
    }

    void Update()
    {
        Direction = 0;
        StickX = Input.GetAxis("Horizontal");
        StickY = 0 - Input.GetAxis("Vertical");
        StickCenterDist = (Math.Abs(StickX) + Math.Abs(StickY)) / 2;
        if (StickCenterDist > 0.5f) { StickCenterDist = 0.5f; }
        if (StickY == 0)
        {
            if (StickX < 0)
            {
                Direction = Convert.ToSingle((Math.PI / 180) * 270);
            }
            else
            {
                Direction = Convert.ToSingle((Math.PI / 180) * 90);
            }

        }
        else
        {
            if (StickY < 0) { Direction = Convert.ToSingle((Math.PI / 180) * 180); }
            Direction += Convert.ToSingle(Math.Atan(StickX / StickY));
        }
        if (StickCenterDist > 0.2f)
        {
            transform.localRotation = Quaternion.Euler(270, Camera.main.transform.localEulerAngles.y - todegrees * Direction, 0);
        }
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.SimpleMove(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}