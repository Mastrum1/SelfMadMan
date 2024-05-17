using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingPopup : MonoBehaviour
{
    [SerializeField] private int _mCurrentRating = 0;
    [SerializeField] private Image[] _mStarIcons;
    [SerializeField] private Sprite _mStarIconFilled;
    [SerializeField] private Sprite _mStarIconEmpty;
    [SerializeField] private Animator _mRatingAnim;

    public bool Rated;

    public void Rating(int starIndex)
    {
        _mCurrentRating = starIndex;
        UpdateStars();
    }

    private void UpdateStars()
    {
        for (int i = 0; i < _mStarIcons.Length; i++)
        {
            if (i > _mCurrentRating)
            {
                _mStarIcons[i].sprite = _mStarIconFilled;
            }
            else
            {
                _mStarIcons[i].sprite = _mStarIconEmpty;
            }
        }
    }

    public void RateButton()
    {
        if (_mCurrentRating >= 3)
        {
            Application.OpenURL("https://apps.apple.com/us/app/self-mad-man-become-a-chad/id6479361111?platform=iphone");
            CloseRateUs();
        }
        else
        {
            Application.OpenURL("https://selfmadman.fr/");
            CloseRateUs();
        }
    }

    public void PopUP()
    {
        int r = Random.Range(0, 10);

        if (r == 3 || r == 6 || r == 9)
        {
            if (!Rated)
            {
                OpenRateUs();
            }
            else
            {
                return;
            }
        } 
    }

    public void OpenRateUs()
    {
        _mRatingAnim.SetBool("Open", true);
    }

    public void CloseRateUs()
    {
        _mRatingAnim.SetBool("Open", false);
    }
}
