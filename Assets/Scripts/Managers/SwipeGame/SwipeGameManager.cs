using System.Collections.Generic;
using UnityEngine;

public class SwipeGameManager : MiniGameManager
{
    [SerializeField]
    private int _mTotalNbSwipeLeft = 5;
    [SerializeField]
    private int _mTotalNbSwipeRight = 10;

    [SerializeField]
    private List<GameObject> _mListPub;

    [SerializeField]
    private GameObject _mInteractables;

    [SerializeField]
    private List<GameObject> _mListPeople;

    [SerializeField]
    private SwipeDetection _mSwipe;

    private int _mSwipeLeft = 0;

    private int _mSwipeRight = 0;

    private bool _mIsPub = false;

    private GameObject _mActualProfile;

    public string SwipeDir = "Horizontal";

    public float time = 10f;

    private void Awake()
    {
        _mTimer.ResetTimer(time);
        ShowProfile();
    }

    private void OnEnable()
    {
        _mSwipe.OnSwipeLeft += SwipeLeft;
        _mSwipe.OnSwipeRight += SwipeRight;
    }

    private void OnDisable()
    {
        _mSwipe.OnSwipeLeft -= SwipeLeft;
        _mSwipe.OnSwipeRight -= SwipeRight;
    }

    private void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
    }

    public void SwipeLeft()
    {
        if (!_mIsPub)
        {
            Debug.Log("Game Over");
            EndMiniGame(false, miniGameScore);

        }
        else
        {
            Debug.Log("Swipe gauche Pub");
            _mActualProfile.SetActive(false);
            _mSwipeLeft++;
            ShowProfile();


        }
    }

    public void SwipeRight()
    {
        if (_mIsPub)
        {
            Debug.Log("Game Over");
            EndMiniGame(false, miniGameScore);

        }
        else
        {
            Debug.Log("Swipe Right People");
            _mActualProfile.SetActive(false);
            _mSwipeRight++;
            if (_mSwipeRight < _mTotalNbSwipeRight)
            {
                ShowProfile();
            }
            else
            {
                Debug.Log("Win Game");
                EndMiniGame(true, miniGameScore);

            }
        }
    }

    private void ShowProfile()
    {
        int pub = Random.Range(0, 100);

        if (pub < 50)
        {
            int index = Random.Range(0, _mListPeople.Count);
            _mActualProfile = _mListPeople[index];
            _mActualProfile.SetActive(true);
            if (_mIsPub) _mIsPub = false;
        }
        else if (_mSwipeRight < _mTotalNbSwipeRight)
        {
            int index = Random.Range(0, _mListPub.Count);
            _mActualProfile = _mListPub[index];
            _mActualProfile.SetActive(true);
            if (!_mIsPub) _mIsPub = true;
        }

    }
}
