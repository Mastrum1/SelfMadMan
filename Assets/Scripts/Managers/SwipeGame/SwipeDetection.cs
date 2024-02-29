using System.Collections;
using UnityEngine;
using static InputManager;

public class SwipeDetection : MonoBehaviour
{
    #region Events
    public delegate void SwipeLeft();
    public event SwipeLeft OnSwipeLeft;
    public delegate void SwipeRight();
    public event SwipeRight OnSwipeRight;
    public delegate void SwipeUp();
    public event SwipeUp OnSwipeUp;
    public delegate void SwipeDown();
    public event SwipeDown OnSwipeDown;
    #endregion


    [SerializeField]
    private float _mMinimumDist = .1f;
    [SerializeField]
    private float _mMaximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float _mDirectionTreshold = .9f;

    private InputManager _mInputManager;

    private SwipeGameManager _mGameManager;

    private Vector2 _mStartPos;
    private float _mStartTime;
    private Vector2 _mEndPos;
    private float _mEndTime;

    private void Awake()
    {
        _mInputManager = InputManager.Instance;
        _mGameManager = GetComponent<SwipeGameManager>();
    }

    private void OnEnable()
    {
        _mInputManager.OnStartTouch += SwipeStart;
        _mInputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _mInputManager.OnStartTouch -= SwipeStart;
        _mInputManager.OnEndTouch -= SwipeEnd;

    }

    private void SwipeStart(Vector2 position, float time)
    {
        _mStartPos = position;
        _mStartTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _mEndPos = position;
        _mEndTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_mStartPos, _mEndPos) >= _mMinimumDist && (_mEndTime - _mStartTime) <= _mMaximumTime)
        {
            Debug.Log("Swipe detected");
            Debug.DrawLine(_mStartPos, _mEndPos, Color.red, 5f);
            Vector3 direction = _mEndPos - _mStartPos;
            Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2d);
        }
    }

    private void SwipeDirection(Vector2 dir)
    {
        if (Vector2.Dot(Vector2.right, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "Horizontal" || Vector2.Dot(Vector2.right, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            OnSwipeRight();
        }
        else if (Vector2.Dot(Vector2.left, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "Horizontal" || Vector2.Dot(Vector2.left, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            OnSwipeLeft();
        }
        else if (Vector2.Dot(Vector2.up, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "Vertical" || Vector2.Dot(Vector2.up, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            OnSwipeUp();
        }
        else if (Vector2.Dot(Vector2.down, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "Vertical" || Vector2.Dot(Vector2.down, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            OnSwipeDown();
        }
        else if (dir == new Vector2(1f, 1f))
        {

        }
    }
}
