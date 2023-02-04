using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHealthTaken : MonoBehaviour
{
    public int healthPoints = 10;
    public float lifetime = 10f;
    
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        Destroy(gameObject, lifetime);
    }
    
    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
            
        }
    }

    // Callback jika ada object yang keluar dari trigger
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            AddHealth();
        }
    }
    
    void AddHealth()
    {
        playerHealth.GainHealth(healthPoints);
        Destroy(gameObject, 0.2f);
    }
}
