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
        t.OnCountChanged += UpdateCount;
        t.OnEnableSkill += UpdateEnable;
    }

    private void OnDisable()
    {
        t.OnCountChanged -= UpdateCount;
        t.OnEnableSkill -= UpdateEnable;
    }

    private void UpdateCount(int count)
    {
        cooldownText.text = count > 0 ? count.ToString() : string.Empty;
    }

    private void UpdateEnable(bool isEnable)
    {
        image.color = isEnable ? Color.white : Utility.disableColor;
    }
}
