using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    //[SerializeField] private GameObject m_BloodPrehabs;
    [SerializeField] private GameObject m_HitPrehabs;
    [SerializeField] private bool m_IsTakeDamaged;
    [SerializeField] private float m_timer;
    private void OnTriggerEnter(Collider other)
    {
        // Ch? x? l� n?u va ch?m v?i enemy ki?u Creep ho?c Boss
        if (!other.gameObject.CompareTag("Creep") && !other.gameObject.CompareTag("Boss"))
            return;

        // N?u player ?ang ? tr?ng th�i idle th� kh�ng g�y damage
        if (PlayerManager.instance.m_PlayerState.Equals(PlayerManager.PlayerState.idle))
            return;

        // N?u enemy ?� b? damage trong ?�n n�y, b? qua
        if (m_IsTakeDamaged)
            return;
        int damage = PlayerManager.instance.playerDamage.GetPlayerDamage();

        if (PlayerManager.instance.playerDamage.Heavyattack)
        {
            Debug.Log($"Heavyattack :{PlayerManager.instance.playerDamage.Heavyattack}");
            int baseDamage = damage;
            int bonus = Mathf.RoundToInt(baseDamage * 1.15f);
            damage = bonus;
            Debug.Log($"damage : {damage}");
            Debug.Log($"bonus :{bonus}");
        }
        else
        {
            Debug.Log($"Heavyattack :{PlayerManager.instance.playerDamage.Heavyattack}");
        }

        if (other.gameObject.CompareTag("Creep"))
        {
            EnemyHeal enemyHeal = other.GetComponentInParent<EnemyHeal>();
            if (enemyHeal != null)
            {
                enemyHeal.ReducePlayerHealth(damage);
                if (EffectManager.HasInstance)
                {

                    Vector3 closepoint = other.ClosestPoint(other.transform.position);
                    GameObject textDamage = Instantiate(EffectManager.Instance.GetPrefabs("DamageText"), other.transform.position, other.transform.rotation);

                    textDamage.TryGetComponent<SetupTextDamage>(out var damageText);
                    if (damageText != null)
                    {
                        damageText.ChangeTextDamage(damage, closepoint);
                    }
                }
                CameraManager.Instance.ShakeCamera();
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE("attaccolidersound");
                }
            }
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            if (other.TryGetComponent<WormBoss>(out var wormBoss))
            {
                wormBoss.GetDamage(damage);
                if (EffectManager.HasInstance)
                {
                    Vector3 closepoint = other.ClosestPoint(other.transform.position);
                    Vector3 newClosePoint = new Vector3(closepoint.x, closepoint.y, closepoint.z - 1);
                    GameObject textDamage = Instantiate(EffectManager.Instance.GetPrefabs("DamageText"), other.transform.position, other.transform.rotation);
                    textDamage.TryGetComponent<SetupTextDamage>(out var damageText);
                    if (damageText != null)
                    {
                        damageText.ChangeTextDamage(damage, newClosePoint);
                    }
                }
                CameraManager.Instance.ShakeCamera();
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE("WormBossHit");
                    AudioManager.Instance.PlaySE("attaccolidersound");

                }
            }
        }

        // ?�nh d?u ?� g�y damage r?i v� reset flag sau m?t kho?ng th?i gian nh?t ??nh
        m_IsTakeDamaged = true;
        StartCoroutine(ResetDamageFlag());

        SpawnHitPrehabs(other);
    }

    private void SpawnHitPrehabs(Collider other)
    {
        Vector3 hitPos = other.ClosestPoint(transform.position);
        GameObject HitFX = Instantiate(m_HitPrehabs, hitPos, Quaternion.identity);
        HitFX.SetActive(true);
        Destroy(HitFX, 1f);
    }
    private IEnumerator ResetDamageFlag()
    {
        yield return new WaitForSeconds(m_timer);
        m_IsTakeDamaged = false;
    }

}

