using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Vector2 moveInput = Vector2.zero;
    [SerializeField] Transform cameraTransform;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Vector3 cameraRight = cameraTransform.right;
        Vector3 cameraForward = cameraTransform.forward;

        cameraRight.y = 0;
        cameraForward.y = 0;
        cameraRight.Normalize();
        cameraForward.Normalize();

        movement.MoveDirection = (moveInput.x * cameraRight + moveInput.y * cameraForward).normalized;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }


}
