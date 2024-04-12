using System;
using System.Collections.Generic;
using UnityEngine;

public class CardashianInteractableManager : MonoBehaviour
{

    [SerializeField] private List<cardashianCard> _mObj;

    public event Action<bool> GameEnd;
    [SerializeField] private float _mSpace = 1.5f;

    [SerializeField] private int NbObjToSpawn = 2;

    private void Start()
    {
        Vector3 pos = new Vector3(_mObj[0].transform.position.x, 0, _mObj[0].transform.position.z);
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                NbObjToSpawn = 2;
                pos.y -= _mSpace / 2;
                break;
            case 2:
                NbObjToSpawn = 3;
                pos.y -= _mSpace;

                break;
            case 3:
                NbObjToSpawn = 5;
                pos.y -= _mSpace * 2;
                break;
            default:
                break;
        }

        int Random = UnityEngine.Random.Range(0, NbObjToSpawn);
        _mObj[Random].Different = true;
        _mObj[Random].ChangeText();
        for (int id = 0; id < NbObjToSpawn; id++)
        {
            _mObj[id].OnGameEnd += EndGame;
            if (id != 0)
            {
                _mObj[id].transform.position = new Vector3(pos.x, _mObj[id - 1].transform.position.y + _mSpace, pos.z);
            }
            else
            {
                _mObj[id].transform.position = pos;
            }
            _mObj[id].gameObject.SetActive(true);
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
