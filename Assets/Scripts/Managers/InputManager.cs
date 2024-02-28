
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

    private PlayerController playerController;
    private Camera mainCam;

    private void Awake()
    {
        playerController = new PlayerController();
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx);
        playerController.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);

    }

    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            Vector3 position = playerController.Touch.PrimaryPos.ReadValue<Vector2>();
            position.z = mainCam.nearClipPlane;
            Vector3 worldPos = mainCam.ScreenToWorldPoint(position);
            OnStartTouch(worldPos, (float)context.startTime);
        }
    }

    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            Vector3 position = playerController.Touch.PrimaryPos.ReadValue<Vector2>();
            position.z = mainCam.nearClipPlane;
            Vector3 worldPos = mainCam.ScreenToWorldPoint(position);
            OnEndTouch(worldPos, (float)context.time);
        }
    }

    public Vector2 PrimaryPos()
    {
        Vector3 position = playerController.Touch.PrimaryPos.ReadValue<Vector2>();
        position.z = mainCam.nearClipPlane;
        Vector2 worldPos = mainCam.ScreenToWorldPoint(position);
        return worldPos;
    }
}
