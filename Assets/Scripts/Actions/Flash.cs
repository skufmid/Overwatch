using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Flash : MonoBehaviour
{
    const float DEFAULT_FLASH_DISTANCE = 4f;
    const float DEFAULT_FLASH_INTERVAL = 2.5f;
    const int DEFAULT_FLASH_MAX_COUNT = 3;

    float flash_distance;
    float flash_interval;
    int flash_max_count;

    int flash_count;
    public int Flash_count
    {   
        get { return flash_count; }
        set
        {
            flash_count = value;
            OnFlashCountChanged?.Invoke(flash_count);
        }
    }
    Coroutine counter;

    float flash_timer = 0f; // 0이하면 flash 하나 충전

    public Action<int> OnFlashCountChanged;

    public Flash()
    {
        flash_distance = DEFAULT_FLASH_DISTANCE;
        flash_interval = DEFAULT_FLASH_INTERVAL;
        flash_max_count = DEFAULT_FLASH_MAX_COUNT;

        Flash_count = flash_max_count;
    }

    public void HandleFlash(Vector3 flashDirection)
    {
        if (Flash_count == 0)
        {
            Debug.Log("Flash is on cooldown. Time remaining: " + flash_timer.ToString("F2") + " seconds.");
            return;
        }

        if (!Mathf.Approximately(flashDirection.magnitude, 1f))
            Debug.LogWarning("Flash direction should be normalized. Flash may not work as expected.");

        Vector3 targetPosition = transform.position + flashDirection * flash_distance;
        transform.position = GetNearestNavMeshPoint(targetPosition, flash_distance);

        Flash_count--;
        Debug.Log($"Flash Count : {Flash_count}");
        if (counter == null)
            counter = StartCoroutine(CoFlashTimer());
        
    }

    private Vector3 GetNearestNavMeshPoint(Vector3 targetPosition, float maxDistance)
    {
        NavMeshHit hit;

        int walkableAreaIndex = NavMesh.GetAreaFromName("Walkable");
        int mask = 1 << walkableAreaIndex;
        if (NavMesh.SamplePosition(targetPosition, out hit, maxDistance, mask))
        {
            return hit.position;
        }
        return transform.position; // 이동할 수 없다면 제자리 점멸
    }

    IEnumerator CoFlashTimer()
    {
        while (Flash_count < flash_max_count)
        {
            flash_timer = flash_interval;
            while (flash_timer > 0)
            {
                flash_timer -= Time.deltaTime;
                yield return null;
            }
            Flash_count++;
            Debug.Log($"Flash Count : {Flash_count}");
        }
        counter = null;
    }
}
