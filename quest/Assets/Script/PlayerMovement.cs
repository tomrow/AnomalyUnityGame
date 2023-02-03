using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
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
    Vector2 inputDirOld;
    Vector2 input;
    Vector2 input2;
    Vector2 inputDir;
    RaycastHit touchRay;
    public float vspeed;
    public float hspeed;
    public int animSubID;
    public int jumpHesitationFrames;
    int hesitationCounter;
    public float yangle;
    public bool onJumpRamp = false;

    public Transform debugCube;
    public Transform debugCubeFront;
    public Transform debugCubeUp;
    public Transform dropShadow;
    GameObject launchSoundObj;
    AudioSource launchSoundControl;
    MeshRenderer dropShadowGraphics;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Start!!");
        cameraT = Camera.main.transform;
        debugCube = transform.Find("Data");
        //debugCubeFront = debugCube.Find("front");
        //debugCubeUp = debugCube.Find("up");
        launchSoundObj = debugCube.Find("Sound").Find("LaunchSound").gameObject;
        launchSoundControl = launchSoundObj.GetComponent<AudioSource>();
        dropShadow = transform.Find("dropShadow");
        dropShadowGraphics = dropShadow.gameObject.GetComponent<MeshRenderer>();
    }

    
    void MoveCharacterTic()
    {
        inputDirOld = input.normalized;
        //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputDir = input.normalized; //why we use this I don't know, when standard GetAxis already outputs in range -1 to 1
        //Aparently normalised removes analogue values and snaps to the furthest out spot in the direction the stick is pushed, maybe for easier math?
        //changing input to vector2 with standard getaxis for analog distance measurement
        //Debug.Log(inputDir);
        //stickPushedFromCenter = Mathf.Clamp(Mathf.Sqrt((input2.x * input2.x) + (input2.y * input2.y)), 0, 1);
        stickPushedFromCenter = Mathf.Sqrt((input2.x * input2.x) + (input2.y * input2.y));
        stickPushedFromCenter = Mathf.Clamp01(stickPushedFromCenter);
        if (ultraMode)
        {
            if ((inputDir != Vector2.zero)&& (inputDirOld == Vector2.zero))
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y; //I wish I knew about these angle things sooner
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0);
            }else if((inputDir != Vector2.zero))
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
        float speed = ((running) ? runSpeed : runSpeed) * inputDir.magnitude;
        speed = speed * stickPushedFromCenter;
        transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);

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
            //Debug.Log("Ray was cast forward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.forward * -2, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 0, -1)), out touchRay, 2, 1))
        {
            //Debug.Log("Ray was cast backward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.forward * 2, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out touchRay, 2, 1))
        {
            //Debug.Log("Ray was cast right, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.right * -2, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out touchRay, 2, 1))
        {
            //Debug.Log("Ray was cast left, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.right * 2, Space.World);
        }
    }
    private void CollideFloorPitchModTic()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 6, 1))
        {
            //Debug.Log("Ray was cast downward, and we got a hit!");
            transform.position = touchRay.point;
            transform.Translate(transform.up * 4.8f, Space.World);
            if (vspeed < 0)
            {
                vspeed = 0;
            }
            if (touchRay.collider.gameObject.tag == "Respawn")
            {
                gameObject.GetComponent<GameStateVariables>().health = 0;
            }



        }
        else
        {
            //Debug.Log("Ray was cast downward, and we got a MISS! Switching player to freefall state");
            playerActionMode = 5;
            if (onJumpRamp)
            {
                launchSoundControl.Play();
                //play ramp launch sound
            }

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
        //hspeed = 0f;

    }
    void MoveCharacterDuringFreeFallTic()
    {
        inputDirOld = input.normalized;
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
        float speed = ((running) ? runSpeed : runSpeed) * inputDir.magnitude;
        speed = speed * stickPushedFromCenter;
        transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        transform.Translate(transform.forward * hspeed * Time.fixedDeltaTime, Space.World);
        hspeed = hspeed * 0.98f ;


    }








    private void JumpAbilityTic()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 5, 1))
        {
            if (touchRay.collider.gameObject.tag == "JumpStorageBoost")
            {
                vspeed = 150;
                hspeed = 150;
                onJumpRamp = true;
            }
            else
            {
                if (vspeed < 0) 
                { 
                    vspeed = 0;
                    
                    
                }
                hspeed = 0;
                onJumpRamp = false;
            }
        }
        if (Input.GetAxis("Fire2") == 1f) 
        {
            vspeed = 70f;
            hspeed = 0f;

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
        //pitch angle detection
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), out touchRay, 9999, 1))
        {
            //enable plane renderer and move shadow transform to raycast hit point
            dropShadowGraphics.enabled = true;
            dropShadow.position = touchRay.point + Vector3.up * 0.1f;
        }
        else
        {
            //missed, maybe above a bottomless pit? whatever the ground is so far below we can hide the drop shadow
            dropShadowGraphics.enabled = false;
        }

    }


}
