using System;
using UnityEngine;

public class Utility : MonoBehaviour
{

    public static Color32 darkColor = new Color32(30, 30, 30, 200);
    public static Color32 disableColor = new Color32(100, 100, 100, 200);

    public static void SetFloat(
        Animator worldModelAnim, Animator viewModelAnim, string parameterName, float value, float dampTime, float deltaTime)
    {
        worldModelAnim?.SetFloat(parameterName, value, dampTime, deltaTime);
        viewModelAnim?.SetFloat(parameterName, value, dampTime, deltaTime);
    }

    public static void SetTrigger(
        Animator worldModelAnim, Animator viewModelAnim, string parameterName)
    {
        worldModelAnim?.SetTrigger(parameterName);
        viewModelAnim?.SetTrigger(parameterName);
    }
}

