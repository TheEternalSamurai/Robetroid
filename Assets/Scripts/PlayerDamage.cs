using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int maxHealth;
    public Animator anim;

    private bool hasBeenHit;
    private int healthRemaining;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private Object explosionRef;

    private void Start()
    {
        hasBeenHit = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthRemaining = maxHealth;
        defaultMaterial = spriteRenderer.material;
        explosionRef = Resources.Load("Animations/Explosion");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && !hasBeenHit)
        {
            Debug.Log("Hit");
            StartCoroutine("ReceiveDamage", collider.gameObject);
            if (healthRemaining <= 0)
                KillSelf();
        }
    }

    private IEnumerator ReceiveDamage(GameObject enemy)
    {
        healthRemaining--;
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemy.layer, true);
        hasBeenHit = true;

        yield return new WaitForSeconds(3f);

        Physics2D.IgnoreLayerCollision(gameObject.layer, enemy.layer, false);
        hasBeenHit = false;
    }

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
