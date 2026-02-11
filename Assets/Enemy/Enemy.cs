using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EntityScript entityData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // init enemy stats from ScriptableObject
        int health = entityData.health;
        int damage = entityData.damage;
        int attackSpeed = entityData.attackSpeed;
        float speed = entityData.speed;
        int defense = entityData.defense;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
