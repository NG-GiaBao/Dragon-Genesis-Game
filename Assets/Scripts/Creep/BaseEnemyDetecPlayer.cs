using UnityEngine;

public class BaseEnemyDetecPlayer : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController;
    [SerializeField] protected Transform m_Player;
    [SerializeField] protected bool m_DetectedPlayer;
    [SerializeField] protected float m_DistacneWithPlayer;
    [SerializeField] protected float m_DictanceStopped;
    [SerializeField] protected float m_MaxDistance;

    protected virtual void Awake()
    {
        m_Player = GameObject.Find("Player").GetComponent<Transform>();
        enemyController = GetComponent<EnemyController>();
    }

    // Ph??ng th?c t�nh kho?ng c�ch, ki?m tra ph�t hi?n v� t?n c�ng
    public virtual void CalculateDistance()
    {
        
    }

    public virtual void ResetAttackAnimation()
    {

    }    

    // V? gizmo ?? hi?n th? ph?m vi ph�t hi?n trong Editor
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_MaxDistance);
    }

}
