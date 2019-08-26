using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public int maxHealth;
    public float knockBackTime;
    public float invulnerableTime;
    public float deathTime;
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
            StartCoroutine("ReceiveDamage");

            if (healthRemaining > 0)
                StartCoroutine("KnockBack");
            else
                StartCoroutine("KillSelf");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("StageBounds"))
            StartCoroutine("KillSelf");
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

        yield return new WaitForSeconds(invulnerableTime);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        hasBeenHit = false;
    }

    private IEnumerator KillSelf()
    {
        PlayerController controller = GetComponent<PlayerController>();
        controller.enabled = false;
        rigBody.bodyType = RigidbodyType2D.Static;

        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(deathTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
