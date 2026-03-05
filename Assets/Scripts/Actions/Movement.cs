using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator worldModelAnim;
    private Animator viewModelAnim;

    const float DEFAULT_WALK_SPEED = 3f;
    const float DEFAULT_RUN_SPEED = 8f;

    float walkSpeed;
    float runSpeed;

    public float MoveSpeed { get; set; }
    private bool isRunning = false;
    public bool IsRunning {
        get { return isRunning; }
        set
        {
            MoveSpeed = value ? runSpeed : walkSpeed;
            isRunning = value;
        }
    }

    public Movement()
    {
        walkSpeed = DEFAULT_WALK_SPEED;
        runSpeed = DEFAULT_RUN_SPEED;

        MoveSpeed = DEFAULT_WALK_SPEED;
    }

    private void Awake()
    {
        worldModelAnim = GetComponent<Animator>();
        viewModelAnim = GetComponentInChildren<Camera>()
            ?.GetComponentInChildren<Animator>();
    }

    public Vector3 MoveDirection { get; set; } = Vector3.zero;
    public Vector3 MoveRelativeDirection { get; set;} = Vector3.zero;

    void Update()
    {
        transform.position += 
            MoveDirection * MoveSpeed * Time.deltaTime;

        Animate();
    }

    void Animate()
    {
        Utility.SetFloat(
            worldModelAnim, viewModelAnim, "Horizontal",
            MoveRelativeDirection.x * MoveSpeed / runSpeed, 0.2f, Time.deltaTime);
        Utility.SetFloat(
            worldModelAnim, viewModelAnim, "Vertical",
            MoveRelativeDirection.z * MoveSpeed / runSpeed, 0.2f, Time.deltaTime);
    }
}
