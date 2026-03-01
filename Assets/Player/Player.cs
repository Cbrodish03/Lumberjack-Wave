using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private EntityData entityData;
    public Collider2D playerCollider;
    public int health;
    public int MAX_HEALTH;
    public int damage;
    public int attackSpeed;
    public float speed;
    public int defense;
    public int materials;
    private float nextAttackTime = 0f;

    private Color startColor;


    [SerializeField] private GameObject slashAttack;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // init player stats from ScriptableObject
        this.health = entityData.health;
        this.MAX_HEALTH = this.health;
        this.damage = entityData.damage;
        this.attackSpeed = entityData.attackSpeed;
        this.speed = entityData.speed;
        this.defense = entityData.defense;

        startColor = GetComponent<SpriteRenderer>().color;  // starting color required for visual indicators
        
        // get player collider reference
        playerCollider = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime) // Left mouse button for attack
        {
            Debug.Log("Player attacks with damage: " + damage);
            attack();
            nextAttackTime = Time.time + 1f / attackSpeed; // Set next attack time based on attack speed
        }

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
                int incomingDamage = Math.Max(enemy.damage - defense, 1);     // consider entity defense on damage till damage <= 1
                this.health -= incomingDamage;
                playerCollider.enabled = false;
                StartCoroutine(VisualIndicator(Color.red));
                Invoke("EnablePlayerCollider", 2f); // Re-enable collider after 2 seconds
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

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = startColor;
    }

    private void EnablePlayerCollider()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }

    private void heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Heal cannot be negative. Enter value > 0");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }

        StartCoroutine(VisualIndicator(Color.green));
    }

    private void attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 attackDirection = (mousePosition - transform.position).normalized;
        Vector3 attackPosition = transform.position + attackDirection * 1.75f;

        // NOTE: GIVING THE ANGLE HAS NO EFFECT HERE, CHECK IN "SelfKill" for rotation logic
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;        // Compute rotation
        Quaternion rotation = Quaternion.Euler(20f, 20f, 20f);                                  // Adjust if your triangle points upward by default
        Instantiate(slashAttack, attackPosition, rotation);                                     // Spawn slash

        // Debug.DrawLine(Vector3 , Vector3 end, Color color = Color.white, float duration = 0.0f, bool depthTest = true);
        Debug.DrawLine(transform.position, attackPosition, Color.red, 0.5f, false);
        
        // 2. Check for enemies that collide with this circle and apply damage to them
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, 0.5f); // Check for enemies within a radius of 0.5 units
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                if (enemyCollider.CompareTag("Enemy"))
                {
                    Enemy enemy = enemyCollider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log("Hit enemy with damage: " + damage);
                        if (enemy.health <= 0) 
                        {
                            heal(1);    // heal on death of enemy
                            // deal with giving the player a resource on the death of the enemy
                        }
                    }
                }
            }
    }

    public void AddDrops(int maxDrops)
    {
        System.Random r = new System.Random();
        int dropsToAdd = r.Next((int)(maxDrops * 0.5), maxDrops);
        this.materials += dropsToAdd;
    }
    
}
