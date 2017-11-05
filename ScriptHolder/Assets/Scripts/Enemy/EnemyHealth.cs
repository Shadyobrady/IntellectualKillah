using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = -1;
    public AudioClip deathClip;
    private EnemyAnimController animController;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        animController = GetComponentInChildren<EnemyAnimController>();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;
        //enemyAudio.Play ();
        currentHealth -= amount;
        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage (float amount, Vector3 hitPoint)
    {
        if(isDead)
            return;
        //enemyAudio.Play ();
        currentHealth -= amount;
        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        Destroy(gameObject);
        animController.isDead();
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
