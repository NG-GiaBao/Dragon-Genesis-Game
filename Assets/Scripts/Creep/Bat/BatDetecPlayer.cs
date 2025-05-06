using UnityEngine;

public class BatDetecPlayer : BaseEnemyDetecPlayer
{
    private bool hasPlayedIdle = false;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void CalculateDistance()
    {
        // T�nh kho?ng c�ch gi?a enemy v� player
        m_DistacneWithPlayer = Vector3.Distance(this.transform.position, m_Player.position);

        // N?u player trong ph?m vi ph�t hi?n (maxDistance)
        if (m_DistacneWithPlayer < m_MaxDistance)
        {
            m_DetectedPlayer = true;
            enemyController.GetAnimator().SetBool("IsDetec", true);

            // Enemy b?t ??u theo ?u?i player
            enemyController.GetNavMeshAgent().SetDestination(m_Player.position);
            enemyController.GetNavMeshAgent().stoppingDistance = m_DictanceStopped;
          
            if (!hasPlayedIdle)
            {
               
                if (AudioManager.HasInstance)
                {
                    Debug.Log($"hasPlayedIdle {hasPlayedIdle}");
                    AudioManager.Instance.PlaySE("BatIdle");
                }

                hasPlayedIdle = true;
            }
           
            // Ki?m tra tr?ng th�i t?n c�ng
            if (m_DistacneWithPlayer <= m_DictanceStopped) // Trong ph?m vi t?n c�ng
            {
                enemyController.GetNavMeshAgent().isStopped = true; // D?ng di chuy?n
                enemyController.GetAnimator().SetBool("Attack", true); // T?n c�ng
            }
            else // Ngo�i ph?m vi t?n c�ng nh?ng trong maxDistance
            {
                enemyController.GetNavMeshAgent().isStopped = false; // Ti?p t?c di chuy?n
                enemyController.GetAnimator().SetBool("Attack", false); // Kh�ng t?n c�ng
            }
        }
        else
        {
            // N?u player v??t kh?i ph?m vi ph�t hi?n (maxDistance), reset tr?ng th�i
            m_DetectedPlayer = false;
            hasPlayedIdle = false;
            enemyController.GetAnimator().SetBool("IsDetec", false);
            enemyController.GetAnimator().SetBool("Attack", false); // T?t t?n c�ng
            enemyController.GetNavMeshAgent().stoppingDistance = 0f; // Reset kho?ng c�ch d?ng
            enemyController.GetNavMeshAgent().isStopped = false; // Cho ph�p di chuy?n t? do
           
        }
    }
    public override void ResetAttackAnimation()
    {
        enemyController.GetAnimator().SetBool("Attack", false);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
