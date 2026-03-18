using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI<T> : MonoBehaviour where T : ISkill
{
    Image image;
    TextMeshProUGUI cooldownText;
    T t;

    private void Awake()
    {
        image = GetComponent<Image>();
        cooldownText = GetComponentInChildren<TextMeshProUGUI>();
        
        t = GameManager.Instance.Player.GetComponent<T>();
    }

    private void OnEnable()
    {
        t.OnTimerChanged += UpdateTimer;
        t.OnChargeChanged += UpdateCharge;
    }

    private void OnDisable()
    {
        t.OnTimerChanged -= UpdateTimer;
        t.OnChargeChanged -= UpdateCharge;
    }

    private void UpdateTimer(int timer)
    {
        cooldownText.text = timer > 0 ? timer.ToString() : string.Empty;
    }

    private void UpdateCharge(int charge)
    {
        image.color = charge > 0 ? Color.white : Utility.disableColor;
    }
}
