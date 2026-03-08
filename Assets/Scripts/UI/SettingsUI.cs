using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    Canvas canvas;
    private int lastToggleFrame = -1;

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

    private void ToggleSettings(bool isOpen)
    {
        if (canvas.enabled == isOpen
            || Time.frameCount == lastToggleFrame) return;

        canvas.enabled = isOpen;
        lastToggleFrame = Time.frameCount;
        GameManager.Instance.ChangePlayerInput(isOpen);
    }
}
