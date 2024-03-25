
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartSlide;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndSlide;
    #endregion

    private static InputManager instance = null;
    public static InputManager Instance => instance;

    [SerializeField, Range(0f, 1f)] private float _mDirectionTreshold = .9f;

    [SerializeField] private float _mMinimumDist = .3f;

    private bool _mHold;

    [SerializeField] private bool _mEnableHold = true;

    [SerializeField] private bool _mEnableSlide = true;

    [SerializeField] private bool _mEnableTap = true;

    //[SerializeField] private 

    private Vector3 _mStartTouchPos;

    private Vector3 _mEndTouchPos;

    [SerializeField] private float _mHoldTiming;

    private float _mStartTiming;



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
    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (_mStartTouchPos == Vector3.zero)
            {
                _mStartTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                _mStartTiming = Time.time;
            }

            float holdTiming = Time.time - _mStartTiming;

            if (touch.phase == TouchPhase.Stationary && holdTiming >= _mHoldTiming && _mEnableHold)
            {
                Hold();
            }

            if (touch.phase == TouchPhase.Began && !_mEnableSlide)
            {
                Tap();
            }

            if (touch.phase == TouchPhase.Ended && _mEnableSlide)
            {
                _mEndTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= 0.5)
                {
                    Slide();
                }
                else if (!_mHold && _mEnableTap)
                {
                    Tap();
                }

                _mStartTouchPos = Vector3.zero;
                _mStartTiming = Time.time;
                _mHold = false;
            }

        }
    }

    public void Slide()
    {
        Vector3 direction = _mEndTouchPos - _mStartTouchPos;
        Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
        Debug.Log("Slide");

        if (Vector2.Dot(Vector2.right, direction2d) > _mDirectionTreshold)
        {
            Debug.Log("Right");

            //OnSwipeRight();
        }
        else if (Vector2.Dot(Vector2.left, direction2d) > _mDirectionTreshold)
        {
            Debug.Log("Left");

            //OnSwipeLeft();
        }
        else if (Vector2.Dot(Vector2.up, direction2d) > _mDirectionTreshold)
        {
            //OnSwipeUp();
            Debug.Log("Up");
        }
        else if (Vector2.Dot(Vector2.down, direction2d) > _mDirectionTreshold)
        {
            //OnSwipeDown();
            Debug.Log("Down");
        }
    }

    public void Tap()
    {
        Debug.Log("Tap");
    }

    public void Hold()
    {
        Debug.Log("hold");
        _mHold = true;
    }

}
