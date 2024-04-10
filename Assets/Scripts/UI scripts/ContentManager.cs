using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Touch;

public class ContentManager : MonoBehaviour
{
    [Header("Content Viewport")]
    [SerializeField] private Image _mBaseImage;
    [SerializeField] private Image _mJames;
    [SerializeField] private List<Sprite> _mSprites;
    [SerializeField] private List<Sprite> _mJamesForms;

    [Header("Navigation Dots")]
    [SerializeField] private GameObject _mDotsContainer;
    [SerializeField] private GameObject _mDotPrefab;
    [SerializeField] private TMP_Text _mText;

    public int CurrentIndex { get => _mCurrentIndex; set => _mCurrentIndex = value; }
    [Header("Page Settings")]
    [SerializeField] private int _mCurrentIndex = 0;

    void Start()
    {
        // Initialize dots
        InitializeDots();

        // Display initial content
        ShowContent();
    }

    void InitializeDots()
    {
        // Create dots based on the number of content panels
        for (int i = 0; i < _mSprites.Count; i++)
        {
            GameObject dot = Instantiate(_mDotPrefab, _mDotsContainer.transform);
            Image dotImage = dot.GetComponent<Image>();
            dotImage.color = (i == _mCurrentIndex) ? Color.white : Color.gray;
            dotImage.fillAmount = 0f; // Initial fill amount
            // You may want to customize the dot appearance and layout here
            _mText.text = "Ere " + (_mCurrentIndex + 1);
        }
    }

    void UpdateDots()
    {
        // Update the appearance of dots based on the current index
        for (int i = 0; i < _mDotsContainer.transform.childCount; i++)
        {
            Image dotImage = _mDotsContainer.transform.GetChild(i).GetComponent<Image>();
            dotImage.color = (i == _mCurrentIndex) ? Color.white : Color.gray;

            _mText.text = "Ere " + (_mCurrentIndex + 1);
        }
    }

    public void SwapContent(int i)
    {
        _mCurrentIndex = (_mCurrentIndex + i + _mSprites.Count) % _mSprites.Count;
        GameManager.instance.Era = _mCurrentIndex + 1;
        ShowContent();
        UpdateDots();
    }

    void ShowContent()
    {
        // Activate the current panel and deactivate others
        for (int i = 0; i < _mSprites.Count; i++)
        {
            bool isActive = i == _mCurrentIndex;
            _mBaseImage.sprite = _mSprites[_mCurrentIndex];

            // Update dot visibility and color based on the current active content
            Image dotImage = _mDotsContainer.transform.GetChild(i).GetComponent<Image>();
            dotImage.color = isActive ? Color.white : Color.gray;
            dotImage.fillAmount = isActive ? 1f : 0f;
        }

        for (int i = 0; i < _mJamesForms.Count; i++) 
        {
            _mJames.sprite = _mJamesForms[_mCurrentIndex];
        }
    }
}