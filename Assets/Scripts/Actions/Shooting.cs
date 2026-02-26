using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator anim;

    const float DEFAULT_SHOOT_INTERVAL = 1f;
    const int DEFAULT_MAGAZINE_COUNT = 5;

    float shootInterval;
    int magazineCount;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void HandleShoot(bool isShoot)
    {
        anim.SetBool("IsShoot", isShoot);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
    }
}
