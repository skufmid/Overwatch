using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class ShareBones : MonoBehaviour
{
    [Tooltip("애니메이션이 재생되는 원본 SkinnedMeshRenderer (WorldModel_Visual)")]
    public SkinnedMeshRenderer sourceRenderer;

    void Awake()
    {
        // 그림자용 타겟 렌더러 가져오기
        SkinnedMeshRenderer targetRenderer = GetComponent<SkinnedMeshRenderer>();

        if (sourceRenderer != null && targetRenderer != null)
        {
            // 1. 원본의 뼈대(Bones) 배열을 그대로 복사하여 할당
            targetRenderer.bones = sourceRenderer.bones;

            // 2. 최상위 루트 본(Root Bone) 할당
            targetRenderer.rootBone = sourceRenderer.rootBone;
        }
        else
        {
            Debug.LogWarning("ShareBones: Source Renderer가 할당되지 않았습니다!");
        }
    }
}