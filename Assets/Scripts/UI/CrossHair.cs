using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private float size = 10f;
    private float thickness = 2f;

    private void OnGUI()
    {
        float x = Screen.width / 2f;
        float y = Screen.height / 2f;

        GUI.DrawTexture(new Rect(x - thickness / 2, y - size, thickness, size * 2), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x - size, y - thickness / 2, size * 2, thickness), Texture2D.whiteTexture);
    }
}
