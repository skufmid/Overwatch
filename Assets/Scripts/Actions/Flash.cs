using Unity.Loading;
using UnityEngine;

public class Flash : MonoBehaviour
{
    const float DEFAULT_FLASH_DISTANCE = 10f;
    const float DEFAULT_FLASH_COOLDOWN = 3f;

    float flash_distance;
    float flash_cooldown;

    public Flash()
    {
        flash_distance = DEFAULT_FLASH_DISTANCE;
        flash_cooldown = DEFAULT_FLASH_COOLDOWN;
    }

    public void HandleFlash(Vector3 flashDirection)
    {
        if (!Mathf.Approximately(flashDirection.magnitude, 1f))
            Debug.LogWarning("Flash direction should be normalized. Flash may not work as expected.");

        transform.position += flashDirection * flash_distance;
    }
}
