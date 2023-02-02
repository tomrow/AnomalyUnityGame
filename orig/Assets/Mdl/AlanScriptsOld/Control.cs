using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Control : MonoBehaviour
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StickX= Input.GetAxis("Horizontal");
        StickY = 0-Input.GetAxis("Vertical");
        StickCenterDist = (Math.Abs(StickX) + Math.Abs(StickY)) / 2;
        if (StickCenterDist > 0.5f) { StickCenterDist = 0.5f; }
        if (StickCenterDist > HorizontalSpeed)
        {
            HorizontalSpeed = HorizontalSpeed + (StickCenterDist / 12);

        }
        if (StickCenterDist < HorizontalSpeed)
        {
            HorizontalSpeed = Convert.ToSingle(HorizontalSpeed*0.8);
            


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




        Direction = 0;
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
            Direction += Convert.ToSingle(Math.Atan(StickX/StickY));
        }

        if (StickCenterDist > 0.1f)
        {
            //transform.localRotation = Quaternion.Euler(270, Camera.main.transform.localEulerAngles.y - todegrees * Direction, 0);
        }
        Debug.DrawRay(transform.position + new Vector3(1, 1, 1), transform.TransformDirection(new Vector3(0, 0, -10)), Color.green);
        if (CollidingWithGround)
        {
            //Quaternion.LookRotation(collision.contacts[0].normal));
            Debug.Log("TOUCH");
            //transform.localRotation = Quaternion.Euler(270, Camera.main.transform.localEulerAngles.y - todegrees * Direction, 0);
            //transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z) * Quaternion.LookRotation(collision.contacts[0].normal); 
            if (Physics.Raycast(transform.position , transform.TransformDirection(new Vector3(0, 0, -10)), out hit, 7, 1))
            {
                Debug.Log("RAY HIT");
                Debug.DrawRay(transform.position , transform.TransformDirection(new Vector3(0, 0, -10)), Color.red);
                if (StickCenterDist > 0.1f) { transform.localRotation = Quaternion.Euler(270, Camera.main.transform.localEulerAngles.y - todegrees * Direction, 0); }

                Debug.Log(hit.normal);
                if (StickCenterDist > 0.1f)
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                    transform.Rotate(-90, 0, 0);
                }

            }
            else
            {
                Debug.Log("NO RAY HIT");
                Debug.DrawRay(transform.position , transform.TransformDirection(new Vector3(0, 0, -10)), Color.green);
                transform.localRotation = Quaternion.Euler(270, Camera.main.transform.localEulerAngles.y - todegrees * Direction, 0);
                transform.position += new Vector3(0, 0.1f, 0);
            }
        }
        else
        {
            transform.position += new Vector3(0, -0.1f, 0);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        CollidingWithGround = true;
    }
    void OnTriggerExit(Collider collision)
    {
        CollidingWithGround = false;
    }
}
