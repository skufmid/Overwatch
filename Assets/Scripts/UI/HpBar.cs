using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HpBar : MonoBehaviour
{
    _Character character;
    Slider slider;

    private void Awake()
    {
        character = transform.root.GetComponent<_Character>();
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        character.OnHpChanged += UpdateHPBar;
    }

    private void OnDisable()
    {
        character.OnHpChanged -= UpdateHPBar;
    }

    private void UpdateHPBar()
    {
        slider.value = (float) character.HP / character.MaxHP;
    }
}
