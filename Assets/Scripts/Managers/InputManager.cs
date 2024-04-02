using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class InputManager : MonoBehaviour
{

    [SerializeField, Range(0f, 1f)] private float _mDirectionTreshold = 0.85f;

    [SerializeField, Range(0f, 0.6f)] private float _mDiagonalDirectionTreshold = 0.45f;

    [SerializeField] private float _mMinimumDist = .3f;

    [SerializeField] private float _mDragAndDropMinimumDist = .15f;

    [SerializeField] private float _mHoldTiming = 0.35f;

    private bool _mHold;

    private bool _mIsDraging;

    private GameObject _mSelectedObject;

    [SerializeField] private bool _mEnableHold = true;

    [SerializeField] private bool _mEnableSlide = true;

    [SerializeField] private bool _mEnableSlide4Dir = true;

    [SerializeField] private bool _mEnableSlide8Dir = false;

    [SerializeField] private bool _mEnableTapOnFingerDown = false;

    [SerializeField] private bool _mEnableTapOnFingerUp = true;

    [SerializeField] private bool _mEnableTapOnObject = true;

    [SerializeField] private bool _mEnableDragAndDrop = true;

    [SerializeField] private bool _mEnableGiroscope = false;

    [SerializeField] private bool _mEnableSelectable = false;

    [SerializeField] private bool _mEnableAccelerometer = false;

    [SerializeField] public UnityEvent _mOnTap;

    [SerializeField] public UnityEvent _mOnHold;

    [SerializeField] public UnityEvent _mOnTapObject;

    [SerializeField] public UnityEvent<Quaternion> _mOnGiroscope;

    [SerializeField] public UnityEvent<Vector3> _mOnAccelerometer;

    [Header("Slide dir Events")]

    [SerializeField] public UnityEvent _mOnSlideUp;

    [SerializeField] public UnityEvent _mOnSlideUpRight;

    [SerializeField] public UnityEvent _mOnSlideRight;

    [SerializeField] public UnityEvent _mOnSlideDown;

    [SerializeField] public UnityEvent _mOnSlideDownRight;

    [SerializeField] public UnityEvent _mOnSlideDownLeft;

    [SerializeField] public UnityEvent _mOnSlideLeft;

    [SerializeField] public UnityEvent _mOnSlideUpLeft;

    [Header("Slide Events")]

    [SerializeField] public UnityEvent<Vector2> _mOnDelta;

    [SerializeField] public UnityEvent<float> _mOnDistance;

    [SerializeField] public UnityEvent<Vector3> _mOnWorldFrom;

    [SerializeField] public UnityEvent<Vector3> _mOnWorldTo;

    [SerializeField] public UnityEvent<Vector3> _mOnWorldDelta;

    [SerializeField] public UnityEvent<Vector3, Vector3> _mOnWorldFromTo;

    private Vector3 _mStartTouchPos;

    private Vector3 _mEndTouchPos;

    private float _mStartTiming;

    void Start()
    {

        if (SystemInfo.supportsGyroscope && _mEnableGiroscope)
        {
            Input.gyro.enabled = true;
        }

    }

    private void Update()
    {
        if (_mEnableAccelerometer)
        {
            _mOnAccelerometer?.Invoke(Input.acceleration);
        }
        if (_mEnableGiroscope)
        {
            _mOnGiroscope?.Invoke(GyroToUnity(Input.gyro.attitude));
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (_mStartTouchPos == Vector3.zero)
            {
                _mStartTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                _mStartTiming = Time.time;
            }

            float holdTiming = Time.time - _mStartTiming;

            if (touch.phase == TouchPhase.Stationary && holdTiming >= _mHoldTiming && _mEnableHold && !_mIsDraging)
            {
                Hold();
            }

            if (touch.phase == TouchPhase.Moved && holdTiming >= _mHoldTiming && _mEnableDragAndDrop && Vector3.Distance(_mStartTouchPos, Camera.main.ScreenToWorldPoint(touch.position)) >= _mDragAndDropMinimumDist || _mIsDraging)
            {
                DragAndDrop(Camera.main.ScreenToWorldPoint(touch.position), touch);
                _mIsDraging = true;
            }

            if (touch.phase == TouchPhase.Began && _mEnableTapOnFingerDown)
            {
                Tap(touch);
            }

            if (_mEnableSelectable && _mSelectedObject == null)
            {
                SelectObject(touch);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _mEndTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= _mMinimumDist && _mEnableSlide4Dir && !_mIsDraging || Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= _mMinimumDist && _mEnableSlide8Dir && !_mIsDraging)
                {
                    SlideDir();
                }

                else if (!_mHold && _mEnableTapOnFingerUp && !_mIsDraging)
                {
                    Tap(touch);
                }

                if (_mEnableSlide && Vector3.Distance(_mStartTouchPos, _mEndTouchPos) >= _mMinimumDist)
                {
                    Slide();
                }

                if(_mEnableSelectable && _mSelectedObject != null)
                {
                    SelectableObject script = _mSelectedObject.GetComponent<SelectableObject>();
                    script.GetDeselected();
                    _mSelectedObject = null;
                }

                _mStartTouchPos = Vector3.zero;
                _mStartTiming = Time.time;
                _mHold = false;
                _mIsDraging = false;
                _mSelectedObject = null;
            }
        }
    }

    public void Slide()
    {
        var finalDelta = _mEndTouchPos - _mStartTouchPos;

        finalDelta = finalDelta.normalized;

        _mOnDelta?.Invoke(finalDelta);

        _mOnDistance?.Invoke(finalDelta.magnitude);

        var worldFrom = Camera.main.ScreenToWorldPoint(_mStartTouchPos);
        var worldTo = Camera.main.ScreenToWorldPoint(_mEndTouchPos);

        _mOnWorldFrom?.Invoke(worldFrom);


        _mOnWorldTo?.Invoke(worldTo);

        _mOnWorldDelta?.Invoke(worldTo - worldFrom);

        _mOnWorldFromTo?.Invoke(worldFrom, worldTo);
    }

    public void SlideDir()
    {
        Vector3 direction = _mEndTouchPos - _mStartTouchPos;
        Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;

        if (_mEnableSlide8Dir)
        {
            if (direction2d.x > _mDiagonalDirectionTreshold && direction2d.y > _mDiagonalDirectionTreshold)
            {
                _mOnSlideUpRight?.Invoke();
            }
            else if (-direction2d.x > _mDiagonalDirectionTreshold && -direction2d.y > _mDiagonalDirectionTreshold)
            {
                _mOnSlideDownLeft?.Invoke();
            }
            else if (-direction2d.x > _mDiagonalDirectionTreshold && direction2d.y > _mDiagonalDirectionTreshold)
            {
                _mOnSlideUpLeft?.Invoke();
            }
            else if (direction2d.x > _mDiagonalDirectionTreshold && -direction2d.y > _mDiagonalDirectionTreshold)
            {
                _mOnSlideDownRight?.Invoke();
            }
        }

        if (Vector2.Dot(Vector2.right, direction2d) > _mDirectionTreshold)
        {
            _mOnSlideRight?.Invoke();


        }
        else if (Vector2.Dot(Vector2.left, direction2d) > _mDirectionTreshold)
        {
            _mOnSlideLeft?.Invoke();

        }
        else if (Vector2.Dot(Vector2.up, direction2d) > _mDirectionTreshold)
        {
            _mOnSlideUp?.Invoke();

        }
        else if (Vector2.Dot(Vector2.down, direction2d) > _mDirectionTreshold)
        {
            _mOnSlideDown?.Invoke();
        }
    }

    public void Tap(Touch touch)
    {
        if (_mEnableTapOnObject)
        {
            if (_mSelectedObject == null)
                SelectObject(touch);

            if (_mSelectedObject != null && _mOnTapObject != null)
            {
                _mOnTapObject.Invoke();
            }
        }
        else
        {
            _mOnTap?.Invoke();
        }
    }


    public void Hold()
    {
        _mHold = true;
        _mOnHold?.Invoke();
    }

    public void DragAndDrop(Vector3 pos, Touch touch)
    {
        if(_mSelectedObject == null)
            SelectObject(touch);

        if (_mSelectedObject != null)
        {
            DragAndDropManager dragScript = _mSelectedObject.GetComponent<DragAndDropManager>();
            pos.z = 0;
            dragScript._mOnDragAndDrop?.Invoke(pos);
        }
    }

    public void SelectObject(Touch touch)
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), transform.TransformDirection(Vector3.forward), Mathf.Infinity);

        if (hit.collider != null)
        {
            SelectableObject script = hit.collider.GetComponent<SelectableObject>();
            if (script != null)
            {
                script.GetSelected();
                _mSelectedObject = hit.collider.gameObject;
            }
        }
        else
        {
            Debug.Log("notSelectable");
        }
        // Create a particle if hit
    }
    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
