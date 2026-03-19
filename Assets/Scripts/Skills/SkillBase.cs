using System;
using System.Collections;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract string Name { get; }
    public abstract float DefaultInterval { get; }
    public abstract int DefaultMaxCharge { get; }
    public float Interval { get; private set; }
    public int MaxCharge { get; private set; }
    public int CurCharge { get; private set; }
    public float Timer { get; private set; } // 0이하면 스킬 충전
    public int CurrentTimer { get; private set; }

    public Action<int> OnChargeChanged { get; set; }
    public Action<int> OnTimerChanged { get; set; }

    Coroutine coTimer;

    protected virtual void Awake()
    {
        Interval = DefaultInterval;
        MaxCharge = DefaultMaxCharge;
        CurCharge = MaxCharge;
    }

    public void Use()
    {
        if (CurCharge <= 0) return;

        OnChargeChanged?.Invoke(--CurCharge);
        Execute();
    }

    protected void StartCoolDown()
    {
        Debug.Log("StartCoolDown");
        if (coTimer == null)
            coTimer = StartCoroutine(CoTimer());
    }

    protected abstract void Execute();

    protected IEnumerator CoTimer()
    {
        Debug.Log("Start CoTimer");
        Timer = 0;

        while (CurCharge < MaxCharge)
        {
            Timer += Interval;
            while (Timer > 0)
            {
                Timer -= Time.deltaTime;
                int timer = Mathf.CeilToInt(Timer);
                if (CurrentTimer != timer)
                {
                    CurrentTimer = timer;
                    OnTimerChanged?.Invoke(CurrentTimer);
                }
                yield return null;
            }

            OnChargeChanged?.Invoke(++CurCharge);
        }

        coTimer = null;
        yield return null;
    }
}
