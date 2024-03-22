using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindherGameManager : MiniGameManager
{
    [SerializeField]
    private int _mTotalNbSwipeRight = 10;

    [SerializeField]
    private List<GameObject> _mListPub;

    [SerializeField]
    private GameObject _mInteractables;

    [SerializeField]
    private List<GameObject> _mListPeople;

    private int _mSwipeRight = 0;

    private bool _mIsPub = false;

    private GameObject _mActualProfile;

    private void Start()
    {
        ShowProfile();
    }


    public void SwipeLeft()
    {
        if (!_mIsPub)
        {
            EndMiniGame(false, miniGameScore);

        }
        else
        {
            _mActualProfile.SetActive(false);
            ShowProfile();
        }
    }

    public void SwipeRight()
    {
        if (_mIsPub)
        {
            EndMiniGame(false, miniGameScore);

        }
        else
        {
            _mActualProfile.SetActive(false);
            _mSwipeRight++;
            if (_mSwipeRight < _mTotalNbSwipeRight)
            {
                ShowProfile();
            }
            else
            {

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
            while (_mListPeople[index] == _mActualProfile)
            {
                index = Random.Range(0, _mListPeople.Count);
            }
            _mActualProfile = _mListPeople[index];
            _mActualProfile.SetActive(true);
            if (_mIsPub) _mIsPub = false;
        }
        else
        {
            int index = Random.Range(0, _mListPub.Count);
            while (_mListPub[index] == _mActualProfile)
            {
                index = Random.Range(0, _mListPub.Count);
            }
            _mActualProfile = _mListPub[index];
            _mActualProfile.SetActive(true);
            if (!_mIsPub) _mIsPub = true;
        }

    }
}
