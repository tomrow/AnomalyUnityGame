using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject target;
    private float yrot;
    private float xrot;
    public float cam_dist;
    void Update()
    {
        cam_dist = Vector3.Distance(transform.position, target.transform.position);
        // Spin the object around the target at 20 degrees/second.
        transform.LookAt(target.transform, Vector3.left);
        transform.RotateAround(target.transform.position, Vector3.up, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        transform.Translate(Vector3.forward * (cam_dist-(15*2)));
        transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
        transform.position += Vector3.Scale(target.transform.position, new Vector3(0, 1, 0));
        transform.position += new Vector3(0, 2, 0);

    }
}