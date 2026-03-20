using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Flash : SkillBase
{
    private Transform fpsCameraTransform;

    const float DEFAULT_FLASH_DISTANCE = 4f;
    float flash_distance;

    public override string Name => "Flash";
    public override float DefaultInterval => 2.5f;
    public override int DefaultMaxCharge => 3;

    protected override void Awake()
    {
        base.Awake();

        flash_distance = DEFAULT_FLASH_DISTANCE;
        fpsCameraTransform = GameManager.Instance.FPS_Cam.transform;
    }

    protected override void Execute(object context)
    {
        Vector3 dir = (Vector3)context;
        HandleFlash(dir);

        StartCoolDown();
    }

    public void HandleFlash(Vector3 flashDirection)
    {
        if (!Mathf.Approximately(flashDirection.magnitude, 1f))
            Debug.LogWarning("Flash direction should be normalized. Flash may not work as expected.");

        Vector3 targetPosition = transform.position + flashDirection * flash_distance;
        transform.position = GetNearestNavMeshPoint(targetPosition, flash_distance);
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
}
