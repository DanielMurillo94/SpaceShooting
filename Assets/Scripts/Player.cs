using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration
    [Header("Player")]
    [SerializeField] float moveSpeed;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] [Range(0, 1)] float deathAudioVolume = 1f;
    [SerializeField] [Range(0, 5)] float deathShakeduration = 5f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileVelocity;
    [SerializeField] float projectileFiringPeriod;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootAudioVolume = 0.75f;
    [SerializeField] [Range(0, 1)] float projectileSpread = 0.3f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newPosX, newPosY);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            float xPosition = Random.Range(-projectileSpread, projectileSpread);
            GameObject laser = Instantiate(laserPrefab, new Vector3(transform.position.x + xPosition, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileVelocity);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootAudioVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        FindObjectOfType<CameraShaker>().StartShake(deathShakeduration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathAudioVolume);
        GameObject deathVFX = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(deathVFX, 5f);
        Destroy(gameObject);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        if (health <= 0)
        {
            return 0;
        }
        return health;
    }
}
