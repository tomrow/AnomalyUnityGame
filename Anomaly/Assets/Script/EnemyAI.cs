using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    //Variable declarations
    NavMeshAgent nav; //navigation mesh
    Transform player; // contains player position
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindGameObjectWithTag("Player"); //search for player object and get its position properties when found
    }

    // Update is called once per frame
    void Update()
    {
        player = playerObject.transform;
        nav.SetDestination(player.position); //tells object to seek the player coordinates
    }
}
