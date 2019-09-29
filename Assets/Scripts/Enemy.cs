using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] [Range(0, 1)] float deathAudioVolume = 0.75f;
    [SerializeField] AudioClip hitAudioClip;
    [SerializeField] [Range(0, 1)] float hitAudioVolume = 0.3f;
    [SerializeField] int pointsObtainedAtKill = 15;
    [SerializeField] [Range(0, 1)] float deathShakeduration = 0.05f;

    [Header("Projectile")]
    [SerializeField] float minTimeBetweenShooting = 0.5f;
    [SerializeField] float maxTimeBetweenShooting = 3f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileVelocity = 5f;
    [SerializeField] AudioClip ShootAudioClip;
    [SerializeField] [Range(0, 1)] float shootAudioVolume = 0.5f;

    float shotTimer;
    // Start is called before the first frame update
    void Start()
    {
        shotTimer = UnityEngine.Random.Range(minTimeBetweenShooting, maxTimeBetweenShooting);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileVelocity);
        AudioSource.PlayClipAtPoint(ShootAudioClip, Camera.main.transform.position, shootAudioVolume);
        shotTimer = UnityEngine.Random.Range(minTimeBetweenShooting, maxTimeBetweenShooting);
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
            DestructSelf();
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitAudioClip, Camera.main.transform.position, hitAudioVolume);
        }
    }

    private void DestructSelf()
    {
        GameObject explotion = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(explotion, 1f);
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession)
        {
            gameSession.AddScore(pointsObtainedAtKill);
        }
        AudioSource.PlayClipAtPoint(deathAudioClip, Camera.main.transform.position, deathAudioVolume);
        FindObjectOfType<CameraShaker>().StartShake(deathShakeduration);
        Destroy(gameObject);
    }
}
