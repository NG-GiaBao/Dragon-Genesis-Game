using System.Linq;
using UnityEngine;

public class SwapClothes : MonoBehaviour
{
    [Header("Settings")]
    public SkinnedMeshRenderer[] skinnedMeshRenderersList; // Danh s�ch c�c SkinnedMeshRenderer c?n chuy?n
    public Transform newArmature; // Armature m?i (b? khung x??ng c?a nh�n v?t)
    public Transform newParent; // Transform m?i ?? l�m cha

    private void Start()
    {
        TransferSkinnedMeshes();
    }
    /// <summary>
    /// Th?c hi?n chuy?n ??i c�c SkinnedMeshRenderers.
    /// </summary>
    public void TransferSkinnedMeshes()
    {
        if (newArmature == null || newParent == null)
        {
            Debug.LogError("New Armature or New Parent is not assigned!");
            return;
        }

        if (skinnedMeshRenderersList == null || skinnedMeshRenderersList.Length == 0)
        {
            Debug.LogError("No SkinnedMeshRenderers assigned to transfer.");
            return;
        }

        foreach (var skinnedMeshRenderer in skinnedMeshRenderersList)
        {
            if (skinnedMeshRenderer == null)
            {
                Debug.LogWarning("One of the SkinnedMeshRenderers is null. Skipping.");
                continue;
            }

            // L?u l?i root bone g?c
            string cachedRootBoneName = skinnedMeshRenderer.rootBone.name;

            // T�m x??ng t??ng ?ng trong Armature m?i
            var newBones = new Transform[skinnedMeshRenderer.bones.Length];
            for (int i = 0; i < skinnedMeshRenderer.bones.Length; i++)
            {
                string boneName = skinnedMeshRenderer.bones[i].name;
                Transform newBone = newArmature.GetComponentsInChildren<Transform>()
                    .FirstOrDefault(bone => bone.name == boneName);

                if (newBone != null)
                {
                    newBones[i] = newBone;
                }
                else
                {
                    Debug.LogWarning($"Bone '{boneName}' not found in new armature.");
                }
            }

            // T�m root bone t??ng ?ng trong Armature m?i
            Transform matchingRootBone = GetRootBoneByName(newArmature, cachedRootBoneName);
            skinnedMeshRenderer.rootBone = matchingRootBone != null ? matchingRootBone : newArmature;

            // G�n l?i c�c x??ng m?i
            skinnedMeshRenderer.bones = newBones;

            // ??t l?i Transform c?a v?t ph?m
            Transform itemTransform = skinnedMeshRenderer.transform;
            itemTransform.SetParent(newParent);
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;

            Debug.Log($"Transferred SkinnedMeshRenderer: {skinnedMeshRenderer.name}");
        }
    }

    /// <summary>
    /// T�m root bone trong Armature m?i d?a tr�n t�n.
    /// </summary>
    private Transform GetRootBoneByName(Transform parentTransform, string name)
    {
        return parentTransform.GetComponentsInChildren<Transform>()
            .FirstOrDefault(transformChild => transformChild.name == name);
    }
}
