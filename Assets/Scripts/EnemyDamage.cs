using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public Material whiteMaterial;
    public float flashDurationTime;
    public int maxHealth;
    public int numOfExplosions = 1;
    public float timeBetweenExplosions = 0f;

    private int healthRemaining;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private Object explosionRef;

    [Header("UI")]
    public Image healthBar;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthRemaining = maxHealth;
        defaultMaterial = spriteRenderer.material;
        explosionRef = Resources.Load("Animations/Explosion");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlasmaBullet"))
        {
            Hit(collider);

            if (healthRemaining <= 0)
                StartCoroutine(KillSelf());
            else
                Invoke("ResetMaterial", flashDurationTime);
        }
    }

    private void Hit(Collider2D collider)
    {
        Destroy(collider.gameObject);
        healthRemaining--;
        spriteRenderer.material = whiteMaterial;
        if (healthBar != null)
            healthBar.fillAmount = (float)healthRemaining / (float)maxHealth;

        FindObjectOfType<AudioManager>().Play("Hit");
    }

    private void ResetMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    private IEnumerator KillSelf()
    {
        if (gameObject.name == "BossSprite")
        {
            gameObject.GetComponent<ShooterAI>().enabled = false;
            gameObject.GetComponent<PatrolAir>().enabled = false;

            FindObjectOfType<AudioManager>().Stop("Boss Music");
        }

        for (int i = 0; i < numOfExplosions; i++)
        {
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = transform.position;
            FindObjectOfType<AudioManager>().Play("Explosion");

            yield return new WaitForSeconds(timeBetweenExplosions);
        }

        Destroy(gameObject);

        if (gameObject.name == "BossSprite")
        {
            FindObjectOfType<AudioManager>().Play("Victory");
            GameObject ui = GameObject.Find("UI");
            GameObject winMenu = ui.transform.Find("WinMenu").gameObject;
            winMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
