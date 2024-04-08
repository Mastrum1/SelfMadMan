using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindherGameManager : MiniGameManager
{
    [SerializeField] int _mTotalNbSwipeRight = 10;
    [SerializeField] private GameObject _mInteractables;

    //Girls Variables
    [SerializeField] GameObject _mProfile;
    [SerializeField] Image _mGirlSpriteRenderer;
    [SerializeField] TextMeshProUGUI _mGirlName;
    [SerializeField] TextMeshProUGUI _mGirlAge;
    [SerializeField] TextMeshProUGUI _mGirlDescription;
    Sprite _mGirlBackground;

    //Ad Variables
    [SerializeField] GameObject _mAd;
    [SerializeField] Image _mAdSpriteRenderer;
    [SerializeField] TextMeshProUGUI _mAdName;
    [SerializeField] TextMeshProUGUI _mAdDescription;

    //SO lists
    [SerializeField] List<Ads> _mListAd;
    [SerializeField] List<Girls> _mListPeople;
   
    [SerializeField] List<Sprite> _mListImages;

    private int _mSwipeRight = 0;
    private bool _mIsAd = false;

    private void Start()
    {
        ShowProfile();
    }

    public void SwipeLeft()
    {
        if (!_mIsAd)
        {
            EndMiniGame(false, miniGameScore);
        }
        else
        {
            _mAd.SetActive(false);
            _mProfile.SetActive(false);
            ShowProfile();
        }
    }

    public void SwipeRight()
    {
        if (_mIsAd)
        {
            EndMiniGame(false, miniGameScore);

        }
        else
        {
            _mAd.SetActive(false);
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
            //_mGirlDescription.text = _mListPeople[index].Description;
            
            int bgIndex = Random.Range(0, _mListImages.Count);
            _mGirlBackground = _mListImages[bgIndex];

            _mProfile.SetActive(true);
            if (_mIsAd) _mIsAd = false;
        }
        else
        {
            int index = Random.Range(0, _mListAd.Count);

            _mAdSpriteRenderer.sprite = _mListAd[index].ProfilePicture;
            //_mAdName.text = _mListAd[index].Name;
            //_mAdName.text = _mListAd[index].Description;

            _mAd.SetActive(true);
            if (!_mIsAd) _mIsAd = true;
        }
    }
}
