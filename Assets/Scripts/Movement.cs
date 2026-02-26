using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator anim;

    const float DEFAULT_MOVE_SPEED = 3f;
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

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public Vector3 MoveDirection { get; set; } = Vector3.zero;
    public Vector3 MoveLocalDirection { get; set;} = Vector3.zero;

    void Update()
    {
        transform.position += 
            MoveDirection * MoveSpeed * Time.deltaTime;

        anim.SetFloat(
            "Horizontal", MoveLocalDirection.x * MoveSpeed / DEFAULT_RUN_SPEED, 0.2f, Time.deltaTime);
        anim.SetFloat(
            "Vertical", MoveLocalDirection.z * MoveSpeed / DEFAULT_RUN_SPEED, 0.2f, Time.deltaTime);
    }
}
