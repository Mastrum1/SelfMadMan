using System;
using System.Collections.Generic;
using UnityEngine;

public class CasinoInteractableManager : MonoBehaviour
{
    private int _mIndex = 0;

    [SerializeField] private List<GameObject> _mParents;
    [SerializeField] private List<GameObject> _mStopPos;

    private GameObject _mPreviousObj;

    public event Action<bool> OnGameEnd;

    private bool gameStarted = false;

    private void Start()
    {
        for (int id = 0; id < _mParents.Count; id++)
        {
            int RandomChild = UnityEngine.Random.Range(0, _mParents[id].transform.childCount - 1);
            var child = _mParents[id].transform.GetChild(RandomChild).GetComponent<ObjectScrolling>();

            Vector2 pos = child.transform.position;
            child.transform.position = _mStopPos[id].transform.position;
            child.GetComponent<SpriteRenderer>().enabled = true;
            _mParents[id].transform.GetChild(0).GetComponent<ObjectScrolling>().transform.position = pos;
            _mParents[id].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    public void OnTap()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            for (int id = 0; id < _mParents.Count; id++)
            {
                for (int i = 0; i < _mParents[id].transform.childCount; i++)
                {
                    var addChild = _mParents[id].transform.GetChild(i).GetComponent<ObjectScrolling>();
                    if (addChild != null)
                    {
                        addChild.StartGame();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < _mParents[_mIndex].transform.childCount; i++)
            {
                var addChild = _mParents[_mIndex].transform.GetChild(i).GetComponent<ObjectScrolling>();
                if (addChild != null)
                {
                    addChild.enabled = false;

                    if (addChild.GetComponent<SpriteRenderer>().enabled == true)
                    {
                        addChild.transform.position = _mStopPos[_mIndex].transform.position;
                        if (_mIndex != 0 && _mParents[_mIndex - 1].transform.GetChild(i).GetComponent<ObjectScrolling>() != null)
                        {
                            if (!addChild.CompareTag(_mPreviousObj.tag))
                            {
                                Debug.Log("loose");
                                OnGameEnd?.Invoke(false);
                            }
                        }
                        else
                        {
                            _mPreviousObj = addChild.gameObject;
                        }

                    }
                }
            }
            _mIndex++;
            if (_mIndex > 2)
            {
                _mIndex = 0;
                Debug.Log("End Game");
                OnGameEnd?.Invoke(true);
            }
        }
    }
}
