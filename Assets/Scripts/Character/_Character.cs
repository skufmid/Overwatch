using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public abstract class _Character : MonoBehaviour
{
    // Todo : 체력, 피격 이벤트, 피격시 UI, 이펙트, 소리
    public Action<int> OnHit;
    public Action OnHpChanged;

    private Animator worldModelAnim;
    private Animator viewModelAnim;
    private Rig rig;

    public int MaxHP { get; protected set; } = 100;

    private int hp;
    public int HP {
        get
        {
            return hp;
        }
        protected set
        {
            hp = value;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public int Armor { get; protected set; } = 0;

    protected virtual void Awake()
    {
        worldModelAnim = GetComponent<Animator>();
        viewModelAnim = GetComponentInChildren<Camera>()
            ?.GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
    }

    protected void Start()
    {
        HP = MaxHP;
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = damage - Armor;
        HP -= finalDamage;
        Debug.Log($"HP: {HP}\t finalDamage: {damage} - {Armor} = {finalDamage}");
        Utility.SetTrigger(worldModelAnim, viewModelAnim, "OnHit");

        OnHit?.Invoke(damage);
        OnHpChanged?.Invoke();
    }

    public void Die()
    {
        Debug.Log("HP: 0이하로 사망");
        rig.weight = 0f;
        Utility.SetTrigger(worldModelAnim, viewModelAnim, "OnDie");
    }

    public void DestroySelf()
    {
        Destroy(gameObject, 1f);
    }
}
