using UnityEngine;

public class LookCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = GameManager.Instance.MainCam.transform.rotation;
    }
}
