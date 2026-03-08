using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    Canvas canvas;
    Slider sensitivitySlider;
    private int lastToggleFrame = -1;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        sensitivitySlider = GetComponentInChildren<Slider>(); // 이후 slider 추가시 제대로 연결해야 함
    }

    private void Start()
    {
        sensitivitySlider.value = GameManager.Instance.RotateSensitivity;
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

    public void OnCloseButtonClicked()
    {
        GameManager.Instance.OnSettings?.Invoke(false);
    }

    public void OnSensitivitySliderChanged()
    {
        GameManager.Instance.ChangeRotateSensitivity(sensitivitySlider.value);
    }

    public void OnQuitButtonClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
