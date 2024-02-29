
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private PlayerController _mPlayerController;
    private Camera _mMainCam;

    private void Awake()
    {
        _mPlayerController = new PlayerController();
        _mMainCam = Camera.main;
    }

    private void OnEnable()
    {
        _mPlayerController.Enable();
    }

    private void OnDisable()
    {
        _mPlayerController.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _mPlayerController.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx);
        _mPlayerController.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);

    }

    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            Vector3 position = _mPlayerController.Touch.PrimaryPos.ReadValue<Vector2>();
            position.z = _mMainCam.nearClipPlane;
            Vector3 worldPos = _mMainCam.ScreenToWorldPoint(position);
            OnStartTouch(worldPos, (float)context.startTime);
        }
    }

    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            Vector3 position = _mPlayerController.Touch.PrimaryPos.ReadValue<Vector2>();
            position.z = _mMainCam.nearClipPlane;
            Vector3 worldPos = _mMainCam.ScreenToWorldPoint(position);
            OnEndTouch(worldPos, (float)context.time);
        }
    }

    public Vector2 PrimaryPos()
    {
        Vector3 position = _mPlayerController.Touch.PrimaryPos.ReadValue<Vector2>();
        position.z = _mMainCam.nearClipPlane;
        Vector2 worldPos = _mMainCam.ScreenToWorldPoint(position);
        return worldPos;
    }
}
