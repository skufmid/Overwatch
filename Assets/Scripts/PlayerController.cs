using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Rotation rotation;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 rotateInput = Vector2.zero;

    public float RotateSensitivity { get; set; } = 10f;

    const float MIN_CAMERA_X = -85f;
    const float MAX_CAMERA_X = 45f;

    [SerializeField] Transform cameraTransform;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        rotation = GetComponent<Rotation>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotateInput = context.ReadValue<Vector2>();
        }
    }


    private void Update()
    {
        HandleMove();
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
    }

    private void HandleRotate()
    {
        rotation.RotateDirection =
            new Vector3(0, rotateInput.x, 0) * RotateSensitivity;

        RotateCameraX(-rotateInput.y * RotateSensitivity);
    }

    private void RotateCameraX(float value)
    {
        // 현재 회전값을 오일러 각도로 변환
        Vector3 currentRotation = cameraTransform.rotation.eulerAngles;

        // X축 회전값 계산
        float newXRotation = currentRotation.x + (value * Time.deltaTime);

        // eulerAngles는 0-360 범위이므로, -85에서 45 범위로 변환
        if (newXRotation > 180)
            newXRotation -= 360;

        // -85에서 45 범위로 제한
        newXRotation = Mathf.Clamp(newXRotation, -85f, 45f);

        // 회전 적용
        cameraTransform.rotation = Quaternion.Euler(newXRotation, currentRotation.y, currentRotation.z);
    }
}
