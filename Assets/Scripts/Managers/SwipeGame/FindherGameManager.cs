using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;


public class FindherGameManager : MiniGameManager
{
    [SerializeField] int _mTotalNbSwipeRight = 10;
    [SerializeField] private GameObject _mInteractables;

    //Girls Variables
    [SerializeField] GameObject _mProfile;
    [SerializeField] SpriteRenderer _mGirlSpriteRenderer;
    [SerializeField] TextMeshProUGUI _mGirlName;
    [SerializeField] TextMeshProUGUI _mGirlAge;

    //Pub Variables
    [SerializeField] GameObject _mPub;
    [SerializeField] SpriteRenderer _mPubSpriteRenderer;

    //SO lists
    [SerializeField] List<Ads> _mListAd;
    [SerializeField] List<Girls> _mListPeople;

    private int _mSwipeRight = 0;
    private bool _mIsPub = false;

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
            _mPub.SetActive(false);
            _mProfile.SetActive(false);
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
            _mPub.SetActive(false);
            _mProfile.SetActive(false);

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

            _mGirlSpriteRenderer.sprite = _mListPeople[index].ProfilePicture;
            _mGirlName.text = _mListPeople[index].GirlName;
            _mGirlAge.text = _mListPeople[index].Age.ToString();

            _mProfile.SetActive(true);
            if (_mIsPub) _mIsPub = false;
        }
        else
        {
            int index = Random.Range(0, _mListAd.Count);

            _mPubSpriteRenderer.sprite = _mListAd[index].ProfilePicture;

            _mPub.SetActive(true);
            if (!_mIsPub) _mIsPub = true;
        }
    }
}
