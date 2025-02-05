using UnityEngine;
using System.Collections.Generic;

public class FireballCollision : MonoBehaviour
{
    // L?u tr? danh s�ch c�c s? ki?n va ch?m c?a particle
    [SerializeField] private List<ParticleCollisionEvent> collisionEvents = new();
    [SerializeField] private ParticleSystem ps;

    void Start()
    {
        // L?y component ParticleSystem c?a fireball
        ps = GetComponent<ParticleSystem>();
    }

    // H�m n�y s? ???c Unity g?i m?i khi particle va ch?m v?i m?t GameObject c� collider
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Va ch?m v?i: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Va ch?m v?i Player");
            int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
            Debug.Log("S? l??ng s? ki?n va ch?m: " + numCollisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                // X? l� theo logic mong mu?n
            }
        }
    }

}
