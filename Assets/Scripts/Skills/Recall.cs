using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Recall : SkillBase
{
    public struct StatusInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public StatusInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }

    private PlayerController playerController;

    private LinkedList<StatusInTime> pointsInTime = new LinkedList<StatusInTime>();
    private float recordTime = 3f;
    private WaitForSeconds fixedDeltaTime3 = new WaitForSeconds(0.02f * 3);

    private bool isRewinding = false;
    public bool IsRewinding {
        get { return isRewinding; }

        set
        {
            isRewinding = value;
            if (isRewinding)
            {
                playerController.IsMovable = false;
                playerController.IsRotatable = false;
                playerController.IsShootable = false;
            }
            else
            {
                playerController.IsMovable = true;
                playerController.IsRotatable = true;
                playerController.IsShootable = true;

                StartCoroutine(CoRecord());
            }
        }
    }

    private int maxStorage;

    public override string Name => "Recall";
    public override float DefaultInterval => 4f;
    public override int DefaultMaxCharge => 1;

    [SerializeField] private Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    Coroutine fadeCoroutine;

    protected override void Awake()
    {
        base.Awake();

        playerController = GetComponent<PlayerController>();

        maxStorage = Mathf.RoundToInt(recordTime / (Time.fixedDeltaTime * 3));

        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
        }
    }

    private void Start()
    {
        StartCoroutine(CoRecord());
    }

    protected override void Execute()
    {
        StartCoroutine(CoRewind());
    }

    IEnumerator CoRecord()
    {
        while (!IsRewinding)
        {
            if (pointsInTime.Count >= maxStorage)
            {
                pointsInTime.RemoveFirst();
            }
            pointsInTime.AddLast(new StatusInTime(transform.position, transform.rotation));

            yield return fixedDeltaTime3;
        }
    }

    IEnumerator CoRewind()
    {
        IsRewinding = true;

        // ������ Fade ȿ���� ���� ���̾��ٸ� �����ϰ� ���� ����
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); }
        fadeCoroutine = StartCoroutine(CoFadeEffect(-100f, 2f));

        while (pointsInTime.Count > 0)
        {
            StatusInTime point = pointsInTime.Last.Value;

            transform.position = point.position;
            transform.rotation = point.rotation;

            pointsInTime.RemoveLast();

            yield return new WaitForFixedUpdate();
        }

        IsRewinding = false;

        // ������ Fade ȿ���� ���� ���̾��ٸ� �����ϰ� ���� ����
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); }
        fadeCoroutine = StartCoroutine(CoFadeEffect(0f, 8f));

        StartCoolDown();
    }

    IEnumerator CoFadeEffect(float targetValue, float transitionSpeed)
    {
        float startValue = colorAdjustments.saturation.value;
        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * transitionSpeed;
            // Lerp�� �̿��� �ε巴�� �� ����
            colorAdjustments.saturation.value = Mathf.Lerp(startValue, targetValue, time);
            yield return null;
        }

        colorAdjustments.saturation.value = targetValue;
    }
}
