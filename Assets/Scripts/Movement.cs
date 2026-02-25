using UnityEngine;

public class Movement : MonoBehaviour
{
    const float DEFAULT_MOVE_SPEED = 4f;
    const float DEFAULT_RUN_SPEED = 8f;
    public float MoveSpeed { get; set; } = DEFAULT_MOVE_SPEED;
    private bool isRunning = false;
    public bool IsRunning {
        get { return isRunning; }
        set
        {
            MoveSpeed = value ? DEFAULT_RUN_SPEED : DEFAULT_MOVE_SPEED;
            isRunning = value;
        }
    }

    public Vector3 MoveDirection { get; set; } = Vector3.zero;

    void Update()
    {
        transform.position += 
            MoveDirection * MoveSpeed * Time.deltaTime;
    }
}
