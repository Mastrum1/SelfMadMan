using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScrolling : MonoBehaviour
{
    [SerializeField] float _mSpeed;
    [SerializeField] float _mEndMoveSpeed;
    private bool _mStarted = false;
    public bool Started
    {
        get { return _mStarted; }
        set { _mStarted = value; }
    }

    private bool _mIsCentered = false;
    public bool IsCentered
    {
        get { return _mIsCentered; }
        set { _mIsCentered = value; }
    }

    public void StartGame()
    {
        Started = true;
    }

    private void Start()
    {
        _mSpeed = _mSpeed * GameManager.instance.FasterLevel;
        _mEndMoveSpeed = _mEndMoveSpeed * GameManager.instance.FasterLevel;
    }

    void Update()
    {
        if (Started)
        {
            transform.Translate(Vector3.down * _mSpeed * Time.deltaTime);
        }
    }

    public void moveToPos(float distance)
    {
        StartCoroutine(MoveToPos(distance));
    }

    IEnumerator MoveToPos(float distance)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.localPosition;
        Vector3 targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + distance, transform.localPosition.z);

        while (elapsedTime < 0.5)
        {
            transform.localPosition = Vector3.Lerp(startingPos, targetPos, elapsedTime * _mEndMoveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPos;
    }
}
