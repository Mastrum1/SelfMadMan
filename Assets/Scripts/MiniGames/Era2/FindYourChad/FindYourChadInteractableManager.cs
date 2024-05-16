using System;
using System.Collections.Generic;
using UnityEngine;

public class FindYourChadInteractableManager : MonoBehaviour
{

    [SerializeField] private List<FindYourChadObj> _mObj;
    public event Action<bool> GameEnd;
    [SerializeField] private int NbObjToSpawn = 2;

    private void Start()
    {
        int Random = 0;
        Vector3 pos = new Vector3(_mObj[0].transform.position.x, 0, _mObj[0].transform.position.z);
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                NbObjToSpawn = 20;
                Random = UnityEngine.Random.Range(10, NbObjToSpawn);

                //pos.y -= _mSpace / 2;
                break;
            case 2:
                NbObjToSpawn = 25;
                Random = UnityEngine.Random.Range(5, NbObjToSpawn);

                //pos.y -= _mSpace;

                break;
            case 3:
                NbObjToSpawn = 33;
                Random = UnityEngine.Random.Range(5, NbObjToSpawn);
                //pos.y -= _mSpace * 2;
                break;
            default:
                break;
        }

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

        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                for (int id = 5; id < NbObjToSpawn + 5; id++)
                {
                    if (id == Random)
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
                break;
            case 2:
                for (int id = 0; id < NbObjToSpawn; id++)
                {
                    if (id == Random)
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
                break;
            case 3:
                for (int id = 0; id < NbObjToSpawn; id++)
                {
                    if (id == Random)
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
                break;
            default:
                break;
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
