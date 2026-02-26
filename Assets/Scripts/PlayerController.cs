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
    private Shooting shooting;

    private Transform spine;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 rotateInput = Vector2.zero;

    public float RotateSensitivity { get; set; } = 10f;

    private float cameraXRotation = 0f;

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
        shooting = gameObject.AddComponent<Shooting>();
    }

    private void Start()
    {
        spine = anim.GetBoneTransform(HumanBodyBones.Spine);
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            bool shootInput = context.ReadValueAsButton();
            shooting.HandleShoot(shootInput);
        }
    }


    private void Update()
    {
        HandleMove();

        //CheckFalling();
    }

    private void LateUpdate()
    {
        HandleRotate();
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
        cameraXRotation += value * Time.deltaTime;
        cameraXRotation = Mathf.Clamp(cameraXRotation, MIN_CAMERA_X, MAX_CAMERA_X);

        cameraTransform.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);
        spine.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);
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
