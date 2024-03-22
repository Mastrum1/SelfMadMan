using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentManager : MonoBehaviour
{
    [Header("For Screnn space Camera")]
    [SerializeField] private Canvas _mMyCanvas;
    private Camera cam { get => _mMyCanvas.worldCamera; }

    [Header("Content Vieport")]
    [SerializeField] private Image _mContentDisplay;
    [SerializeField] private Image _mEraBGContent;
    [SerializeField] private List<GameObject> _mContentPanels;
    [SerializeField] private List<GameObject> _mEraBGPanel;

    [Header("Navigation Dots")]
    [SerializeField] private GameObject _mDotsContainer;
    [SerializeField] private GameObject _mDotPrefab;
    [SerializeField] private TMP_Text _mText;

    [Header("Pagination Buttons")]
    [SerializeField] private Button _mNextButton;
    [SerializeField] private Button _mPrevButton;

    [Header("Page Settings")]
    [SerializeField] private bool isLimitedSwipe = false;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private Vector2 touchStartPos;

    // Reference to the RectTransform of the content area
    [SerializeField] private RectTransform contentArea;

    void Start()
    {
        _mNextButton.onClick.AddListener(NextContent);
        _mPrevButton.onClick.AddListener(PreviousContent);

        // Initialize dots
        InitializeDots();

        // Display initial content
        ShowContent();
    }

    void InitializeDots()
    {
        // Create dots based on the number of content panels
        for (int i = 0; i < _mContentPanels.Count; i++)
        {
            GameObject dot = Instantiate(_mDotPrefab, _mDotsContainer.transform);
            Image dotImage = dot.GetComponent<Image>();
            dotImage.color = (i == currentIndex) ? Color.white : Color.gray;
            dotImage.fillAmount = 0f; // Initial fill amount
            // You may want to customize the dot appearance and layout here
            _mText.text = "Ere " + (currentIndex + 1);
        }
    }

    void UpdateDots()
    {
        // Update the appearance of dots based on the current index
        for (int i = 0; i < _mDotsContainer.transform.childCount; i++)
        {
            Image dotImage = _mDotsContainer.transform.GetChild(i).GetComponent<Image>();
            dotImage.color = (i == currentIndex) ? Color.white : Color.gray;

            _mText.text = "Ere " + (currentIndex + 1);
        }
    }

    void Update()
    {
        // Detect swipe input only within the content area
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchEndPos = Input.mousePosition;
            Vector3 output = Vector2.zero;
            float swipeDistance = touchEndPos.x - touchStartPos.x;

            contentArea.position = output;

            // Check if the swipe is within the content area bounds
            if (Mathf.Abs(swipeDistance) > swipeThreshold && IsTouchInContentArea(touchStartPos, output))
            {
                
                if (isLimitedSwipe && ((currentIndex == 0 && swipeDistance > 0) || (currentIndex == _mContentPanels.Count - 1 && swipeDistance < 0)))
                {
                    // Limited swipe is enabled, and at the edge of content
                    return;
                }

                if (swipeDistance > 0)
                {
                    PreviousContent();
                }
                else
                {
                    NextContent();
                }
            }
        }
    }

    // Check if the touch position is within the content area bounds
    bool IsTouchInContentArea(Vector2 touchPosition, Vector3 output)
    {
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(_mMyCanvas.GetComponent<RectTransform>(), touchPosition, cam, out output);
    }

    void NextContent()
    {
        currentIndex = (currentIndex + 1) % _mContentPanels.Count;
        ShowContent();
        UpdateDots();
    }

    void PreviousContent()
    {
        currentIndex = (currentIndex - 1 + _mContentPanels.Count) % _mContentPanels.Count;
        ShowContent();
        UpdateDots();
    }

    void ShowContent()
    {
        // Activate the current panel and deactivate others
        for (int i = 0; i < _mContentPanels.Count; i++)
        {
            bool isActive = i == currentIndex;
            _mContentPanels[i].SetActive(isActive);

            // Update dot visibility and color based on the current active content
            Image dotImage = _mDotsContainer.transform.GetChild(i).GetComponent<Image>();
            dotImage.color = isActive ? Color.white : Color.gray;
            dotImage.fillAmount = isActive ? 1f : 0f;
        }

        for (int i = 0; i < _mEraBGPanel.Count; i++)
        {
            bool isActive = i == currentIndex;
            _mEraBGPanel[i].SetActive(isActive);
        }
    }
    public void SetCurrentIndex(int newIndex)
    {
        if (newIndex >= 0 && newIndex < _mContentPanels.Count)
        {
            currentIndex = newIndex;
            ShowContent();
            UpdateDots();
        }
    }
}