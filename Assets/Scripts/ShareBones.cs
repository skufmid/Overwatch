using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class ShareBones : MonoBehaviour
{
    [Tooltip("애니메이션이 재생되는 원본 SkinnedMeshRenderer (WorldModel_Visual)")]
    public SkinnedMeshRenderer sourceRenderer;

    void Awake()
    {
        SkinnedMeshRenderer targetRenderer = GetComponent<SkinnedMeshRenderer>();

        if (sourceRenderer != null && targetRenderer != null)
        {
            targetRenderer.bones = sourceRenderer.bones;
            targetRenderer.rootBone = sourceRenderer.rootBone;
        }
    }
}