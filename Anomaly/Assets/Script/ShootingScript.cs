using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    float projectileSpeed = 11000f;
    public GameObject projectile;
    AudioSource boom;
    
    // Start is called before the first frame update
    void Start()
    {
        //audio
        boom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            boom.Play();
        }
    }
    void Fire() 
    {
        GameObject tempProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyProjectile = tempProjectile.GetComponent<Rigidbody>();
        tempRigidBodyProjectile.AddForce(tempRigidBodyProjectile.transform.forward*projectileSpeed);
        Destroy(tempProjectile, 0.5f);

    }
}
