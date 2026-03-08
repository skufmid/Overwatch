using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerCharacter Player { get; private set; }
    public Camera FPS_Cam => fpsCam;
    public Camera TPS_Cam => tpsCam;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;

    public Camera MainCam { get; private set; }

    public float RotateSensitivity { get; private set; } = 10f;
    PlayerInput playerInput;

    public Action<bool> OnSettings;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Player = FindAnyObjectByType<PlayerCharacter>();
        MainCam = fpsCam;
        playerInput = Player.GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        OnSettings += ChangeCursorVisibility;
    }
    
    private void OnDisable()
    {
        OnSettings -= ChangeCursorVisibility;
    }

    private void Start()
    {
        ChangeCursorVisibility(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleView();
        }

        void ToggleView()
        {
            bool isFPS = fpsCam.enabled;

            fpsCam.enabled = !isFPS;
            tpsCam.enabled = isFPS;

            MainCam = fpsCam.enabled ? fpsCam : tpsCam;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangePlayerInput(bool toUI)
    {
        playerInput.SwitchCurrentActionMap(toUI ? "UI" : "Player");
    }

    public void ChangeCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ChangeRotateSensitivity(float newSensitivity)
    {
        RotateSensitivity = newSensitivity;
    }
}
