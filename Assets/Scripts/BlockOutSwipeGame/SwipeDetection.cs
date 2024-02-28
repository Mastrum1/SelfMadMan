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
    private float minimumDist = .1f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionTreshold = .9f;

    private InputManager inputManager;
    [SerializeField]
    private GameManager gameManager;

    private Vector2 startPos;
    private float startTime;
    private Vector2 endPos;
    private float endTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;

    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPos = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPos = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPos, endPos) >= minimumDist && (endTime - startTime) <= maximumTime)
        {
            Debug.Log("Swipe detected");
            Debug.DrawLine(startPos, endPos, Color.red, 5f);
            Vector3 direction = endPos - startPos;
            Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2d);
        }
    }

    private void SwipeDirection(Vector2 dir)
    {
        if (Vector2.Dot(Vector2.right, dir) > directionTreshold && gameManager.SwipeDir == "Horizontal" || Vector2.Dot(Vector2.right, dir) > directionTreshold && gameManager.SwipeDir == "AllDir")
        {
            OnSwipeRight();
        }
        else if (Vector2.Dot(Vector2.left, dir) > directionTreshold && gameManager.SwipeDir == "Horizontal" || Vector2.Dot(Vector2.left, dir) > directionTreshold && gameManager.SwipeDir == "AllDir")
        {
            OnSwipeLeft();
        }
        else if (Vector2.Dot(Vector2.up, dir) > directionTreshold && gameManager.SwipeDir == "Vertical" || Vector2.Dot(Vector2.up, dir) > directionTreshold && gameManager.SwipeDir == "AllDir")
        {
            OnSwipeUp();    
        }
        else if (Vector2.Dot(Vector2.down, dir) > directionTreshold && gameManager.SwipeDir == "Vertical" || Vector2.Dot(Vector2.down, dir) > directionTreshold && gameManager.SwipeDir == "AllDir")
        {
            OnSwipeDown();
        }
    }
}
