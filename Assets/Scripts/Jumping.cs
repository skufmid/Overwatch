using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float JumpForce { get; set; } = 5f;
    public bool IsJumping { get; set; } = false;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HandleJump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        IsJumping = true;
        StartCoroutine(ResetJumping());
    }

    IEnumerator ResetJumping()
    {
        while (true)
        {
            if (_rigidbody.linearVelocity.y <= 0f)
            {
                IsJumping = false;
                break;
            }
            yield return null;
        }
    }
}
