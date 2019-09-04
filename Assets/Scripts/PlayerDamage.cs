using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerDamage : MonoBehaviour
{
    public int startLives;
    public int maxHealth;
    public float knockBackTime;
    public float invulnerableTime;
    public float deathTime;
    public Vector2 damageThrust;
    public Animator anim;

    [Header("UI")]
    public Image healthBar;
    public TextMeshProUGUI livesText;
    public Canvas gameOverScreen;

    private Rigidbody2D rigBody;
    private bool hasBeenHit;
    private bool hasDied;
    private int livesRemaining;
    private int healthRemaining;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private Object explosionRef;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerCurrentLives"))
            PlayerPrefs.SetInt("PlayerCurrentLives", startLives);

        livesRemaining = PlayerPrefs.GetInt("PlayerCurrentLives");
        livesText.SetText("x " + livesRemaining);
    }

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        hasBeenHit = false;
        hasDied = false;
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
        else if (collider.CompareTag("Battery"))
        {
            AddHealth(collider.gameObject);
            Destroy(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("StageBounds"))
            StartCoroutine("KillSelf");
    }

    private void AddHealth(GameObject batteryObject)
    {
        Battery battery = batteryObject.GetComponent<Battery>();

        if (healthRemaining < maxHealth && !battery.hasBeenPickedUp)
        {
            healthRemaining += battery.healthGain;
            battery.hasBeenPickedUp = true;

            healthBar.fillAmount = (float)healthRemaining / (float)maxHealth;
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
        healthBar.fillAmount = (float)healthRemaining / (float)maxHealth;

        yield return new WaitForSeconds(invulnerableTime);

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        hasBeenHit = false;
    }

    private IEnumerator KillSelf()
    {
        if (!hasDied)
        {
            hasDied = true;
            PlayerController controller = GetComponent<PlayerController>();
            controller.enabled = false;
            rigBody.bodyType = RigidbodyType2D.Kinematic;
            rigBody.velocity = Vector2.zero;

            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = transform.position;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(deathTime);
            LoseLife();

            if (livesRemaining < 0)
                gameOverScreen.gameObject.SetActive(true);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void LoseLife()
    {
        livesRemaining--;
        livesText.SetText("x " + livesRemaining.ToString());

        if (livesRemaining < 0)
            PlayerPrefs.DeleteKey("PlayerCurrentLives");
        else
            PlayerPrefs.SetInt("PlayerCurrentLives", livesRemaining);
    }
}
