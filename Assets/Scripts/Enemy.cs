using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int attackSpeed;
    public float speed;
    public int defense;
    public int unitCost;

    [SerializeField] public EntityData data;

    private GameObject player;
    private Color startColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // inherit attributes from EnemyData
        health = data.health;
        damage = data.damage;
        attackSpeed = data.attackSpeed;
        speed = data.speed;
        defense = data.defense;
        unitCost = data.unitCost;

        startColor = GetComponent<SpriteRenderer>().color;  // starting color required for visual indicators

    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    private void followPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);    // follow until player is destoryed
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        int incomingDamage = Math.Max(damageAmount - defense, 1);     // consider entity defense on damage till damage <= 1
        this.health -= incomingDamage;
        StartCoroutine(VisualIndicator(Color.red));
        if (this.health <= 0)
        {
            Die();
        }
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = startColor;
    }

     private void Die()
    {
        // For now, just destroy the player object
        Destroy(gameObject);
    }

}
