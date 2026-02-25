using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Entity", order = 1)]
public class EntityData : ScriptableObject
{
    public int health;              // Health points of the entity
    public int damage;              // Damage the entity can inflict
    public int attackSpeed;         // Attack speed of the entity
    public float speed;             // Movement speed of the entity
    public int defense;             // Defense points of the entity
    public int unitCost;            // value of the unit
    public Vector3 currentPosition; // Current position of the entity in the game world
}
