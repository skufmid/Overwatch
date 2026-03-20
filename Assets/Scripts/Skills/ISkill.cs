using System;
using System.Collections;
using UnityEngine;

public interface ISkill
{
    string Name { get; }
    float DefaultInterval { get; }
    float Interval { get; }
    int DefaultMaxCharge { get; }
    int MaxCharge { get; }
    int CurCharge { get; }
    float Timer { get; } // 0���ϸ� ��ų ��� ����
    int CurrentTimer { get; }

    Action<int> OnChargeChanged { get; set; }
    Action<int> OnTimerChanged { get; set; }
}
