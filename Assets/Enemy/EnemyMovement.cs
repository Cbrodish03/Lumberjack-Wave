using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;  // Movement speed of the enemy
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set enemy speed from ScriptableObject
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.entityData != null)
        {
            speed = enemy.entityData.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move towards player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
}
