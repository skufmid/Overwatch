using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float JumpForce { get; set; } = 5f;
    public bool IsGrounded { get; set; } = false;
    public bool IsJumping { get; set; } = false;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsJumping && IsGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsJumping = true;
        }
    }
}
