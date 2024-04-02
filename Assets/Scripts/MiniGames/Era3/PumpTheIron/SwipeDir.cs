using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDir : MonoBehaviour
{
    public event Action<string> OnSwipe;
    public event Action<bool> OnSwipeTooEarly;

    public void SwipeLeft()
    {
        if (OnSwipe != null)
        {
            OnSwipe.Invoke("Left");
        }
        else
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
        else
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
        else
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
        else
        {
            OnSwipeTooEarly?.Invoke(false);
        }
    }

}
