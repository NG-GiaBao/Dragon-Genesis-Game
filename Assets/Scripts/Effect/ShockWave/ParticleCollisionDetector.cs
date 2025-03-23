using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDetector : MonoBehaviour
{
    public EnemySkill enemySkill; // Tham chi?u ??n script qu?n l�

    private void Awake()
    {
        enemySkill = GetComponentInParent<EnemySkill>();
    }
    void OnParticleCollision(GameObject other)
    {
        // L?y ParticleSystem hi?n h�nh
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // L?y th�ng tin chi ti?t c?a va ch?m n?u c?n
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(ps, other, collisionEvents);

        // G?i h�m x? l� trong EnemySkill
        if (enemySkill != null)
        {
            enemySkill.HandleParticleCollision(gameObject, other, collisionEvents);
        }
    }

}
