using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerCharacter Player { get; private set; }
    public Camera FPS_Cam => fpsCam;
    public Camera TPS_Cam => tpsCam;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;

    public Camera MainCam { get; private set; }

    public Action OnSettings;
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
    }


    private void Start()
    {
        // 마우스 안보이게 하기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDisplay();
        }

        void ToggleDisplay()
        {
            bool isFPS = fpsCam.enabled;

            fpsCam.enabled = !isFPS;
            tpsCam.enabled = isFPS;

            MainCam = fpsCam.enabled ? fpsCam : tpsCam;
        }
    }
}
