using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MoveSpeed { get; set; } = 5f;
    public Vector3 MoveDirection { get; set; } = Vector3.zero;



    // Update is called once per frame
    void Update()
    {
        transform.position += MoveSpeed * MoveDirection * Time.deltaTime;
    }
}
