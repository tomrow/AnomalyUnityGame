using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AlanAnimator2D : MonoBehaviour
{
    public float targetFPS = 60;
    public float animationMultiplier;

    //Bones
    public Transform pNeck;
    public Transform PSpine;
    public Transform pLeftShoulder;
    public Transform pLeftElbow;
    public Transform pRightShoulder;
    public Transform pRightElbow;
    public Transform pRightHip;
    public Transform pRightKnee;
    public Transform pRightAnkle;
    public Transform pLeftHip;
    public Transform pLeftKnee;
    public Transform pLeftAnkle;
    public Transform pRightWrist;
    public Transform pLeftWrist;
    public Transform PPelvis;

    //Visible
    public Transform PHead;
    public Transform pLeftUpperArm;
    public Transform pRightUpperArm;
    public Transform pLeftLowerArm;
    public Transform pRightLowerArm;
    public Transform pLeftHand;
    public Transform pRightHand;
    public Transform PWaist;
    public Transform pLeftUpperLeg;
    public Transform pLeftLowerLeg;
    public Transform pLeftFoot;
    public Transform pRightUpperLeg;
    public Transform pRightLowerLeg;
    public Transform pRightFoot;

    //Both
    public Transform pTorso;
    public Transform pBody;


    public float runAnimCounter;
    private float runAnimRadians;
    private float runAnimCounterAlt;
    private float runAnimRadiansAlt;
    public float incrementBy;
    //private int WaitInc;
    public GameObject thePlayer;
    public int animationID = 1;
    int animationIDOld;
    public float animSpeedModifierFpsAffected;
    public float vSpeed;

    //public Control playerScript = thePlayer.GetComponent<Control>();

    //public static float Direction;
    //public static float StickCenterDist;
    //public static float HorizontalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Alan");
        
        //_______________________________________Assign controls to body parts
        pBody = transform.Find("body");
        pTorso = pBody.Find("torso");

        pNeck = pTorso.Find("neck");
            PHead = pNeck.Find("head");

        PSpine = pTorso.Find("spine");
            pLeftShoulder = PSpine.Find("left_shoulder");
                pLeftElbow = pLeftShoulder.Find("left_elbow");
                    pLeftWrist = pLeftElbow.Find("left wrist");
            pRightShoulder = PSpine.Find("right_shoulder");
                pRightElbow = pRightShoulder.Find("right_elbow");
                    pRightWrist = pRightElbow.Find("right wrist");


        PPelvis = pTorso.Find("pelvis");
            pLeftHip = PPelvis.Find("left hip");
                pLeftUpperLeg = pLeftHip.Find("upperleg");
                pLeftKnee = pLeftHip.Find("left knee");
                    pLeftLowerLeg = pLeftKnee.Find("lowerleg");
                    pLeftAnkle = pLeftKnee.Find("left ankle");
                        pLeftFoot = pLeftAnkle.Find("foot");
            pRightHip = PPelvis.Find("right hip");
                pRightUpperLeg = pRightHip.Find("upperleg 1");
                pRightKnee = pRightHip.Find("right knee");
                    pRightLowerLeg = pRightKnee.Find("lowerleg 1");
                    pRightAnkle = pRightKnee.Find("right ankle");
                        pRightFoot = pRightAnkle.Find("foot 1");
            PWaist = PPelvis.Find("waist");

        pLeftUpperArm = pLeftShoulder.Find("upperarm");
        pRightUpperArm = pRightShoulder.Find("upperarm 1");
        pLeftHand = pLeftWrist.Find("fist");
        pRightHand = pRightWrist.Find("fist 1");
        pLeftLowerArm = pLeftElbow.Find("lowerarm");
        pRightLowerArm = pRightElbow.Find("lowerarm 1");
        

        //___________________________________________________________Tweak part offsets so the character looks humanoid and not a jumble of parts at the origin of the object
        pBody.Translate(0, 0, 4.7f);
        pNeck.Translate(0, 0, 1.5f);
        PSpine.Translate(0, 0, 0.7f); //put f after decimals or incur the wrath of c# angry you gave it doubles instead of floats

        pLeftShoulder.Translate(1.4f, 0, 0);
        pRightShoulder.Translate(-1.4f, 0, 0);
        pLeftUpperArm.Translate(0, 0, -0.5f);
        pRightUpperArm.Translate(0, 0, 0.5f);

        pLeftElbow.Translate(1.4f, 0, 0);
        pRightElbow.Translate(-1.4f, 0, 0);
        pLeftLowerArm.Translate(0, 0, -0.5f);
        pRightLowerArm.Translate(0, 0, 0.5f);

        pLeftWrist.Translate(1.3f, 0, 0);
        pRightWrist.Translate(-1.3f, 0, 0);
        pLeftHand.Translate(0, 0, 0.3f);
        pRightHand.Translate(0, 0, -0.3f);

        PHead.Translate(0, 0.3f, 1.5f);
        PPelvis.Translate(0, 0, -2);
        pLeftHip.Translate(0.7f, 0, -0.3f);
        pRightHip.Translate(-0.7f, 0, -0.3f);
        pLeftKnee.Translate(00, 0, -1.4f);
        pRightKnee.Translate(0, 0, -1.4f);
        pLeftAnkle.Translate(00, 0, -1.4f);
        pRightAnkle.Translate(0, 0, -1.4f);


        pLeftFoot.Translate(00, 0, -1.1f);
        pRightFoot.Translate(00, 0, -1.1f);
        pLeftUpperLeg.Translate(00, 0, -1.1f);
        pRightUpperLeg.Translate(00, 0, -1.1f);
        pLeftLowerLeg.Translate(00, 0, -1.1f);
        pRightLowerLeg.Translate(00, 0, -1.1f);
        incrementBy = 0.0f;
        //WaitInc = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float targetFrameTime = Convert.ToSingle(1 / targetFPS);
        animSpeedModifierFpsAffected = Convert.ToSingle(targetFrameTime * Time.fixedDeltaTime);
        //animationIDOld = animationID;
        animationID = thePlayer.GetComponent<PlayerMovement2D>().playerActionMode;
        vSpeed = thePlayer.GetComponent<PlayerMovement2D>().vspeed;
        animationID += 0;
        //Debug.Log(Time.fixedDeltaTime);
        //pNeck.localPosition = new Vector3(0, 0, 0);
        //if (WaitInc > 600) { incrementBy += 0.000002f; }
        //else { WaitInc++; }
        incrementBy = Convert.ToSingle(Math.Floor(thePlayer.GetComponent<PlayerMovement2D>().stickPushedFromCenter * animationMultiplier) * Time.fixedDeltaTime)*100; //* animSpeedModifierFpsAffected; //For run animation
        //*(incrementBy*1.4)
        if(animationID != animationIDOld) //If animation changes since last frame
        { 
            runAnimCounter = 0f;
            Debug.Log("Animation ID did not match last tick");
            pNeck.localRotation = Quaternion.Euler(0, 0, 0); //reset neck angle
        }    //Start Over when Animation changes


        //pLeftWrist.Rotate(0, 0.2f, 0);
        //pLeftElbow.Rotate(0, 0.2f, 0);





        //__________________________________________These animations are mostly made using trigonometric functions to simulate smooth start and stop. I just kinda threw things at a wall to see what stuck here, so that's why there's little to no comments.
        if (animationID == 0)  //run animation
        {
            animationIDOld = 0;
            if (incrementBy != 0f)
            {
                runAnimCounter = runAnimCounter + incrementBy;
                runAnimCounterAlt = runAnimCounter + 180;
                runAnimCounter = runAnimCounter % 360;
                runAnimCounterAlt = runAnimCounterAlt % 360;
                runAnimRadians = Convert.ToSingle(Math.PI * (runAnimCounter / 90));
                runAnimRadiansAlt = Convert.ToSingle(Math.PI * (runAnimCounterAlt / 90));

                if (incrementBy > 0.5) //sprint
                {
                    pRightShoulder.localRotation = Quaternion.Euler(0, -45, Convert.ToSingle(Math.Sin(runAnimRadians) * 80));
                    pLeftShoulder.localRotation = Quaternion.Euler(0, 45, Convert.ToSingle(Math.Sin(runAnimRadians) * 80));
                    pRightElbow.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(runAnimRadians) * 20) - 20));
                    pLeftElbow.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(runAnimRadians) * 20) + 20));

                    pTorso.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(runAnimRadians) * 30);
                    PPelvis.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(runAnimRadians) * -30);
                    PWaist.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(runAnimRadians) * 30);
                    pNeck.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(runAnimRadians) * -30);

                    pLeftHip.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Sin(runAnimRadians) * 80), 0, 0);
                    pRightHip.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Sin(runAnimRadians) * 80), 0, 0);
                    pRightKnee.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Cos(runAnimRadians) * 60) - 40f, 0, 0);
                    pLeftKnee.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Cos(runAnimRadians) * 60) - 40f, 0, 0);
                    pTorso.localPosition = new Vector3(0, 0, Convert.ToSingle(Math.Abs(Math.Sin(runAnimRadians)) / 3));
                }
                else //very bad walk animation
                {
                    pRightShoulder.localRotation = Quaternion.Euler(0, -60, Convert.ToSingle(Math.Sin(runAnimRadians) * 40));
                    pLeftShoulder.localRotation = Quaternion.Euler(0, 60, Convert.ToSingle(Math.Sin(runAnimRadians) * 40));
                    pRightElbow.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(runAnimRadians) * 10) - 20));
                    pLeftElbow.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(runAnimRadians) * 10) + 20));


                    pLeftHip.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Sin(runAnimRadians) * 40), 0, 0);
                    pRightHip.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Sin(runAnimRadians) * 40), 0, 0);
                    pRightKnee.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Cos(runAnimRadians) * 30) - 40f, 0, 0);
                    pLeftKnee.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Cos(runAnimRadians) * 30) - 40f, 0, 0);
                    pTorso.localPosition = new Vector3(0, 0, Convert.ToSingle(Math.Abs(Math.Sin(runAnimRadians)) / 6));
                }
            }
            else //stand still
            {
                pRightShoulder.localRotation = Quaternion.Euler(0, -80, 00);
                pLeftShoulder.localRotation = Quaternion.Euler(0, 80, 0);
                pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);
                runAnimCounter = 15;
                pTorso.localRotation = Quaternion.Euler(0, 0, 0);
                PPelvis.localRotation = Quaternion.Euler(0, 0, 0);
                PWaist.localRotation = Quaternion.Euler(0, 0, 0);
                pNeck.localRotation = Quaternion.Euler(0, 0, 0);

                pLeftHip.localRotation = Quaternion.Euler(0, 0, 0);
                pRightHip.localRotation = Quaternion.Euler(0, 0, 0);
                pRightKnee.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftKnee.localRotation = Quaternion.Euler(0, 0, 0);
                pTorso.localPosition = new Vector3(0, 0, 0);
            }
        }
        else if (animationID == 5) //freefall animation
        {
            if (vSpeed < 0)
            {
                runAnimCounter += 0.004f * animationMultiplier;
                pRightShoulder.localRotation = Quaternion.Euler(0, (-80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) + 70 - 5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10), 00);
                pLeftShoulder.localRotation = Quaternion.Euler(0, 5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) - 70, 0);
                pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);

                pLeftHip.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100, 0, 0);
                pRightHip.localRotation = Quaternion.Euler(-5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pRightKnee.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pLeftKnee.localRotation = Quaternion.Euler(0 - (5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100), 0, 0);
                pNeck.localRotation = Quaternion.Euler(30 * Mathf.Pow(runAnimCounter + 1f, -1) - 31, 0, 0);

                pTorso.localPosition = new Vector3(0, 0, 0);
                animationIDOld = 5;
            }
            else 
            {
                runAnimCounter += 0.04f * animationMultiplier;
                pRightShoulder.localRotation = Quaternion.Euler(0, -80, 00);
                pLeftShoulder.localRotation = Quaternion.Euler(0, 80, 0);
                pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);

                pLeftHip.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100, 0, 0);
                pRightHip.localRotation = Quaternion.Euler(-5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pRightKnee.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pLeftKnee.localRotation = Quaternion.Euler(0 - (5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100), 0, 0);
                pNeck.localRotation = Quaternion.Euler(0, 0, 0);

                pTorso.localPosition = new Vector3(0, 0, 0);
                animationIDOld = 5;
            }

        }
        else if (animationID == 7) //secret dance animation
        {
            runAnimCounter += 0.04f * animationMultiplier;
            pRightShoulder.localRotation = Quaternion.Euler(0, 8 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) + 70, 00);
            pLeftShoulder.localRotation = Quaternion.Euler(0, 8 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) - 70, 0);
            pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
            pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);


            pLeftHip.localRotation = Quaternion.Euler(0, 0, 0);
            pRightHip.localRotation = Quaternion.Euler(0, 0, 0);
            pRightKnee.localRotation = Quaternion.Euler(0, 0, 0);
            pLeftKnee.localRotation = Quaternion.Euler(0, 0, 0);
            pTorso.localPosition = new Vector3(0, 0, 0);
            animationIDOld = 7;
        }
        else if (animationID == 8) //jump windup
        {
            pTorso.localRotation = Quaternion.Euler(0, 0, 0);
            PPelvis.localRotation = Quaternion.Euler(0, 0, 0);
            PWaist.localRotation = Quaternion.Euler(0, 0, 0);
            pNeck.localRotation = Quaternion.Euler(0, 0, 0);
            runAnimCounter -= 1f * animationMultiplier;
            pRightElbow.localRotation = Quaternion.Euler(0, -60- (runAnimCounter * 3), 00);
            pLeftElbow.localRotation = Quaternion.Euler(0, 60+ (runAnimCounter * 3), 0);
            pRightShoulder.localRotation = Quaternion.Euler(0, 60 + (runAnimCounter * 3), 0);
            pLeftShoulder.localRotation = Quaternion.Euler(0, -60 - (runAnimCounter * 3), 0);


            pLeftHip.localRotation = Quaternion.Euler(70 + runAnimCounter, 0, 0);
            pRightHip.localRotation = Quaternion.Euler(70 + runAnimCounter, 0, 0);
            pRightKnee.localRotation = Quaternion.Euler(-70 - runAnimCounter, 0, 0);
            pLeftKnee.localRotation = Quaternion.Euler(-70 - runAnimCounter, 0, 0);
            pTorso.localPosition = new Vector3(0, 0, -0.2f-(runAnimCounter/300));
            animationIDOld = 8;
        }
        else if (animationID == 9)
        {
            if (vSpeed < 0)
            {
                runAnimCounter += 0.04f * animationMultiplier;
                pRightShoulder.localRotation = Quaternion.Euler(0, (-80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) + 70 - 5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10), 00);
                pLeftShoulder.localRotation = Quaternion.Euler(0, 5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (80 * (Mathf.Pow(runAnimCounter + 0.5f, -1f))) - 70, 0);
                pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);

                pLeftHip.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100, 0, 0);
                pRightHip.localRotation = Quaternion.Euler(-5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pRightKnee.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pLeftKnee.localRotation = Quaternion.Euler(0 - (5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100), 0, 0);
                pNeck.localRotation = Quaternion.Euler(30 * Mathf.Pow(runAnimCounter + 1f, -1) - 31, 0, 0);

                pTorso.localPosition = new Vector3(0, 0, 0);
                animationIDOld = 9;
            }
            else
            {
                runAnimCounter += 0.004f * animationMultiplier;
                pRightShoulder.localRotation = Quaternion.Euler(0, -80, 00);
                pLeftShoulder.localRotation = Quaternion.Euler(0, 80, 0);
                pRightElbow.localRotation = Quaternion.Euler(0, 0, 0);
                pLeftElbow.localRotation = Quaternion.Euler(0, 0, 0);

                pLeftHip.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100, 0, 0);
                pRightHip.localRotation = Quaternion.Euler(-5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pRightKnee.localRotation = Quaternion.Euler(5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) - 20, 0, 20);
                pLeftKnee.localRotation = Quaternion.Euler(0 - (5 * Mathf.Sin((runAnimCounter * Mathf.Rad2Deg) / 10) + (-60 * (Mathf.Pow((runAnimCounter * 2) + 0.5f, -1f))) + 100), 0, 0);
                pNeck.localRotation = Quaternion.Euler(30 * Mathf.Pow(runAnimCounter + 1f, -1) , 0, 0);

                pTorso.localPosition = new Vector3(0, 0, 0);
                animationIDOld = 9;
            }

        }
        //pLeftKnee.Rotate(0.2f, 0, 0);
    }
}
