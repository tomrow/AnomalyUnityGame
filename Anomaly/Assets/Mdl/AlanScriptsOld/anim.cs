using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class anim : MonoBehaviour
{
    
    //Bones
    public GameObject PNeck;
    public GameObject PSpine;
    public GameObject PLeftShoulder;
    public GameObject PLeftElbow;
    public GameObject PRightShoulder;
    public GameObject PRightElbow;
    public GameObject PRightHip;
    public GameObject PRightKnee;
    public GameObject PRightAnkle;
    public GameObject PLeftHip;
    public GameObject PLeftKnee;
    public GameObject PLeftAnkle;
    public GameObject PRightWrist;
    public GameObject PLeftWrist;
    public GameObject PPelvis;

    //Visible
    public GameObject PHead;
    public GameObject PLeftUpperArm;
    public GameObject PRightUpperArm;
    public GameObject PLeftLowerArm;
    public GameObject PRightLowerArm;
    public GameObject PLeftHand;
    public GameObject PRightHand;
    public GameObject PWaist;
    public GameObject PLeftUpperLeg;
    public GameObject PLeftLowerLeg;
    public GameObject PLeftFoot;
    public GameObject PRightUpperLeg;
    public GameObject PRightLowerLeg;
    public GameObject PRightFoot;

    //Both
    public GameObject PTorso;
    public GameObject PBody;


    public float RunAnimCounter;
    private float RunAnimRadians;
    private float RunAnimCounterAlt;
    private float RunAnimRadiansAlt;
    public float IncrementBy;
    //private int WaitInc;
    public GameObject thePlayer;
    public int AnimationID = 1;


    //public Control playerScript = thePlayer.GetComponent<Control>();

    //public static float Direction;
    //public static float StickCenterDist;
    //public static float HorizontalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Alan");


        PBody = GameObject.Find("body");
        PTorso = GameObject.Find("torso");

        PNeck = GameObject.Find("neck");
        PSpine = GameObject.Find("spine");
        PLeftShoulder = GameObject.Find("left_shoulder");
        PLeftElbow = GameObject.Find("left_elbow");
        PRightShoulder = GameObject.Find("right_shoulder");
        PRightElbow = GameObject.Find("right_elbow");
        PRightHip = GameObject.Find("right hip");
        PRightKnee = GameObject.Find("right knee");
        PRightAnkle = GameObject.Find("right ankle");
        PLeftHip = GameObject.Find("left hip");
        PLeftKnee = GameObject.Find("left knee");
        PLeftAnkle = GameObject.Find("left ankle");
        PRightWrist = GameObject.Find("right wrist");
        PLeftWrist = GameObject.Find("left wrist");
        PPelvis = GameObject.Find("pelvis");

        PHead = GameObject.Find("head");
        PWaist = GameObject.Find("waist");
        PLeftUpperLeg = GameObject.Find("upperleg");
        PRightUpperLeg = GameObject.Find("upperleg 1");
        PLeftLowerLeg = GameObject.Find("lowerleg");
        PRightLowerLeg = GameObject.Find("lowerleg 1");
        PLeftUpperArm = GameObject.Find("upperarm");
        PRightUpperArm = GameObject.Find("upperarm 1");
        PLeftHand = GameObject.Find("fist");
        PRightHand = GameObject.Find("fist 1");
        PLeftLowerArm = GameObject.Find("lowerarm");
        PRightLowerArm = GameObject.Find("lowerarm 1");
        PLeftFoot = GameObject.Find("foot");
        PRightFoot = GameObject.Find("foot 1");

        PBody.transform.Translate(0, 0, 4.7f);
        PNeck.transform.Translate(0, 0, 1.5f);
        PSpine.transform.Translate(0, 0, 0.7f); //put f after decimals or incur the wrath of c# angry you gave it doubles instead of floats
        
        PLeftShoulder.transform.Translate(1.4f, 0, 0);
        PRightShoulder.transform.Translate(-1.4f, 0, 0);
        PLeftUpperArm.transform.Translate(0, 0, -0.5f);
        PRightUpperArm.transform.Translate(0, 0, 0.5f);

        PLeftElbow.transform.Translate(1.4f, 0, 0);
        PRightElbow.transform.Translate(-1.4f, 0, 0);
        PLeftLowerArm.transform.Translate(0, 0, -0.5f);
        PRightLowerArm.transform.Translate(0, 0, 0.5f);

        PLeftWrist.transform.Translate(1.3f, 0, 0);
        PRightWrist.transform.Translate(-1.3f, 0, 0);
        PLeftHand.transform.Translate(0, 0, 0.3f);
        PRightHand.transform.Translate(0, 0, -0.3f);

        PHead.transform.Translate(0, 0.3f, 1.5f);
        PPelvis.transform.Translate(0, 0, -2);
        PLeftHip.transform.Translate(0.7f, 0, -0.3f);
        PRightHip.transform.Translate(-0.7f, 0, -0.3f);
        PLeftKnee.transform.Translate(00, 0, -1.4f);
        PRightKnee.transform.Translate(0, 0, -1.4f);
        PLeftAnkle.transform.Translate(00, 0, -1.4f);
        PRightAnkle.transform.Translate(0, 0, -1.4f);


        PLeftFoot.transform.Translate(00, 0, -1.1f);
        PRightFoot.transform.Translate(00, 0, -1.1f);
        PLeftUpperLeg.transform.Translate(00, 0, -1.1f);
        PRightUpperLeg.transform.Translate(00, 0, -1.1f);
        PLeftLowerLeg.transform.Translate(00, 0, -1.1f);
        PRightLowerLeg.transform.Translate(00, 0, -1.1f);
        IncrementBy = 0.0f;
        //WaitInc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationID = 1;
        //PNeck.transform.localPosition = new Vector3(0, 0, 0);
        //if (WaitInc > 600) { IncrementBy += 0.000002f; }
        //else { WaitInc++; }
        IncrementBy = Convert.ToSingle(Math.Floor(thePlayer.GetComponent<Control>().StickCenterDist * 150)/100);
        //*(IncrementBy*1.4)
        RunAnimCounter = RunAnimCounter + IncrementBy;
        RunAnimCounterAlt = RunAnimCounter + 180;
        RunAnimCounter = RunAnimCounter % 360;
        RunAnimCounterAlt = RunAnimCounterAlt % 360;
        RunAnimRadians = Convert.ToSingle(Math.PI * ( RunAnimCounter / 90 ) ) ;
        RunAnimRadiansAlt = Convert.ToSingle(Math.PI * (RunAnimCounterAlt / 90));

        //PLeftWrist.transform.Rotate(0, 0.2f, 0);
        //PLeftElbow.transform.Rotate(0, 0.2f, 0);
        if (AnimationID == 1)
        {
            if (IncrementBy != 0f)
            {
                if (IncrementBy > 0.5)
                {
                    PRightShoulder.transform.localRotation = Quaternion.Euler(0, -45, Convert.ToSingle(Math.Sin(RunAnimRadians) * 80));
                    PLeftShoulder.transform.localRotation = Quaternion.Euler(0, 45, Convert.ToSingle(Math.Sin(RunAnimRadians) * 80));
                    PRightElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * 20) - 20));
                    PLeftElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * 20) + 20));


                    PLeftHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Sin(RunAnimRadians) * 80), 0, 0);
                    PRightHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Sin(RunAnimRadians) * 80), 0, 0);
                    PRightKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Cos(RunAnimRadians) * 60) - 40f, 0, 0);
                    PLeftKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Cos(RunAnimRadians) * 60) - 40f, 0, 0);
                    PTorso.transform.localPosition = new Vector3(0, 0, Convert.ToSingle(Math.Abs(Math.Sin(RunAnimRadians)) / 3));
                }
                else
                {
                    PRightShoulder.transform.localRotation = Quaternion.Euler(0, -60, Convert.ToSingle(Math.Sin(RunAnimRadians) * 40));
                    PLeftShoulder.transform.localRotation = Quaternion.Euler(0, 60, Convert.ToSingle(Math.Sin(RunAnimRadians) * 40));
                    PRightElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * 10) - 20));
                    PLeftElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * 10) + 20));


                    PLeftHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Sin(RunAnimRadians) * 40), 0, 0);
                    PRightHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Sin(RunAnimRadians) * 40), 0, 0);
                    PRightKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Cos(RunAnimRadians) * 30) - 40f, 0, 0);
                    PLeftKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Cos(RunAnimRadians) * 30) - 40f, 0, 0);
                    PTorso.transform.localPosition = new Vector3(0, 0, Convert.ToSingle(Math.Abs(Math.Sin(RunAnimRadians)) / 6));
                }
            }
            else
            {
                PRightShoulder.transform.localRotation = Quaternion.Euler(0, -80, 00);
                PLeftShoulder.transform.localRotation = Quaternion.Euler(0, 80, 0);
                PRightElbow.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PLeftElbow.transform.localRotation = Quaternion.Euler(0, 0, 0);
                RunAnimCounter = 15;

                PLeftHip.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PRightHip.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PRightKnee.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PLeftKnee.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PTorso.transform.localPosition = new Vector3(0, 0, 0);
            }
        }else if(AnimationID == 0)
        {
            if (IncrementBy != 0f)
            {
                    PRightShoulder.transform.localRotation = Quaternion.Euler(0, -45, Convert.ToSingle(Math.Sin(RunAnimRadians) * (80 * (IncrementBy * 1.4))));
                    PLeftShoulder.transform.localRotation = Quaternion.Euler(0, 45, Convert.ToSingle(Math.Sin(RunAnimRadians) * (80 * (IncrementBy * 1.4))));
                    PRightElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * (20 * (IncrementBy * 1.4))) - 20));
                    PLeftElbow.transform.localRotation = Quaternion.Euler(0, 0, (Convert.ToSingle(Math.Sin(RunAnimRadians) * (20 * (IncrementBy * 1.4))) + 20));


                    PLeftHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Sin(RunAnimRadians) * (80 * (IncrementBy * 1.4))), 0, 0);
                    PRightHip.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Sin(RunAnimRadians) * (80 * (IncrementBy * 1.4))), 0, 0);
                    PRightKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(Math.Cos(RunAnimRadians) * (60 * (IncrementBy * 1.4))) - 40f, 0, 0);
                    PLeftKnee.transform.localRotation = Quaternion.Euler(Convert.ToSingle(0 - Math.Cos(RunAnimRadians) * (60 * (IncrementBy * 1.4))) - 40f, 0, 0);
                    PTorso.transform.localPosition = new Vector3(0, 0, Convert.ToSingle(Math.Abs(Math.Sin(RunAnimRadians)) / 3));
            }
            else
            {
                PRightShoulder.transform.localRotation = Quaternion.Euler(0, -80, 00);
                PLeftShoulder.transform.localRotation = Quaternion.Euler(0, 80, 0);
                PRightElbow.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PLeftElbow.transform.localRotation = Quaternion.Euler(0, 0, 0);
                RunAnimCounter = 15;

                PLeftHip.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PRightHip.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PRightKnee.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PLeftKnee.transform.localRotation = Quaternion.Euler(0, 0, 0);
                PTorso.transform.localPosition = new Vector3(0, 0, 0);
            }
        }

        //PLeftKnee.transform.Rotate(0.2f, 0, 0);
    }
}
