
using Lean.Touch;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class InputManager : MonoBehaviour
{

    [SerializeField, Range(0f, 1f)] private float _mDirectionTreshold = 0.75f;

    [SerializeField] private float _mMinimumDist = .3f;

    [SerializeField] private float _mHoldTiming = 0.35f;

    private bool _mHold;

    [SerializeField] private bool _mEnableHold = true;

    [SerializeField] private bool _mEnableSlide4Dir = true;

    [SerializeField] private bool _mEnableSlide8Dir = true;

    [SerializeField] private bool _mEnableTapOnFingerDown = true;

    [SerializeField] private bool _mEnableTapOnFingerUp = false;

    [SerializeField] private UnityEvent _mOnTap;

    [SerializeField] private UnityEvent _mOnHold;

    [Header("Slide Events")]

    [SerializeField] private UnityEvent _mOnSlideUp;
       
    [SerializeField] private UnityEvent _mOnSlideUpRight;

    [SerializeField] private UnityEvent _mOnSlideRight;

    [SerializeField] private UnityEvent _mOnSlideDown;

    [SerializeField] private UnityEvent _mOnSlideDownRight;

    [SerializeField] private UnityEvent _mOnSlideDownLeft;

    [SerializeField] private UnityEvent _mOnSlideLeft;

    [SerializeField] private UnityEvent _mOnSlideUpLeft;

    private Vector3 _mStartTouchPos;

    private Vector3 _mEndTouchPos;

    private float _mStartTiming;

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

            if (touch.phase == TouchPhase.Began && _mEnableTapOnFingerDown)
            {
                Tap();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _mEndTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= 0.5 && _mEnableSlide4Dir || Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= 0.5 && _mEnableSlide8Dir)
                {
                    Slide();
                }
                else if (!_mHold && _mEnableTapOnFingerUp)
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

        if (_mEnableSlide8Dir)
        {

        }

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
        if (_mOnTap != null) _mOnTap.Invoke();
    }


    public void Hold()
    {
        Debug.Log("hold");
        _mHold = true;
        if (_mOnHold != null) _mOnHold.Invoke();
    }

}
