using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject orb;
    public int spawnOrb;
    public float spawnTime = 5f;
    public Transform spawnPoint;
    
    [SerializeField]
    public MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }
    
    void Start ()
    {
        //Mengeksekusi fungs Spawn setiap beberapa detik sesui dengan nilai spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    
    void Spawn ()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        
        int orbIndex = Random.Range (0, 3);
        
        spawnPoint.position = new Vector3(Random.Range(-20f, 20f), 0.5f, Random.Range(-12f, 12f));

        // Menduplikasi orb
        Instantiate(Factory.FactoryMethod(orbIndex), spawnPoint.position, spawnPoint.rotation);
        

    }
    
}
