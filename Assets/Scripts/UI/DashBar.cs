//using UnityEngine;
//using UnityEngine.UI;

//public class DashBar : MonoBehaviour
//{
//    [SerializeField] int num;
//    Image dashImage;
//    Flash flash;

//    private void Awake()
//    {
//        dashImage = GetComponent<Image>();
//    }
//    private void OnEnable()
//    {
//        if (flash == null)
//        {
//            flash = GameManager.Instance.Player.GetComponent<Flash>();
//        }

//        flash.OnFlashCountChanged += UpdateDashBar;
//    }

//    private void OnDisable()
//    {
//        flash.OnFlashCountChanged -= UpdateDashBar;
//    }

//    private void UpdateDashBar(int count)
//    {
//        dashImage.color =
//            count >= num ? 
//            Color.white :
//            Utility.darkColor;
//    }
//}
