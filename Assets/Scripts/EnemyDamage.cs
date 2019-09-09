using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Material whiteMaterial;
    public float flashDurationTime;
    public int maxHealth;

    private int healthRemaining;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private Object explosionRef;

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
            Debug.Log("PlasmaBullet");
            Destroy(collider.gameObject);
            healthRemaining--;
            spriteRenderer.material = whiteMaterial;

            if (healthRemaining <= 0)
                KillSelf();
            else
                Invoke("ResetMaterial", flashDurationTime);
        }
    }

    private void ResetMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
