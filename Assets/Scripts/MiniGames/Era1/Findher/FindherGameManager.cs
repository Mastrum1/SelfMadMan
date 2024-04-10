using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindherGameManager : MiniGameManager
{
    [SerializeField] int _mTotalNbSwipeRight = 10;
    [SerializeField] private GameObject _mInteractables;
    [SerializeField] private GameObject _mCanvas;

    //Girls Variables
    [Header ("Girls Profile")]
    [SerializeField] GameObject _mProfile;
    [SerializeField] Image _mGirlSpriteRenderer;
    [SerializeField] Image _mGirlBg;
    [SerializeField] TextMeshProUGUI _mGirlName;
    [SerializeField] TextMeshProUGUI _mGirlAge;
    [SerializeField] TextMeshProUGUI _mGirlDescription;
    Sprite _mGirlBackground;

    //Ad Variables
    [Header("Ads Profile")]
    [SerializeField] GameObject _mAd;
    [SerializeField] Image _mAdSpriteRenderer;
    [SerializeField] TextMeshProUGUI _mAdName;
    [SerializeField] TextMeshProUGUI _mAdDescription;

    //SO lists
    [Header("SO Lists")]
    [SerializeField] List<Ads> _mListAd;
    [SerializeField] List<Girls> _mListPeople;
   
    [SerializeField] List<Sprite> _mListImages;

    [Header("Sliders")]
    [SerializeField] List<GameObject> _mListSlider;
    [SerializeField] List<Animator> _mListSliderAnim;
    [SerializeField] List<GameObject> _mListSliderGirlsGO;
    [SerializeField] List<Image> _mListSliderImageGirls;
    [SerializeField] List<Image> _mListSliderImageGirlsBackground;
    [SerializeField] List<TextMeshProUGUI> _mListSliderGirlsName;
    [SerializeField] List<TextMeshProUGUI> _mListSliderGirlsAge;
    [SerializeField] List<TextMeshProUGUI> _mListSliderGirlsJob;

    [SerializeField] List<GameObject> _mListSliderAdsGO;
    [SerializeField] List<Image> _mListSliderImageAds;
    [SerializeField] List<TextMeshProUGUI> _mListSliderAdsName;
    [SerializeField] List<TextMeshProUGUI> _mListSliderAdsText;
    
    

    private int _mSwipeRight = 0;
    private bool _mIsAd = false;
    private int _index = 0;

    public override void Awake()
    {   
        base.Awake();
        ShowProfile();
    }

    public void SwipeLeft()
    {
        SwipeAnimation(true);
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
        SwipeAnimation(false);
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
            _mGirlDescription.text = _mListPeople[index].Description;
            
            int bgIndex = Random.Range(0, _mListImages.Count);
            _mGirlBackground = _mListImages[bgIndex];
            _mGirlBg.sprite = _mGirlBackground;


            _mProfile.SetActive(true);
            if (_mIsAd) _mIsAd = false;
        }
        else
        {
            int index = Random.Range(0, _mListAd.Count);

            _mAdSpriteRenderer.sprite = _mListAd[index].ProfilePicture;
            _mAdName.text = _mListAd[index].Name;
            _mAdDescription.text = _mListAd[index].Description;

            _mAd.SetActive(true);
            if (!_mIsAd) _mIsAd = true;
        }
    }

    private void SwipeAnimation(bool right)
    {
        if (_mIsAd)
        {
            _mListSliderAdsGO[_index].SetActive(true);
            _mListSliderGirlsGO[_index].SetActive(false);
            _mListSliderAdsName[_index].text = _mAdName.text;
            _mListSliderAdsText[_index].text = _mAdDescription.text;
            _mListSliderImageAds[_index].sprite = _mAdSpriteRenderer.sprite;
        }
        else
        {
            _mListSliderGirlsGO[_index].SetActive(true);
            _mListSliderAdsGO[_index].SetActive(false);
            _mListSliderGirlsName[_index].text = _mGirlName.text;
            _mListSliderGirlsAge[_index].text = _mGirlAge.text;
            _mListSliderGirlsJob[_index].text = _mGirlDescription.text;
            _mListSliderImageGirls[_index].sprite = _mGirlSpriteRenderer.sprite;
            _mListSliderImageGirlsBackground[_index].sprite = _mGirlBackground;
        }
        _mListSlider[_index].SetActive(true);
        _mListSliderAnim[_index].SetTrigger(right ? "Left" : "Right");
        StartCoroutine(Reset(_mListSlider[_index]));
        Debug.Log(_index);
        if (_index == _mListSlider.Count-1) _index = 0;
        else _index++;
    }
    IEnumerator Reset(GameObject go)
    {
        yield return new WaitForSeconds(.7f);
        go.SetActive(false);
    }
}
