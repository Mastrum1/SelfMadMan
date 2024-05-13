using System;
using System.Collections.Generic;
using UnityEngine;

public class CardashianInteractableManager : MonoBehaviour
{

    [SerializeField] private List<cardashianCard> _mObj;
    private float _spawnDistance = 2f;
    private float _minDistanceBetweenObjects = 1.5f;
    public event Action<bool> GameEnd;
    [SerializeField] private float _mSpace = 1.5f;
    private Vector3 _lastObjPos = Vector3.zero;
    [SerializeField] private int NbObjToSpawn = 2;

    private void Start()
    {
        Vector3 pos = new Vector3(_mObj[0].transform.position.x, 0, _mObj[0].transform.position.z);
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                NbObjToSpawn = 8;
                //pos.y -= _mSpace / 2;
                break;
            case 2:
                NbObjToSpawn = 10;
                //pos.y -= _mSpace;

                break;
            case 3:
                NbObjToSpawn = 12;
                //pos.y -= _mSpace * 2;
                break;
            default:
                break;
        }

        int Random = UnityEngine.Random.Range(0, NbObjToSpawn);
        _mObj[Random].Real = true;
        for (int id = 0; id < NbObjToSpawn; id++)
        {
            if(id == Random)
            {
                _mObj[id].OnGameEnd += EndGame;
                _mObj[id].gameObject.SetActive(true);

                continue;
            }
            else
            {
                _mObj[id].Real = false;
                _mObj[id].OnGameEnd += EndGame;
                _mObj[id].gameObject.SetActive(true);
            }
           
        }
    }

    private void OnDisable()
    {
        for (int id = 0; id < _mObj.Count; id++)
        {
            _mObj[id].OnGameEnd -= EndGame;
        }
    }



    private void EndGame(bool win)
    {
        GameEnd?.Invoke(win);
    }
}
