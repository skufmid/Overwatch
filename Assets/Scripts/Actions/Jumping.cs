using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float JumpForce { get; set; } = 5f;
    public bool IsJumping { get; set; } = false;

    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void HandleJump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Utility.SetAnimations("OnJump", ParameterType.Trigger);

        IsJumping = true;
        StartCoroutine(ResetJumping());
    }

    IEnumerator ResetJumping()
    {
        while (true)
        {
            if (rb.linearVelocity.y < -0.1f)
            {
                IsJumping = false;
                StartCoroutine(CheckOnLand());
                break;
            }
            yield return null;
        }
    }

    IEnumerator CheckOnLand()
    {
        while (true)
        {
            if (rb.linearVelocity.y > -0.1f)
            {
                Utility.SetAnimations("OnLand", ParameterType.Trigger);
                IsJumping = false;
                break;
            }
            yield return null;
        }
    }

    private void Animate(Eanimation animation, bool isTrue = true)
    {
        string animationName = animation.ToString();

        if (animationName.StartsWith("On")) 
            Utility.SetAnimations(animation.ToString(), ParameterType.Trigger);
        else if (animationName.StartsWith("Is"))
            Utility.SetAnimations(animation.ToString(), ParameterType.Bool, isTrue);
    }
}
