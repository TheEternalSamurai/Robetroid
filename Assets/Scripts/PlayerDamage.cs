using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int maxHealth;
    public float knockBackTime;
    public Vector2 damageThrust;
    public Animator anim;

    private Rigidbody2D rigBody;
    private bool hasBeenHit;
    private int healthRemaining;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private Object explosionRef;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
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
            StartCoroutine("KnockBack");
            StartCoroutine("ReceiveDamage");
            if (healthRemaining <= 0)
                KillSelf();
        }
    }

    private IEnumerator KnockBack()
    {
        PlayerController controller = GetComponent<PlayerController>();
        controller.enabled = false;
        rigBody.velocity = Vector2.zero;
        Vector2 knockBackForce = -transform.right * damageThrust.x + transform.up * damageThrust.y;
        rigBody.AddForce(knockBackForce, ForceMode2D.Impulse);
        anim.SetBool("hasBeenHit", true);

        yield return new WaitForSeconds(knockBackTime);

        controller.enabled = true;
        anim.SetBool("hasBeenHit", false);
    }

    private IEnumerator ReceiveDamage()
    {
        healthRemaining--;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        hasBeenHit = true;

        yield return new WaitForSeconds(3f);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        hasBeenHit = false;
    }

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
