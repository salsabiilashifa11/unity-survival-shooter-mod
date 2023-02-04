using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpeedTaken : MonoBehaviour
{
    public float speedPoints = 1f;
    public float lifetime = 10f;
    
    GameObject player;
    PlayerMovement playerMovement;
    bool playerInRange;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerMovement = player.GetComponent <PlayerMovement> ();
        Destroy(gameObject, lifetime);
    }
    
    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            AddSpeed();
            
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    
    void AddSpeed()
    {
        playerMovement.GainSpeed(speedPoints);
        Destroy(gameObject, 0.2f);
    }
}
