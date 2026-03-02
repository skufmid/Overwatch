using System;
using UnityEngine;

public abstract class _Character : MonoBehaviour
{
    // Todo : 체력, 피격 이벤트, 피격시 UI, 이펙트, 소리
    public Action<int> OnHit;
    public Action OnHpChanged;

    private Animator anim;

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
        anim = GetComponent<Animator>();
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
        anim.SetTrigger("OnHit");

        OnHit?.Invoke(damage);
        OnHpChanged?.Invoke();
    }

    public void Die()
    {
        Debug.Log("HP: 0이하로 사망");
        anim.SetTrigger("OnDie");
    }

    public void DestroySelf()
    {
        Destroy(gameObject, 1f);
    }
}
