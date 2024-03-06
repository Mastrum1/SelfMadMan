
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartSlide;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndSlide;
    #endregion

    private PlayerController _mPlayerController;
    private Camera _mMainCam;

    private static InputManager instance = null;
    public static InputManager Instance => instance;
    public bool isDragging;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        _mPlayerController = new PlayerController();
        _mMainCam = Camera.main;
        isDragging = false;
    }


    public void SetCamera()
    {
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
        isDragging = true;
        if (OnStartSlide != null)
        {
            Vector3 worldPos = PrimaryPos();
            OnStartSlide(worldPos, (float)context.startTime);
        }
    }

    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        isDragging = false;
        if (OnEndSlide != null)
        {
            Vector3 worldPos = PrimaryPos();
            OnEndSlide(worldPos, (float)context.time);
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
