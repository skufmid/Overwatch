using System;
using System.Collections;
using UnityEngine;

public interface ISkill
{
    float DefaultInterval { get; }
    float Interval { get; }
    float Timer { get;} // 0이하면 스킬 사용 가능
    int CurrentCount { get; }

    Action<bool> OnEnableSkill { get; set; }
    Action<int> OnCountChanged { get; set; }
}
