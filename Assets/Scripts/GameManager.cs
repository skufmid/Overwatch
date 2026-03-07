using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerCharacter Player { get; private set; }

    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;

    public Camera MainCam { get; private set; }

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
