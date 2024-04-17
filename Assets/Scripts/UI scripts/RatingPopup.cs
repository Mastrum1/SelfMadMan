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
            //Redirect to the store
        }
        else
        {
            Application.OpenURL("https://selfmadman.fr/");
        }
    }
}
