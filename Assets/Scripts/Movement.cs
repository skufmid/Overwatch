using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MoveSpeed { get; set; } = 5f;
    public Vector3 MoveDirection { get; set; } = Vector3.zero;

    void Update()
    {
        transform.position += 
            MoveDirection * MoveSpeed * Time.deltaTime;
    }
}
