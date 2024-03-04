using UnityEngine;
using Cinemachine;

public class CinemachineCameraController : CinemachineExtension
{
    [SerializeField]
    private GameObject player;
    private InputManager inputManager;
    private CustomCharacterController customCharacterController;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        customCharacterController = player.GetComponent<CustomCharacterController>();
        base.Awake();
    }

    public void PositionCorrection() {
        startingRotation = player.transform.localRotation.eulerAngles;
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow) {
            if (stage == CinemachineCore.Stage.Aim) {
                if (startingRotation == null) startingRotation = player.transform.localRotation.eulerAngles;
                // currentLook is mouse delta. currentLook.x is same as Input.GetAxis("Mouse X"), currentLook.y is same as Input.GetAxis("Mouse Y")
                Vector2 currentLook = inputManager.GetCurrentLook();

                // startingRotation.x represents up/down camera rotation
                startingRotation.x -= currentLook.y * deltaTime * customCharacterController.ySensitivity;
                startingRotation.x = Mathf.Clamp(startingRotation.x, customCharacterController.cameraAngleUp, customCharacterController.cameraAngleDown);

                // startingRotation.y represents left/right camera rotation
                startingRotation.y += currentLook.x * deltaTime * customCharacterController.xSensitivity;

                state.RawOrientation = Quaternion.Euler(startingRotation.x, startingRotation.y, 0f);
            }
        }
    }
}