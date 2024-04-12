using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] Transform _mPreviousObj;
    [SerializeField] ObjectScrolling _mObjectScrolling;
    public Transform PreviousObj
    {
        get { return _mPreviousObj; }
        set { _mPreviousObj = value; }
    }

    public Action<Transform> OnSwitchPrevious;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MiddlePos"))
        {
            _mObjectScrolling.IsCentered = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndPos"))
        {
            gameObject.transform.localPosition = new Vector2(_mPreviousObj.localPosition.x, _mPreviousObj.localPosition.y + 175f);
            OnSwitchPrevious?.Invoke(gameObject.transform);
        }

        if (collision.gameObject.CompareTag("MiddlePos"))
        {
            _mObjectScrolling.IsCentered = false;
        }
    }
}
