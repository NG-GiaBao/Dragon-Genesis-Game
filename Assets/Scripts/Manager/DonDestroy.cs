using UnityEngine;

public class DonDestroy : MonoBehaviour
{
    private static DonDestroy instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;  // L?u instance ??u ti�n
            DontDestroyOnLoad(gameObject);  // Gi? object kh�ng b? destroy
        }
        else
        {
            Destroy(gameObject);  // X�a object m?i t?o n?u ?� c� instance
        }
    }
}
