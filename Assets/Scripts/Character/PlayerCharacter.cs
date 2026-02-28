using UnityEngine;

public class PlayerCharacter : _Character
{
    protected override void Awake()
    {
        base.Awake();
        MaxHP = 1000;
        Armor = 3;
    }
}
