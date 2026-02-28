using UnityEngine;

public class EnemyCharacter : _Character
{
    protected override void Awake()
    {
        base.Awake();
        MaxHP = 5000;
        Armor = 3;
    }
}
