using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 RotateDirection { get; set; } = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= 
            Quaternion.Euler(RotateDirection * Time.deltaTime);
    }
}
