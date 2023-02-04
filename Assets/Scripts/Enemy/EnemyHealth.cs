using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public ParticleSystem hitParticles;


    Animator anim;
    AudioSource enemyAudio;
//    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        // Mendapatkan reference component
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
//        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        

        // Set current health
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            // memindahkan object ke bawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        Debug.Log(hitParticles);

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void BomberTakeDamage(int amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        // Disable NavMesh component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Set Rigidbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        try {
            KillCountManager.kills += 1;
        } catch {
        
        }
        
        Destroy(gameObject, 2f);
    }
}
