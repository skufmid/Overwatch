using UnityEngine;

public class EnemyCharacter : _Character
{
    protected override void Awake()
    {
        base.Awake();
        MaxHP = 500;
        Armor = 3;
    }
}
