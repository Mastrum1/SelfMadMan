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
        _mSwipe = GetComponent<SwipeDetection>();
        InstantiateAll();
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
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        else
        {
            _mTimer.UpdateTimer();
        }
    }

    public void SwipeLeft()
    {
        if (_mIsPub)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Swipe gauche people");
            _mActualProfile.SetActive(false);
            _mSwipeLeft++;
            if (_mSwipeLeft < _mTotalNbSwipeLeft)
            {
                ShowProfile();
            }
            else
            {
                Debug.Log("Win Game");
            }
        }
    }

    public void SwipeRight()
    {
        if (!_mIsPub)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Swipe Right pub");
            _mActualProfile.SetActive(false);
            _mSwipeRight++;
            ShowProfile();
        }
    }

    private void InstantiateAll()
    {

        List<GameObject> tempPeopleList = new List<GameObject>();

        foreach (GameObject obj in _mListPeople)
        {
            GameObject tempObj = Instantiate(obj, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity);
            tempObj.transform.SetParent(_mInteractables.transform);
            tempObj.SetActive(false);
            tempPeopleList.Add(tempObj);
        }

        _mListPeople = tempPeopleList;

        List<GameObject> tempPubList = new List<GameObject>();

        foreach (GameObject obj in _mListPub)
        {
            GameObject tempObj = Instantiate(obj, new Vector3(0.0f, 0.0f, 10.0f), Quaternion.identity);
            tempObj.transform.SetParent(_mInteractables.transform);
            tempObj.SetActive(false);
            tempPubList.Add(tempObj);
        }

        _mListPub = tempPubList;

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
