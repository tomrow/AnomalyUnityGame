using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Control3 : MonoBehaviour
{
    public float StickX;
    public float StickY;
    public float Direction;
    public float StickCenterDist;
    public float HorizontalSpeed;
    public float VerticalSpeed;
    public bool CollidingWithGround;

    RaycastHit hit;
    public float todegrees = Convert.ToSingle(180 / Math.PI);
    // Start is called before the first frame update





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //------------------------GET INPUT
        StickX = Input.GetAxis("Horizontal");
        StickY = 0 - Input.GetAxis("Vertical");
        StickCenterDist = Math.Abs(StickX) * Math.Abs(StickX);
        StickCenterDist += Math.Abs(StickY) * Math.Abs(StickY);
        StickCenterDist = Convert.ToSingle(Math.Sqrt(StickCenterDist));


        if (StickCenterDist > 1f)
        {
            StickCenterDist = 1f;
        }

        if (StickCenterDist > HorizontalSpeed)
        {
            HorizontalSpeed = HorizontalSpeed + (StickCenterDist / 12);
        }

        if (StickCenterDist < HorizontalSpeed)
        {
            HorizontalSpeed = Convert.ToSingle(HorizontalSpeed * 0.8);
        }

        if (HorizontalSpeed < 0.1f)
        {
            //HorizontalSpeed = 0f;
        }
        else
        {
            transform.Translate(0, HorizontalSpeed / 7, 0);
            Console.WriteLine("Inputs");
            Console.WriteLine(StickX);
            Console.WriteLine(StickY);
            Console.WriteLine(HorizontalSpeed);
        }



        //----------------------------END GET INPUT



        //----------------------------PHYSICS AND CONTROL
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -7)), Color.blue);
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out hit, 7, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -7)), Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -7)), Color.red);
        }








    }
}