using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float JumpForce { get; set; } = 5f;
    public bool IsJumping { get; set; } = false;

    private Rigidbody rb;
    private Animator worldModelAnim;
    private Animator viewModelAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        worldModelAnim = GetComponent<Animator>();
        viewModelAnim = GetComponentInChildren<Camera>()
            ?.GetComponentInChildren<Animator>();
    }

    public void HandleJump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Utility.SetTrigger(worldModelAnim, viewModelAnim, "OnJump");

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
                Utility.SetTrigger(worldModelAnim, viewModelAnim, "OnLand");
                IsJumping = false;
                break;
            }
            yield return null;
        }
    }
}
