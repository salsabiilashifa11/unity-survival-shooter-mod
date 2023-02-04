using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPowerTaken : MonoBehaviour
{
    public int powerPoints = 10;
    public float lifetime = 10f;
    
    GameObject player;
    bool playerInRange;
    PlayerGun playerGun;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerGun = player.GetComponent <PlayerGun> ();
        Destroy(gameObject, lifetime);
    }
    
    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            AddPower();
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void AddPower()
    {
        playerGun.GainPower(powerPoints);
        Destroy(gameObject, 0.2f);
    }
}
