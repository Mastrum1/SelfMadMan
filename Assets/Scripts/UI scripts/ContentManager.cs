using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentManager : MonoBehaviour
{
    public enum MENUS
    {
        MainMenu, Shop, QuestAndAchievements, Settings, Furnitures, EraMinigames
    }

    [SerializeField] private HomePageUIManager _mHomePageUIManager;
    [SerializeField] private FurnitureManager _mFurnitureManager;
    [SerializeField] private GameObject[] _MenuGameObjects;

    [Header("Content Viewport")]
    [SerializeField] private Image _mBaseImage;
    [SerializeField] private Image _mJames;
    [SerializeField] private List<Sprite> _mBackgrounds;
    [SerializeField] private List<Sprite> _mJamesSprites;

    [SerializeField] private List<Image> _mImagesToDeactivate;
    [SerializeField] private List<TMP_Text> _mTextsToDeactivate;

    [Header("Navigation Dots")]
    [SerializeField] private TMP_Text _mText;

    [Header("Page Settings")]
    [SerializeField] private int _mCurrentIndex = 0;
    [SerializeField] private GameObject _mLockEraPanel;
    [SerializeField] private TMP_Text _mLockPrice;
    [SerializeField] private Animator _mLockAnimator;
    [SerializeField] private List<GameObject> _mLockObjects;

    private bool IsUnlocking = false;

    void Start()
    {
        _mCurrentIndex = GameManager.instance.Era;
        _mLockEraPanel.SetActive(GameManager.instance.ErasData[GameManager.instance.Era].Unlocked ? false : true);
        ShowContent();

     //   StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(5);
        _mFurnitureManager.UnlockFurniture("ChadStatue");
        _mFurnitureManager.PickFurniture("ChadStatue");
    }

    public void SwapContent(int i)
    {
        if (!IsUnlocking)
        {
            _mCurrentIndex = (_mCurrentIndex + i + _mBackgrounds.Count) % _mBackgrounds.Count;
            GameManager.instance.Era = _mCurrentIndex + 1;
            _mFurnitureManager.SetEra(GameManager.instance.Era);
            bool eraunlocked = GameManager.instance.ErasData[GameManager.instance.Era].Unlocked ? true : false;
            _mLockEraPanel.SetActive(!eraunlocked);
            _mLockPrice.text = GameManager.instance.ErasData[GameManager.instance.Era]._price.ToString();
            _mHomePageUIManager.MenuUIDictionnary[_MenuGameObjects[(int)MENUS.EraMinigames]] = eraunlocked;

            foreach (var go in _mLockObjects)
                go.SetActive(!eraunlocked);

            ShowContent();
        }
    }

    void ShowContent()
    {
        // Activate the current panel and deactivate others
        for (int i = 0; i < _mBackgrounds.Count; i++)
        {
            bool isActive = i == _mCurrentIndex;
            _mBaseImage.sprite = _mBackgrounds[_mCurrentIndex];
            _mJames.sprite = _mJamesSprites[_mCurrentIndex];
        }
    }

    public void UnlockEra()
    {
        
        if (MoneyManager.Instance.CurrentMoney < GameManager.instance.ErasData[GameManager.instance.Era]._price)
        {
            Debug.Log("No Money");
            return;
        }
        else
        {
            MoneyManager.Instance.SubsEra(_mLockPrice);
            IsUnlocking = true;
            _mLockAnimator.SetBool("UnlockEraAnim", true);
            StartCoroutine(Unlocking());
        }
    }

    IEnumerator Unlocking()
    {
        yield return new WaitForSeconds(2f);
        _mLockAnimator.SetBool("UnlockEraAnim", false);
        GameManager.instance.ErasData[GameManager.instance.Era].UnlockEra();
        GameManager.instance.GetComponent<Player>().UnlockEra(GameManager.instance.Era);
        _mHomePageUIManager.MenuUIDictionnary[_MenuGameObjects[(int)MENUS.EraMinigames]] = true;
        foreach (var go in _mLockObjects)
            go.SetActive(false);
        _mLockEraPanel.SetActive(false);
        IsUnlocking = false;
    }
}