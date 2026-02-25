using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private GameObject attackArea = default;    // reference to the attack area GameObject (child of player)
    private bool attacking = false;             // flag to track if the player is currently attacking
    private float timeToAttack = 0.25f;         // attack delay time
    private float timer = 0f;                   // timer to track attack delay

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackArea = transform.Find("AttackArea").gameObject; // Get reference to the AttackArea child GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse button for attack
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true); // Enable the attack area collider to detect hits

    }

}
