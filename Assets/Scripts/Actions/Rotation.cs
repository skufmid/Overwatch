using UnityEngine;

public class Rotation : MonoBehaviour
{
    const float SPINE_PITCH_MULTIPLIER = 0.3f;

    public Vector3 RotateDirection { get; set; } = Vector3.zero;
    public float RotateSensitivity => GameManager.Instance.RotateSensitivity;

    private Transform fpsCameraTransform;
    private Transform tpsCameraTransform;
    private Animator WorldModelAnim;
    private Transform spine;

    float rotationMultiplier;

    Quaternion initialCameraLocalRotation;
    Quaternion initialSpineRotation;

    float currentCameraPitch = 0f;
    float spineCameraPitchOffset = 0f;

    private void Awake()
    {
        WorldModelAnim = GetComponent<Animator>();

        spine = spine = WorldModelAnim.GetBoneTransform(HumanBodyBones.Spine);
    }

    private void Start()
    {
        fpsCameraTransform = GameManager.Instance.FPS_Cam.transform;
        tpsCameraTransform = GameManager.Instance.TPS_Cam.transform;

        initialCameraLocalRotation = fpsCameraTransform.localRotation;
        initialSpineRotation = spine.localRotation;
        currentCameraPitch = initialCameraLocalRotation.eulerAngles.x;

        spineCameraPitchOffset = currentCameraPitch - initialSpineRotation.eulerAngles.x;
        
    }

    void LateUpdate()
    {
        //SyncSpineToCamera();
        RotateBodyYaw();
        RotateCameraPitch();
    }

    private void OnAnimatorIK(int layerIndex)
    {
    }

    void RotateBodyYaw()
    {
        transform.Rotate(0, RotateDirection.y * RotateSensitivity * Time.deltaTime, 0);
    }

    void RotateCameraPitch()
    {
        float pitchDelta = RotateDirection.x * RotateSensitivity * Time.deltaTime;
        currentCameraPitch = Mathf.Clamp(currentCameraPitch + pitchDelta, -89f, 89f);

        fpsCameraTransform.localRotation = Quaternion.Euler(currentCameraPitch, initialCameraLocalRotation.y, initialCameraLocalRotation.z);
        tpsCameraTransform.localRotation = fpsCameraTransform.localRotation;
    }

    void SyncSpineToCamera()
    {
        spine.localRotation = 
            Quaternion.Euler(SPINE_PITCH_MULTIPLIER * currentCameraPitch + spineCameraPitchOffset, 0, 0)
            * spine.localRotation;
    }

}