using UnityEngine;

[CreateAssetMenu(fileName = "EntityScript", menuName = "Scriptable Objects/EntityScript")]
public class EntityScript : ScriptableObject
{
    [SerializeField] public int health;         // Health points of the entity
    [SerializeField] public int damage;         // Damage the entity can inflict
    [SerializeField] public int attackSpeed;    // Attack speed of the entity
    [SerializeField] public float speed;        // Movement speed of the entity
    [SerializeField] public int defense;        // Defense points of the entity
}
