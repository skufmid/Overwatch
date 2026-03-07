using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Recall : MonoBehaviour, ISkill
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

    public float DefaultInterval => 4f;

    public float Interval { get; private set; }
    public float Timer { get; private set; } = 0; // 0이하면 recall 가능
    public int CurrentCount { get; private set; }

    public Action<bool> OnEnableSkill { get; set; }
    public Action<int> OnCountChanged { get; set; }

    [SerializeField] private Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    Coroutine fadeCoroutine;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        maxStorage = Mathf.RoundToInt(recordTime / (Time.fixedDeltaTime * 3));
        Interval = DefaultInterval;

        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
        }
    }

    private void Start()
    {
        StartCoroutine(CoRecord());
    }

    public void HandleRecall()
    {
        if (Timer > 0) return;
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

        // 이전에 Fade 효과가 진행 중이었다면 중지하고 새로 시작
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

        // 이전에 Fade 효과가 진행 중이었다면 중지하고 새로 시작
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); }
        fadeCoroutine = StartCoroutine(CoFadeEffect(0f, 8f));

        StartCoroutine(CoTimer());
    }

    IEnumerator CoTimer()
    {
        Timer = Interval;
        OnEnableSkill?.Invoke(false);

        while (Timer > 0)
        {
            Timer -= Time.deltaTime;
            int count = Mathf.CeilToInt(Timer);
            if (CurrentCount != count)
            {
                CurrentCount = count;
                OnCountChanged?.Invoke(CurrentCount);
            }
            yield return null;
        }

        Timer = 0;
        OnEnableSkill?.Invoke(true);
    }

    IEnumerator CoFadeEffect(float targetValue, float transitionSpeed)
    {
        float startValue = colorAdjustments.saturation.value;
        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * transitionSpeed;
            // Lerp를 이용해 부드럽게 값 변경
            colorAdjustments.saturation.value = Mathf.Lerp(startValue, targetValue, time);
            yield return null;
        }

        colorAdjustments.saturation.value = targetValue;
    }
}
