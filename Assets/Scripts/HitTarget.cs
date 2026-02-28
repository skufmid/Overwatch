using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Collider))]
public class HitTarget : MonoBehaviour
{
    [SerializeField] ETargetType targetType;

    _Character character;

    private void Awake()
    {
        character = transform.root.GetComponent<_Character>();
        if (character == null)
            character = GetComponentInParent<_Character>();
    }

    public void GetHit(float damage)
    {
        Debug.Log("Å¸°Ý");
        float damageMultiplier = 1f;
        if (targetType == ETargetType.Null)
        {
            Debug.LogWarning($"{gameObject.name} has no target type assigned. Defaulting to 1x damage.");
        }
        else
        {
            switch (targetType)
            {
                case ETargetType.Head:
                    damageMultiplier = 2f;
                    break;
                case ETargetType.Chest:
                    damageMultiplier = 1f;
                    break;
                case ETargetType.Stomach:
                    damageMultiplier = 1f;
                    break;
                case ETargetType.Arms:
                    damageMultiplier = 0.75f;
                    break;
                case ETargetType.Legs:
                    damageMultiplier = 0.6f;
                    break;
            }
        }

        damage *= damageMultiplier;
        Debug.Log(targetType.ToString());
        character.TakeDamage((int)damage);
    }
}

enum ETargetType
{
    Null,
    Head,
    Chest,
    Stomach,
    Arms,
    Legs
}