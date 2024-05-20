using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDir : MonoBehaviour
{
    public event Action<string> OnSwipe;
    public event Action<bool> OnSwipeTooEarly;

    private bool _mEndGame = false;
    public bool EndGame { get => _mEndGame; set => _mEndGame = value; }

    public void OnEnable()
    {
        _mEndGame = false;
    }
    public void SwipeLeft()
    {
        if (OnSwipe != null)
        {
            OnSwipe.Invoke("Left");
        }
        else if (!EndGame)
        {
            OnSwipeTooEarly?.Invoke(false);
        }
    }

    public void SwipeRight()
    {
        if (OnSwipe != null)
        {
            OnSwipe?.Invoke("Right");
        }
        else if (!EndGame)
        {
            OnSwipeTooEarly?.Invoke(false);
        }
    }

    public void SwipeUp()
    {
        if (OnSwipe != null)
        {
            OnSwipe?.Invoke("Up");
        }
        else if (!EndGame)
        {
            OnSwipeTooEarly?.Invoke(false);
        }
    }

    public void SwipeDown()
    {
        if (OnSwipe != null)
        {
            OnSwipe?.Invoke("Down");
        }
        else if (!EndGame)
        {
            OnSwipeTooEarly?.Invoke(false);
        }
    }

}
