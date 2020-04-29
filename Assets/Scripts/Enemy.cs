using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 6f;
    [SerializeField] int scoreValue = 50;

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionTime = 1f;
    
    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume = 0.75f;
    [SerializeField] [Range(0,1)] float dieSoundVolume = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f )
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void Fire()
    {
        // Fire the lazer!!

        GameObject laser = Instantiate(
        laserPrefab, 
        transform.position, 
        Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if ( !damageDealer ) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        FindObjectOfType<GameStatus>().AddToScore(scoreValue);
        GameObject explosionVFX = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosionVFX, explosionTime);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position, dieSoundVolume);
    }
}
