using System;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Animator PlayerWorldModelAnim;
    public static Animator PlayerViewModelAnim;
    public static Animator[] Anims => new [] { PlayerWorldModelAnim , PlayerViewModelAnim };

    public static Color32 darkColor = new Color32(30, 30, 30, 200);

    public static void SetAnimations<T>(string parameterName, ParameterType type, T value)
    {
        int paramHash = Animator.StringToHash(parameterName);

        foreach (var anim in Anims)
        {
            if (anim == null) continue;

            switch (type)
            {
                case ParameterType.Float:
                    anim.SetFloat(paramHash, Convert.ToSingle(value));
                    break;
                case ParameterType.Bool:
                    anim.SetBool(paramHash, Convert.ToBoolean(value));
                    break;
                case ParameterType.Int:
                    anim.SetInteger(paramHash, Convert.ToInt32(value));
                    break;
                case ParameterType.Trigger:
                    anim.SetTrigger(paramHash);
                    Debug.LogWarning("Trigger parameters do not require a value. Use the SetAnimations method overload that does not accept a value for trigger parameters.");
                    break;
            }
        }
    }

    public static void SetAnimations(string parameterName, ParameterType type = ParameterType.Trigger)
    {
        Debug.Log("Setting trigger parameter: " + parameterName);
        if (type != ParameterType.Trigger)
        {
            Debug.LogError("This overload is only for trigger parameters. Please use the SetAnimations method that accepts a value for non-trigger parameters.");
            return;
        }

        int paramHash = Animator.StringToHash(parameterName);

        foreach (var anim in Anims)
        {
            if (anim != null) anim.SetTrigger(paramHash);
            else Debug.LogWarning("Animator reference is null. Ensure that the PlayerWorldModelAnim and PlayerViewModelAnim are properly assigned in the Awake method.");
        }
    }

    private void Awake()
    {
        if (GameManager.Instance.Player == null)
        {
            Debug.LogError("PlayerCharacter not found in the scene.");
        }
        PlayerWorldModelAnim = GameManager.Instance.Player.GetComponent<Animator>();
        if (PlayerWorldModelAnim == null) Debug.LogError("PlayerWorldModelAnim not found. Ensure that the PlayerCharacter has an Animator component.");
        PlayerViewModelAnim = GameManager.Instance.Player.GetComponentInChildren<Camera>()
            .GetComponentInChildren<Animator>();
        if (PlayerViewModelAnim == null) Debug.LogError("PlayerViewModelAnim not found. Ensure that the PlayerCharacter's Camera has a child with an Animator component.");
    }
}

public enum Eanimation
{
    OnJump,
    OnFall,
    OnLand
}

public enum ParameterType
{
    Float,
    Bool,
    Int,
    Trigger
}