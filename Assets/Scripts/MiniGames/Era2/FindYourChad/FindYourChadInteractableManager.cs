using System;
using System.Collections.Generic;
using UnityEngine;

public class FindYourChadInteractableManager : MonoBehaviour
{

    [SerializeField] private List<FindYourChadObj> _mObj;
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
                NbObjToSpawn = 11;
                //pos.y -= _mSpace / 2;
                break;
            case 2:
                NbObjToSpawn = 17;
                //pos.y -= _mSpace;

                break;
            case 3:
                NbObjToSpawn = 26;
                //pos.y -= _mSpace * 2;
                break;
            default:
                break;
        }

        int Random = UnityEngine.Random.Range(5, NbObjToSpawn);
        _mObj[Random].Real = true;

        var objColliderOffset = _mObj[Random].Collider.offset;
        var objColliderSize = _mObj[8].Collider.size;

        var tempChadColliderOffset = _mObj[8].Collider.offset;
        var tempChadColliderSize = _mObj[8].Collider.size;

        var tempSprite = _mObj[Random].SpriteRenderer.sprite;
        _mObj[Random].SpriteRenderer.sprite = _mObj[8].SpriteRenderer.sprite;
        _mObj[8].SpriteRenderer.sprite = tempSprite;



        var tempsScale = _mObj[Random].transform.localScale;
        _mObj[Random].transform.localScale = _mObj[8].transform.localScale;
        _mObj[8].transform.localScale = tempsScale;

        _mObj[Random].Collider.offset = tempChadColliderOffset;
        _mObj[Random].Collider.size = tempChadColliderSize;

        _mObj[8].Collider.offset = objColliderOffset;
        _mObj[8].Collider.size = objColliderSize;

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
