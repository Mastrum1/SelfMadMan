using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CasinoInteractableManager : MonoBehaviour
{
    private int _mIndex = 0;

    [SerializeField] private List<GameObject> _mParents;
    [SerializeField] private List<GameObject> _mStopPos;

    public void OnTap()
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
                        if (addChild.CompareTag(_mParents[_mIndex - 1].transform.GetChild(i).tag))
                        {
                            Debug.Log("loose");
                        }
                    }

                }
            }
        }
        _mIndex++;
        if (_mIndex > 2)
        {
            _mIndex = 0;
            Debug.Log("End Game");
        }
    }
}
