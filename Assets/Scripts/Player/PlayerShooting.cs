using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    PlayerGun c;
    int damagePerShot;                  
    float timeBetweenBullets;        
    float range;
    public int whichDirection = 0;
    int diagWeaponLvl;                      

    float timer;                                    
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;                

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        c = gameObject.GetComponentInParent<PlayerGun>();
        damagePerShot = c.damagePerShot;                  
        timeBetweenBullets = c.timeBetweenBullets;        
        range = c.range;
        diagWeaponLvl = c.diagWeaponLvl; 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            if (isShoot(whichDirection, diagWeaponLvl)){
                Shoot();
            }
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        damagePerShot = c.damagePerShot;                  
        timeBetweenBullets = c.timeBetweenBullets;        
        range = c.range;
        diagWeaponLvl = c.diagWeaponLvl;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    bool isShoot(int directionIdx, int lvl){
        if (lvl == 0){
            return (directionIdx == 0);
        } else if (lvl == 1) {
            return (directionIdx == 0 || directionIdx == 1 || directionIdx == 2);
        } else {
            return (directionIdx == 0 || directionIdx == 1 || directionIdx == 2 || directionIdx == 3 || directionIdx == 4);
        }
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
        if (whichDirection == 0){
            shootRay.direction = transform.forward;
        } else if (whichDirection == 1){
            shootRay.direction = (4*transform.forward + transform.right).normalized;
        } else if (whichDirection == 2){
            shootRay.direction = (4*transform.forward - transform.right).normalized;
        } else if (whichDirection == 3){
            shootRay.direction = (2*transform.forward + transform.right).normalized;
        } else if (whichDirection == 4){
            shootRay.direction = (2*transform.forward - transform.right).normalized;
        }
        

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}