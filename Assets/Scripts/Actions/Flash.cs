using System.Collections;
using Unity.Loading;
using UnityEngine;

public class Flash : MonoBehaviour
{
    const float DEFAULT_FLASH_DISTANCE = 10f;
    const float DEFAULT_FLASH_INTERVAL = 3f;

    float flash_distance;
    float flash_interval;
    float flash_timer = 0f; // 0이하면 flash 가능

    public Flash()
    {
        flash_distance = DEFAULT_FLASH_DISTANCE;
        flash_interval = DEFAULT_FLASH_INTERVAL;
    }

    public void HandleFlash(Vector3 flashDirection)
    {
        if (flash_timer > 0f)
        {
            Debug.Log("Flash is on cooldown. Time remaining: " + flash_timer.ToString("F2") + " seconds.");
            return;
        }

        if (!Mathf.Approximately(flashDirection.magnitude, 1f))
            Debug.LogWarning("Flash direction should be normalized. Flash may not work as expected.");

        StartCoroutine(CoFlashTimer());
        transform.position += flashDirection * flash_distance;
    }

    IEnumerator CoFlashTimer()
    {
        flash_timer = flash_interval;
        while (flash_timer > 0)
        {
            flash_timer -= Time.deltaTime;
            yield return null;
        }
    }
}
