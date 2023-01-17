using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DogAnimScript : MonoBehaviour
{
    public bool dogRunning;
    public Transform dogParent;
    public Transform dogBody;
    public Transform dogTailJoint;
    public Transform dogNeck;
    public Transform dogSkull;
    public Transform dogLeftEarJoint;
    public Transform dogRightEarJoint;
    public Transform dogFrontLeftLeg;
    public Transform dogFrontRightLeg;
    public Transform dogRearRightLeg;
    public Transform dogRearLeftLeg;
    public Transform dogFrontLeftAnkle;
    public Transform dogFrontRightAnkle;
    public Transform dogRearRightAnkle;
    public Transform dogRearLeftAnkle;

    public Transform dogTail;
    public Transform dogLeftEar;
    public Transform dogRightEar;
    public Transform dogTorso;
    public Transform dogFrontLeftFoot;
    public Transform dogFrontRightFoot;
    public Transform dogRearRightFoot;
    public Transform dogRearLeftFoot;
    public Transform dogFrontLeftShin;
    public Transform dogFrontRightShin;
    public Transform dogRearRightShin;
    public Transform dogRearLeftShin;
    public Transform dogHead;

    float TailVal; //tail wag timer from 0 to 360
    float HeadVal; //head look timer from 0 to 360
    float legVal; //oops I didnt follow style guide for those two, too late to fix now
    float legAngle;
    

    // Start is called before the first frame update
    void Start()
    {
        //dogParent = transform;
        // Grab joints into Transform type variable objects
        dogBody = dogParent.Find("DogBody");
        dogTailJoint = dogBody.Find("tailJoint");
        dogNeck = dogBody.Find("DogNeckJoint");
        dogSkull = dogNeck.Find("DogSkullJoint");
        dogLeftEarJoint = dogSkull.Find("DogLeftEarJoint");
        dogRightEarJoint = dogSkull.Find("DogRightEarJoint");
        dogFrontLeftLeg = dogBody.Find("DogLeftFrontLegJoint");
        dogFrontRightLeg = dogBody.Find("DogRightFrontLegJoint");
        dogRearRightLeg = dogBody.Find("DogRightBackLegJoint");
        dogRearLeftLeg = dogBody.Find("DogLeftBackLegJoint");

        dogFrontLeftShin = dogFrontLeftLeg.Find("DogShinMesh");
        dogFrontRightShin = dogFrontRightLeg.Find("DogShinMesh");
        dogRearRightShin = dogRearRightLeg.Find("DogShinMesh");
        dogRearLeftShin = dogRearLeftLeg.Find("DogShinMesh");

        dogFrontLeftAnkle = dogFrontLeftLeg.Find("DogAnkleJoint");
        dogFrontRightAnkle = dogFrontRightLeg.Find("DogAnkleJoint");
        dogRearRightAnkle = dogRearRightLeg.Find("DogAnkleJoint");
        dogRearLeftAnkle = dogRearLeftLeg.Find("DogAnkleJoint");
        dogHead = dogSkull.Find("DogHeadMesh");
        //Dog head position offset
        dogBody.Translate(ScaleHack(0f), ScaleHack(0.5f), ScaleHack(0f));
        dogNeck.Translate(ScaleHack(2.6f), ScaleHack(0f), ScaleHack(0f));
        dogSkull.Translate(ScaleHack(0f), ScaleHack(2.8f), ScaleHack(0f));
        dogLeftEarJoint.Translate(ScaleHack(0f), ScaleHack(0.5f), ScaleHack(2.5f));
        dogRightEarJoint.Translate(ScaleHack(0f), ScaleHack(0.5f), ScaleHack(-2.5f));
        dogHead.Translate(ScaleHack(2f), ScaleHack(0), ScaleHack(0));

        //Dog shin position offset - put legs in the corners of the torso
        dogFrontLeftLeg.Translate(ScaleHack(2f), ScaleHack(-1.7f), ScaleHack(0.8f));
        dogFrontRightLeg.Translate(ScaleHack(2f), ScaleHack(-1.7f), ScaleHack(-0.8f));
        dogRearLeftLeg.Translate(ScaleHack(-2f), ScaleHack(-1.7f), ScaleHack(0.8f));
        dogRearRightLeg.Translate(ScaleHack(-2f), ScaleHack(-1.7f), ScaleHack(-0.8f));

        dogFrontLeftShin.Translate( ScaleHack(0),0, ScaleHack(-0.6f));
        dogFrontRightShin.Translate(ScaleHack(0),0, ScaleHack(-0.6f));
        dogRearLeftShin.Translate(ScaleHack(0),0, ScaleHack(-0.6f));
        dogRearRightShin.Translate(ScaleHack(0),0, ScaleHack(-0.6f));
        //dog paw position offset - put paws at the end of each leg
        dogFrontLeftAnkle.Translate(ScaleHack(0.3f), ScaleHack(-1.6f), ScaleHack(0f));
        dogFrontRightAnkle.Translate(ScaleHack(0.3f), ScaleHack(-1.6f), ScaleHack(0f));
        dogRearLeftAnkle.Translate(ScaleHack(0.3f), ScaleHack(-1.6f), ScaleHack(0f));
        dogRearRightAnkle.Translate(ScaleHack(0.3f), ScaleHack(-1.6f), ScaleHack(0f));

        dogTailJoint.Translate(ScaleHack(-3.2f), ScaleHack(0f), ScaleHack(0f));

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        
        TailVal += 15;
        TailVal = TailVal % 360;
        HeadVal += 1;
        HeadVal = HeadVal % 360;
        legVal += 9;
        legVal = legVal % 360;
        dogTailJoint.localRotation = Quaternion.Euler(0, Mathf.Sin(Mathf.Deg2Rad*TailVal)*30, 0);
        dogNeck.localRotation = Quaternion.Euler(0, Mathf.Sin(Mathf.Deg2Rad * HeadVal) * 30, 0);
        if (dogRunning)
        {
            legAngle = Mathf.Sin(legVal * Mathf.Deg2Rad) * 45;
            dogRearLeftLeg.localRotation = Quaternion.Euler(0, 0, legAngle);
            dogRearRightLeg.localRotation = Quaternion.Euler(0, 0, legAngle);
            dogFrontLeftLeg.localRotation = Quaternion.Euler(0, 0, 0 - legAngle);
            dogFrontRightLeg.localRotation = Quaternion.Euler(0, 0, 0 - legAngle);
            dogBody.localRotation = Quaternion.Euler(0, -90, (0 - Mathf.Cos(legVal * Mathf.Deg2Rad) * 45) / 4);
        }
        else
        {
            dogRearLeftLeg.localRotation = Quaternion.Euler(0,0,0);
            dogRearRightLeg.localRotation = Quaternion.Euler(0, 0, 0);
            dogFrontLeftLeg.localRotation = Quaternion.Euler(0, 0, 0);
            dogFrontRightLeg.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }
    float ScaleHack(float value)
    {
        //Measurements were originally trial/error'd at a scale 7.1804 (thanks to Autodesk),
        ////so as a quick hack to resize the dog and still have its body parts attached without changing hardcoded values (i'm aware it's bad but I can't do anything now), 
        ////I pass the values through this function to scale them according to the size the object is when it first appears.

        //force even scale:
        dogParent.localScale = new Vector3(dogParent.localScale.x, dogParent.localScale.x, dogParent.localScale.x);

        float spawnScale = (dogParent.localScale.x + dogParent.localScale.y + dogParent.localScale.z) / 3;
        float fixedposval = (value / 7.1804f) * spawnScale;
        Debug.Log("ScaleHack used on a dog object. spawnScale = " + spawnScale.ToString());
        return fixedposval;
    }
}
