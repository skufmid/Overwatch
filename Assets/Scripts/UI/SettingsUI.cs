using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSettings += ToggleSettings;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnSettings -= ToggleSettings;
    }

    private void ToggleSettings()
    {
        canvas.enabled = !canvas.enabled;
    }
}
