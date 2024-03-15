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

    public delegate void SwipeLeftUp();
    public event SwipeLeftUp OnSwipeLeftUp;

    public delegate void SwipeRightUp();
    public event SwipeRightUp OnSwipeRightUp;

    public delegate void SwipeLeftDown();
    public event SwipeLeftDown OnSwipeLeftDown;

    public delegate void SwipeRightDown();
    public event SwipeRightDown OnSwipeRightDown;
    #endregion


    [SerializeField]
    private float _mMinimumDist = .1f;

    [SerializeField]
    private float _mMaximumTime = 1f;

    [SerializeField, Range(0f, 1f)]
    private float _mDirectionTreshold = .9f;

    [SerializeField, Range(0f, 0.6f)]
    private float _mDiagonalDirectionTreshold = 0.45f;

    private InputManager _mInputManager;
    [SerializeField]
    private SwipeGameManager _mGameManager;

    private Vector2 _mStartPos;
    private float _mStartTime;
    private Vector2 _mEndPos;
    private float _mEndTime;

    private void Awake()
    {
        _mInputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _mInputManager.OnStartSlide += SwipeStart;
        _mInputManager.OnEndSlide += SwipeEnd;
    }

    private void OnDisable()
    {
        _mInputManager.OnStartSlide -= SwipeStart;
        _mInputManager.OnEndSlide -= SwipeEnd;

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
        if (dir.x > _mDiagonalDirectionTreshold && dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "Diagonal" || dir.x > _mDiagonalDirectionTreshold && dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            Debug.Log("Up Right");
            OnSwipeRightUp();
        }
        else if (-dir.x > _mDiagonalDirectionTreshold && -dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "Diagonal" || -dir.x > _mDiagonalDirectionTreshold && -dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            Debug.Log("Down Left");
            OnSwipeLeftDown();
        }
        else if (-dir.x > _mDiagonalDirectionTreshold && dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "Diagonal" || -dir.x > _mDiagonalDirectionTreshold && dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            Debug.Log("Up Left");
            OnSwipeLeftUp();
        }
        else if (dir.x > _mDiagonalDirectionTreshold && -dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "Diagonal" || dir.x > _mDiagonalDirectionTreshold && -dir.y > _mDiagonalDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            Debug.Log("Down Right");
            OnSwipeRightDown();
        }
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
            Debug.Log("Up");
        }
        else if (Vector2.Dot(Vector2.down, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "Vertical" || Vector2.Dot(Vector2.down, dir) > _mDirectionTreshold && _mGameManager.SwipeDir == "AllDir")
        {
            OnSwipeDown();
            Debug.Log("Down");
        }
    }
}
