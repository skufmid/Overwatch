using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                StartCoroutine(Record());
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

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        maxStorage = Mathf.RoundToInt(recordTime / (Time.fixedDeltaTime * 3));
        Interval = DefaultInterval;
    }

    private void Start()
    {
        StartCoroutine(Record());
    }

    public void HandleRecall()
    {
        if (Timer > 0) return;
        StartCoroutine(Rewind());
    }

    IEnumerator Record()
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

    IEnumerator Rewind()
    {
        IsRewinding = true;
        Debug.Log("Rewinding");
        while (pointsInTime.Count > 0)
        {
            StatusInTime point = pointsInTime.Last.Value;

            transform.position = point.position;
            transform.rotation = point.rotation;

            pointsInTime.RemoveLast();

            yield return new WaitForFixedUpdate();
        }

        IsRewinding = false;
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
}
