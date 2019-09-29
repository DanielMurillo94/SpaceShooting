using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;
    [SerializeField] Sprite[] impactSprites;
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        StartCoroutine(DestroyItself());
    }

    private IEnumerator DestroyItself()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteIndex = Random.Range(0, impactSprites.Length);
        spriteRenderer.sprite = impactSprites[spriteIndex];
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
