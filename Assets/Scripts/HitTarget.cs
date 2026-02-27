using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitTarget : MonoBehaviour
{
    public void GetHit(float damage)
    {
        Debug.Log($"{gameObject.name} hit! Damage: {damage}");
    }
}
