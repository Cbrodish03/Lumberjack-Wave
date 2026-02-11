using UnityEngine;

public class Player : MonoBehaviour
{
    public EntityScript entityData;
    public Collider2D playerCollider;
    int health;
    int damage;
    int attackSpeed;
    float speed;
    int defense;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // init player stats from ScriptableObject
        this.health = entityData.health;
        this.damage = entityData.damage;
        this.attackSpeed = entityData.attackSpeed;
        this.speed = entityData.speed;
        this.defense = entityData.defense;
        
        // get player collider reference
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by enemy!");
            // decrease player health by enemy damage
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && playerCollider.enabled) // Only take damage if the player's collider is enabled (not invulnerable)
            {
                this.health -= enemy.entityData.damage;
                // give player a brief invulnerability period after being hit (disable collider for a short time)
                playerCollider.enabled = false;
                Invoke("EnablePlayerCollider", 2f); // Re-enable collider after
            }
            if (this.health <= 0)
            {
                Debug.Log("Player has died!");
                Die();
                // Handle player death (e.g., reload scene, show game over screen, etc.)
            }
        }
    }

    private void Die()
    {
        // For now, just destroy the player object
        Destroy(gameObject);
    }

    private void EnablePlayerCollider()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }
    
}
