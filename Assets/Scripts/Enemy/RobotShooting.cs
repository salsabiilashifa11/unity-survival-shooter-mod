using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public int maxDamagePerShot = 1000;
    public int damagePerShot = 3;                  
    public float timeBetweenBullets = 1f;        
    public float range = 100f;

    float timer;                                    
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;   
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    Animator anim;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        anim = GetComponentInParent <Animator> ();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenBullets && IsInMeleeRangeOf(player) && enemyHealth.currentHealth > 0
            && playerHealth.currentHealth > 0)
        {
            Shoot();
        }
        
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
        
        
    }
    
    private bool IsInMeleeRangeOf (Transform player) {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < range;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            if (shootHit.collider == player.GetComponent<CapsuleCollider>()) {
                if (playerHealth.currentHealth > 0)
                {
                    playerHealth.TakeDamage(damagePerShot);
                }
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        
        
    }
    

}
