using System.Collections;
using UnityEngine;

public class EnemyMarterialHit : MonoBehaviour
{
    public Material whiteHitMaterial;  // Material tr?ng ?� t?o
    private Material originalMaterial;
    private Renderer enemyRenderer;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        originalMaterial = enemyRenderer.material;
    }

    // G?i h�m n�y khi enemy b? hit
    public void ShowHitEffect(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(HitEffect(duration));
    }

    IEnumerator HitEffect(float duration)
    {
        enemyRenderer.material = whiteHitMaterial;
        yield return new WaitForSeconds(duration);
        enemyRenderer.material = originalMaterial;
    }
}
