using UnityEngine;

public class SelfKill : MonoBehaviour
{
    private Transform player;       // player reference
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        SwingAttack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Object.Destroy(gameObject, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // swing the attack from the players location to the attack position (in the direction of the mouse)
    public void SwingAttack(Vector3 attackPosition)
    {
        // orient the attack in the direction of the mouse
        Vector3 directionToMouse = (attackPosition - player.position).normalized;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 30f);
    }
}
