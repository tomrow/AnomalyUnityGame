using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement2D : MonoBehaviour
{
    public Transform AlanAnimatorHeirarchy;
    public float walkSpeed =2f;
    public float runSpeed = 6f;
    public bool ultraMode;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    public float stickPushedFromCenter;
    public int playerActionMode = 0;
    //0 = walking
    //1 = jumping
    //2 = knockback
    //3 = punching
    //4 = dragging something
    //5 = falling
    //6 = death
    //7 = secret dance
    //8 = jump windup
    Transform cameraT;
    /*Vector2 inputDirOld;
    Vector2 input;
    Vector2 input2;
    Vector2 inputDir;*/

    public float StickX;
    public float StickY;
    public bool AnimatorFlip = false;

    RaycastHit touchRay;
    public float vspeed;
    public float hspeed;
    public int animSubID;
    public int jumpHesitationFrames;
    int hesitationCounter;
    public float yangle; 


    public Transform debugCube;
    public Transform debugCubeFront;
    public Transform debugCubeUp;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Start!!");
        cameraT = Camera.main.transform;
        debugCubeFront = debugCube.Find("front");
        debugCubeUp = debugCube.Find("up");

    }

    
    void MoveCharacterTic()
    {
        StickX = Input.GetAxis("Horizontal");
        hspeed += StickX;
        if (Mathf.Abs(StickX) < 0.1f)
        {
            if (hspeed > 0.8f)
            {
                hspeed -= 0.8f;
            }
            else if (hspeed < -0.8f)
            {
                hspeed += 0.8f;
            }
            else
            {
                hspeed = 0f;
            }
        }
        transform.Translate(transform.forward * hspeed * Time.fixedDeltaTime, Space.World);
        stickPushedFromCenter = hspeed;


    }
    private void CollideWallTic()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-7,0, 0)), Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0,-7, 0)), Color.green);
        //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, -7)), Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(7, 0, 0)), Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 7, 0)), Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0, 0, 7)), Color.blue);
        if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)), out touchRay, 2, 1))
        {
            Debug.Log("Ray was cast forward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.forward * -2, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out touchRay, 2, 1))
        {
            Debug.Log("Ray was cast backward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.forward * 2, Space.World);
        }
        /*if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out touchRay, 2, 1))
        {
            Debug.Log("Ray was cast right, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.right * -2, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out touchRay, 2, 1))
        {
            Debug.Log("Ray was cast left, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.right * 2, Space.World);
        }*/
    }
    private void CollideFloorPitchModTic()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 6, 1))
        {
            Debug.Log("Ray was cast downward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.up * 4.8f, Space.World);
            if (vspeed < 0) 
            { 
                vspeed = 0;
            }
            
        }
        else
        {
            Debug.Log("Ray was cast downward, and we got a MISS! Switching player to freefall state");
            playerActionMode = 5;
        }
        //pitch angle detection
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 7, 1))
        {
            //Debug.Log("touchRay.normal.x");
            //Debug.Log(touchRay.normal.x);
            //Debug.Log("touchRay.normal.y");
            //Debug.Log(touchRay.normal.y);
            //Debug.Log("touchRay.normal.z");
            //Debug.Log(touchRay.normal.z);
            Debug.Log(touchRay.normal);
            yangle = transform.localRotation.eulerAngles.y;
            transform.localRotation = Quaternion.Euler((touchRay.normal.z * Mathf.Rad2Deg), 0, 0-touchRay.normal.x * Mathf.Rad2Deg);
            debugCube.Rotate(0, yangle, 0);
            transform.LookAt(debugCubeFront, debugCubeUp.localPosition);
            //debugCube.localRotation = Quaternion.Euler((touchRay.normal.x * Mathf.Rad2Deg), (touchRay.normal.y * Mathf.Rad2Deg)-transform.localRotation.eulerAngles.y, touchRay.normal.z * Mathf.Rad2Deg);
            //debugCube.position = transform.position + new Vector3(touchRay.normal.x * 5, 0 - touchRay.normal.z * 5, touchRay.normal.y * 5);
            //AlanAnimatorHeirarchy.LookAt(debugCube); //point animator toward incline
            //AlanAnimatorHeirarchy.Rotate(-90, 180, 0); //reorientate so it's facing forward and not upward because 3ds max
            //AlanAnimatorHeirarchy.localRotation = Quaternion.Euler(AlanAnimatorHeirarchy.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, AlanAnimatorHeirarchy.localRotation.eulerAngles.z); //and turn toward movement direction
            //transform.localRotation = Quaternion.Euler(45-(touchRay.normal.x * Mathf.Rad2Deg), (touchRay.normal.y * Mathf.Rad2Deg), 0 - (touchRay.normal.z * Mathf.Rad2Deg));
        }
        else
        {
            Debug.Log("Ray was cast downward, and we got a MISS! Switching player to freefall state");
            playerActionMode = 5;
        }
    }
    private void CollideFloorFreeFallTic()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 5, 1))
        {
            Debug.Log("Ray was cast downward, and we got a hit! Switching back to running mode");
            playerActionMode = 0;
            transform.position = touchRay.point;
            transform.Translate(transform.up * 5f, Space.World);
            if (vspeed < 0)
            {
                vspeed = 0;
            }
            
        }
        else
        {
            vspeed -= 1.5f;
        }
        transform.Translate(new Vector3(0, vspeed, 0) * Time.fixedDeltaTime);

    }
    void MoveCharacterDuringFreeFallTic()
    {
        /*inputDirOld = input.normalized;
        //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputDir = input.normalized; //why we use this I don't know, when standard GetAxis already outputs in range -1 to 1
        //Aparently normalised removes analogue values and snaps to the furthest out spot in the direction the stick is pushed, maybe for easier math?
        //changing input to vector2 with standard getaxis for analog distance measurement
        stickPushedFromCenter = Mathf.Sqrt((input2.x * input2.x) + (input2.y * input2.y));
        stickPushedFromCenter = Mathf.Clamp01(stickPushedFromCenter);
        if (false)
        {
            if ((inputDir != Vector2.zero) && (inputDirOld == Vector2.zero))
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y; //I wish I knew about these angle things sooner
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0);
            }
            else if ((inputDir != Vector2.zero))
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y; //I wish I knew about these angle things sooner
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
        }
        else
        {
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y; //I wish I knew about these angle things sooner
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
        }


        //inputDirOld 
        bool running = (Input.GetAxis("Fire3") == 1f);  //I changed this from GetKey(KeyCode.LeftShift) so I can change the controls later from the input manager in project settings.
        //bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        speed = speed * stickPushedFromCenter;
        transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        */

    }








    private void JumpAbilityTic()
    {
        if(Input.GetAxis("Fire2") == 1f) 
        {
            vspeed = 70f;
            //transform.Translate(transform.up * 2 , Space.World);
            //this is no longer needed here as jumping has been moved
            playerActionMode = 8;
            hesitationCounter = 0;
        }
    }

    private void JumpHesitate()
    {
        if (hesitationCounter != jumpHesitationFrames)
        {
            hesitationCounter += 1;
        }
        else
        {
            transform.Translate(transform.up * 2, Space.World);
            playerActionMode = 9;
        }
    }

    private void JumpSwitchToFallAnimation()
    {
        if (vspeed < 0f)
        {
            playerActionMode = 5;
        }
    }





    private void FixedUpdate() // FixedUpdate is called once per 1/60s
    {
        if(playerActionMode==0)
        {
            //running
            
            CollideWallTic();
            MoveCharacterTic();
            CollideWallTic();
            JumpAbilityTic();
            CollideFloorPitchModTic();
        }
        else if(playerActionMode==1)
        {
            //jumping
        }
        else if (playerActionMode == 2)
        {
            //knockback
        }
        else if (playerActionMode == 3)
        {
            //punching
        }
        else if (playerActionMode == 4)
        {
            //dragging/holding
        }
        else if (playerActionMode == 5)
        {
            //falling
            MoveCharacterDuringFreeFallTic();
            CollideFloorFreeFallTic();
            CollideWallTic();
            
        }
        else if (playerActionMode == 6)
        {
            //death
        }
        else if (playerActionMode == 8)
        {
            //jump windup
            MoveCharacterTic();
            CollideWallTic();
            CollideFloorPitchModTic();
            JumpHesitate();

        }
        else if (playerActionMode == 9)
        {
            //jump upward animation hack
            MoveCharacterDuringFreeFallTic();
            CollideFloorFreeFallTic();
            CollideWallTic();
            JumpSwitchToFallAnimation();
        }


    }
}
