using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

// implemented as singleton (requires a GameObject to be attached to)
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private PlayerInput _playerInput;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private PlayerControls playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerInput.ActivateInput();
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnCameraSwitch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            CinemachineVirtualCamera fpc = GameObject.Find("FPS Camera").GetComponent<CinemachineVirtualCamera>();
            // GameObject tpc = GameObject.Find("TPS Camera");
            fpc.m_Priority = fpc.m_Priority > 0 ? 0 : 100;
            fpc.gameObject.GetComponent<CinemachineCameraController>().PositionCorrection();
            Debug.Log("CameraSwitchEvent fired");
        }
    }

    // Возвращает mouseDelta (Input.GetAxis(Mouse))
    public Vector2 GetCurrentLook()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    // Возвращает Input.GetAxis(Horisontal и Vertical)
    public Vector2 GetCurrentMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public void DisableMovement()
    {
        playerControls.Player.Move.Disable();
    }

    public void EnableMovement()
    {
        playerControls.Player.Move.Enable();
    }
}