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

    //private void OnAnimatorIK(int layerIndex)
    //{
    //    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
    //    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);

    //    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
    //    anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
    //}
}
