using CW.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CasinoInteractableManager : MonoBehaviour
{
    private int _mIndex = 0;

    [SerializeField] private List<GameObject> _mParents;
    [SerializeField] private List<ObjectScrolling> _mLeftObjects;
    [SerializeField] private List<ObjectScrolling> _mMiddleObjects;
    [SerializeField] private List<ObjectScrolling> _mRightObjects;

    [SerializeField] private List<CheckCollision> _mLeftCollisionObjects;
    [SerializeField] private List<CheckCollision> _mMiddleCollisionObjects;
    [SerializeField] private List<CheckCollision> _mRightCollisionObjects;


    [SerializeField] private List<GameObject> _mMiddleUpArrows;
    [SerializeField] private List<GameObject> _mMiddleDownArrows;

    [SerializeField] private List<GameObject> _mRightUpArrows;
    [SerializeField] private List<GameObject> _mRightDownArrows;

    [SerializeField] private List<Image> _mBackgrounds;

    [SerializeField] private List<GameObject> _mStopPos;


    private bool _mLost = false;

    public event Action<bool> OnGameEnd;

    private GameObject _mPreviousObj;

    private bool gameStarted = false;

    private void Start()
    {
        for (int id = 0; id < _mLeftObjects.Count; id++)
        {
            int RandomChild = UnityEngine.Random.Range(0, _mLeftObjects.Count - 1);
            var child = _mLeftObjects[RandomChild];
            Vector2 pos = child.transform.localPosition;
            child.transform.localPosition = _mLeftObjects[id].transform.localPosition;
            _mLeftObjects[id].transform.localPosition = pos;
            _mLeftCollisionObjects[id].OnSwitchPrevious += LeftSwitchPrevious;
            if (_mLeftObjects[id].transform.localPosition.y >= 700)
            {
                LeftSwitchPrevious(_mLeftObjects[id].transform);
            }
            else if (child.transform.localPosition.y >= 700)
            {
                LeftSwitchPrevious(child.transform);
            }
        }
        for (int id = 0; id < _mMiddleObjects.Count; id++)
        {
            int RandomChild = UnityEngine.Random.Range(0, _mMiddleObjects.Count - 1);
            var child = _mMiddleObjects[RandomChild];
            Vector2 pos = child.transform.localPosition;
            child.transform.localPosition = _mMiddleObjects[id].transform.localPosition;
            _mMiddleObjects[id].transform.localPosition = pos;
            _mMiddleCollisionObjects[id].OnSwitchPrevious += MiddleSwitchPrevious;
            if (_mMiddleObjects[id].transform.localPosition.y >= 700)
            {
                MiddleSwitchPrevious(_mMiddleObjects[id].transform);
            }
            else if (child.transform.localPosition.y >= 700)
            {
                MiddleSwitchPrevious(child.transform);
            }

        }
        for (int id = 0; id < _mRightObjects.Count; id++)
        {
            int RandomChild = UnityEngine.Random.Range(0, _mRightObjects.Count - 1);
            var child = _mRightObjects[RandomChild];
            Vector2 pos = child.transform.localPosition;
            child.transform.localPosition = _mRightObjects[id].transform.localPosition;
            _mRightObjects[id].transform.localPosition = pos;
            _mRightCollisionObjects[id].OnSwitchPrevious += RightSwitchPrevious;
            if (_mRightObjects[id].transform.localPosition.y >= 700)
            {
                RightSwitchPrevious(_mRightObjects[id].transform);
            }
            else if (child.transform.localPosition.y >= 700)
            {
                RightSwitchPrevious(child.transform);
            }
        }
    }

    public void LeftSwitchPrevious(Transform transform)
    {
        for (int id = 0; id < _mLeftObjects.Count; id++)
        {
            _mLeftCollisionObjects[id].PreviousObj = transform;
        }
    }

    public void MiddleSwitchPrevious(Transform transform)
    {
        for (int id = 0; id < _mLeftObjects.Count; id++)
        {
            _mMiddleCollisionObjects[id].PreviousObj = transform;
        }
    }

    public void RightSwitchPrevious(Transform transform)
    {
        for (int id = 0; id < _mLeftObjects.Count; id++)
        {
            _mRightCollisionObjects[id].PreviousObj = transform;
        }
    }


    public void OnTap()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            for (int id = 0; id < _mParents.Count; id++)
            {
                for (int i = 0; i < _mLeftObjects.Count; i++)
                {
                    var child = _mLeftObjects[i];
                    if (child != null)
                    {
                        child.StartGame();
                    }
                }
                for (int i = 0; i < _mMiddleObjects.Count; i++)
                {
                    var child = _mMiddleObjects[i];
                    if (child != null)
                    {
                        child.StartGame();
                    }
                }
                for (int i = 0; i < _mRightObjects.Count; i++)
                {
                    var child = _mRightObjects[i];
                    if (child != null)
                    {
                        child.StartGame();
                    }

                }
            }
        }
        else
        {
            switch (_mIndex)
            {
                case 0:
                    for (int i = 0; i < _mLeftObjects.Count; i++)
                    {
                        var addChild = _mLeftObjects[i];
                        if (addChild != null)
                        {
                            addChild.Started = false;


                            if (addChild.IsCentered == true)
                            {
                                SendDistToAllLeftObj(_mStopPos[_mIndex].transform.localPosition.y - addChild.transform.localPosition.y);

                                _mPreviousObj = addChild.gameObject;

                            }
                        }
                    }
                    _mMiddleUpArrows[0].SetActive(true);
                    _mMiddleUpArrows[1].SetActive(false);
                    _mMiddleDownArrows[0].SetActive(true);
                    _mMiddleDownArrows[1].SetActive(false);
                    _mBackgrounds[0].color = new Color(_mBackgrounds[0].color.r, _mBackgrounds[0].color.g, _mBackgrounds[0].color.b, 255);
                    break;
                case 1:
                    for (int i = 0; i < _mMiddleObjects.Count; i++)
                    {
                        var addChild = _mMiddleObjects[i];
                        if (addChild != null)
                        {
                            addChild.Started = false;


                            if (addChild.IsCentered == true)
                            {
                                SendDistToAllMidObj(_mStopPos[_mIndex].transform.localPosition.y - addChild.transform.localPosition.y);

                                if (!addChild.CompareTag(_mPreviousObj.tag))
                                {
                                    _mLost = true;
                                    Debug.Log("loose");
                                    OnGameEnd?.Invoke(false);
                                }
                                else
                                {
                                    _mPreviousObj = addChild.gameObject;
                                }
                            }
                        }
                    }
                    _mRightUpArrows[0].SetActive(true);
                    _mRightUpArrows[1].SetActive(false);
                    _mRightDownArrows[0].SetActive(true);
                    _mRightDownArrows[1].SetActive(false);
                    _mBackgrounds[1].color = new Color(_mBackgrounds[1].color.r, _mBackgrounds[1].color.g, _mBackgrounds[1].color.b, 255);


                    break;
                case 2:
                    for (int i = 0; i < _mMiddleObjects.Count; i++)
                    {
                        var addChild = _mMiddleObjects[i];
                        if (addChild != null)
                        {
                            addChild.Started = false;

                            if (addChild.IsCentered == true)
                            {
                                SendDistToAllRightObj(_mStopPos[_mIndex].transform.localPosition.y - addChild.transform.localPosition.y);

                                if (!addChild.CompareTag(_mPreviousObj.tag))
                                {
                                    _mLost = true;
                                    OnGameEnd?.Invoke(false);
                                }

                            }
                        }
                    }


                    break;
                default:
                    break;
            }
            _mIndex++;

            if (_mIndex > 2 && !_mLost)
            {
                _mIndex = 0;
                OnGameEnd?.Invoke(true);

                //StartCoroutine(Wait());
            }
        }
    }

    void SendDistToAllLeftObj(float dist)
    {
        for (int i = 0; i < _mLeftObjects.Count; i++)
        {
            _mLeftObjects[i].moveToPos(dist);
            _mLeftObjects[i].Started = false;

        }
    }

    void SendDistToAllMidObj(float dist)
    {
        for (int i = 0; i < _mMiddleObjects.Count; i++)
        {
            _mMiddleObjects[i].moveToPos(dist);
            _mMiddleObjects[i].Started = false;
        }
    }

    void SendDistToAllRightObj(float dist)
    {
        for (int i = 0; i < _mRightObjects.Count; i++)
        {
            _mRightObjects[i].moveToPos(dist);
            _mRightObjects[i].Started = false;

        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        OnGameEnd?.Invoke(true);
    }
}
