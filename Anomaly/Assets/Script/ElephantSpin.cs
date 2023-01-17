using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantSpin : MonoBehaviour
{
    Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        axis = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(axis * 15);
    }
}
