using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    private Movement movement;
    private Rotation rotation;
    private Jumping jumping;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 rotateInput = Vector2.zero;

    public float RotateSensitivity { get; set; } = 10f;

    public bool IsGrounded
    {
        get
        {
            return Physics.CheckSphere(
                transform.position, GROUND_CHECK_DISTANCE, GROUND_LAYER);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, GROUND_CHECK_DISTANCE);
    }

    const float GROUND_CHECK_DISTANCE = 0.4f;
    const int GROUND_LAYER = 1 << 6;
    const float MIN_CAMERA_X = -85f;
    const float MAX_CAMERA_X = 45f;

    [SerializeField] Transform cameraTransform;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        movement = gameObject.AddComponent<Movement>();
        rotation = gameObject.AddComponent<Rotation>();
        jumping = gameObject.AddComponent<Jumping>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded && !jumping.IsJumping)
        {
            movement.IsRunning = true;
        }
        else if (context.canceled)
        {
            movement.IsRunning = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            rotateInput = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded && !jumping.IsJumping)
        {
            jumping.HandleJump();
        }
    }


    private void Update()
    {
        HandleMove();
        HandleRotate();

        //CheckFalling();
    }

    private void HandleMove()
    {
        Vector3 cameraRight = cameraTransform.right;
        Vector3 cameraForward = cameraTransform.forward;

        cameraRight.y = 0;
        cameraForward.y = 0;
        cameraRight.Normalize();
        cameraForward.Normalize();

        movement.MoveDirection =
            (moveInput.x * cameraRight + moveInput.y * cameraForward).normalized;
        movement.MoveLocalDirection =
            (moveInput.x * Vector3.right + moveInput.y * Vector3.forward);
    }

    private void HandleRotate()
    {
        rotation.RotateDirection =
            new Vector3(0, rotateInput.x, 0) * RotateSensitivity;

        RotateCameraX(-rotateInput.y * RotateSensitivity);
    }

    private void RotateCameraX(float value)
    {
        Vector3 currentRotation = cameraTransform.localEulerAngles;
        float newXRotation = currentRotation.x + (value * Time.deltaTime);

        if (newXRotation > 180)
            newXRotation -= 360;

        newXRotation = Mathf.Clamp(newXRotation, MIN_CAMERA_X, MAX_CAMERA_X);

        cameraTransform.localEulerAngles = new Vector3(newXRotation, currentRotation.y, currentRotation.z);
    }

    //private void CheckFalling()
    //{
    //    if (rb.linearVelocity.y < -0.1f)
    //    {
    //        Animate(Eanimation.OnFall);
    //        return;
    //    }

    //    // 공중인데 점핑중이면 점프 애니메이션 실행 아니면 추락 애니메이션 실행
    //    Animate(jumping.IsJumping ? Eanimation.OnJump : Eanimation.OnFall);
    //}

    //private void Animate(Eanimation animation, bool isTrue = true)
    //{
    //    string animationName = animation.ToString();

    //    if (animationName.StartsWith("On")) anim.SetTrigger(animation.ToString());
    //    else if (animationName.StartsWith("Is")) anim.SetBool(animation.ToString(), isTrue);
    //}
    
}
