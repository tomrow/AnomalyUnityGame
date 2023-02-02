using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit something");
        //is it an enemy?
        if (collision.transform.tag == "Enemy") 
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<GameStateVariables>().score -= 1;
        }
    }
}
