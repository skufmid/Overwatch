using UnityEngine;
using UnityEngine.UI;

public class FlashUIBar : MonoBehaviour
{
    [SerializeField] int num;
    Image dashImage;
    Flash flash;

    private void Awake()
    {
        dashImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        if (flash == null)
        {
            flash = GameManager.Instance.Player.GetComponent<Flash>();
        }

        flash.OnChargeChanged += UpdateFlashBar;
    }

    private void OnDisable()
    {
        flash.OnChargeChanged -= UpdateFlashBar;
    }

    private void UpdateFlashBar(int count)
    {
        dashImage.color =
            count >= num ?
            Color.white :
            Utility.darkColor;
    }
}
