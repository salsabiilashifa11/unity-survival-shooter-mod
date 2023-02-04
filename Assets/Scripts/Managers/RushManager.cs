using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TimerManager timerManager;
    public int killCount;
//     Start is called before the first frame update
    void Start()
    {
    
    }

//     Update is called once per frame
    void Update()
    {
        if (timerManager.time <= 0) {
            Debug.Log("Time up!");
            
            //Kill player
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
    }
}
