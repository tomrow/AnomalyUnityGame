using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool lockCursor; //bools undefined when declared are by default "false"
    public float mouseSensitivity = 10f;
    public Transform target;
    public float distanceFromTarget = 3f;

    public Vector2 pitchRange = new Vector2(-40, 80);
    //    public Vector2 pitchRange = new Vector2(-90, 90);
    //    ^Sonic mode
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float pitch = 0f;
    float yaw = 0f;

    public bool miningCameraRanges;
    public bool oldCamera;
    //OLD CAMERA
    public GameObject oldCamTarget;
    private float yrot;
    private float xrot;
    public float oldCamDistanceCalculation;
    public float oldCamDistanceFromTarget;
    public float oldCamDistanceMultiplier;
    public float oldCamPitchOffset;
    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CameraModeToggle"))
        {
            oldCamera = !oldCamera;
            
        }
        if (!oldCamera)
        {
            yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;// += operator means increment by
            pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;// -= operator means decrement by
            //Controller
            yaw += Input.GetAxis("RightStickX")/3;// += operator means increment by
            yaw += Input.GetAxis("Triggers")/2 ;// += operator means increment by
            pitch += Input.GetAxis("RightStickY")/3 ;// += operator means increment by
            pitch = Mathf.Clamp(pitch, pitchRange.x, pitchRange.y);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
            transform.position = target.position - (transform.forward * distanceFromTarget);
        }
        else
        {
        }

    }
    void FixedUpdate()
    {
        if (!oldCamera)
        {
        }
        else
        {
            oldCamDistanceCalculation = Vector3.Distance(transform.position, oldCamTarget.transform.position);
            // Spin the object around the target at 20 degrees/second.
            transform.LookAt(oldCamTarget.transform, Vector3.left);
            transform.RotateAround(oldCamTarget.transform.position, Vector3.up, 0);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            transform.Translate(Vector3.forward * (oldCamDistanceCalculation - (oldCamDistanceFromTarget * oldCamDistanceMultiplier)));
            transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
            transform.position += Vector3.Scale(oldCamTarget.transform.position, new Vector3(0, 1, 0));
            transform.position += new Vector3(0, oldCamPitchOffset, 0);
        }

    }
}
