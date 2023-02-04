using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberAttack : MonoBehaviour
{
    public int attackDamage = 10;
    
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    ParticleSystem explosionParticles; 
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        anim = GetComponent <Animator> ();
        // Mendapatkan Enemy health
        enemyHealth = GetComponent<EnemyHealth>();
        explosionParticles = GetComponent<ParticleSystem>();
    }
    
    
    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player in range
        if (other.gameObject == player && other.isTrigger == false)
        {
            Attack();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }
    
    void Attack()
    {
        Debug.Log("Calling ATTACK");
        // Taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        
        //Kill self
        explosionParticles.Play();
        enemyHealth.BomberTakeDamage(enemyHealth.currentHealth);
        
    }
}
