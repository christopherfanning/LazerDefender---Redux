using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // Config params
    [Header ("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] float playerDeathTime = 2f;
    [SerializeField] int score = 0;

    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = .2f ;

    [SerializeField] AudioClip playerShootSound;
    [SerializeField] AudioClip playerDieSound;

  // Initialize vars 
    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
      
        
    }




  // Update is called once per frame
  void Update()
    {
      score = FindObjectOfType<GameStatus>().GetScore();
        Move();
        Fire();
        
    }


  public int GetHealth()
  {
    return health;
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
      KillPlayer();
    }
  }

  private void KillPlayer()
  {
  FindObjectOfType<LevelController>().LoadGameOver();    
  // Grab player transform.position to use for a place to play death animation. 
    AudioSource.PlayClipAtPoint(playerDieSound, Camera.main.transform.position);
    Destroy(gameObject, playerDeathTime);
    
  }



 


  private void Fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
        firingCoroutine = StartCoroutine( FireContinuously() );
    }
    if (Input.GetButtonUp("Fire1"))
    {
      StopCoroutine(firingCoroutine);
    }

  }


 
  IEnumerator FireContinuously()
  {
    while(true) 
    {
      AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position);
      GameObject laser = Instantiate(
        laserPrefab, 
        transform.position, 
        Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      yield return new WaitForSeconds(projectileFiringPeriod);
    }
  }

  private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        // newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

    }




private void SetUpMoveBoundries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;

  }
}
