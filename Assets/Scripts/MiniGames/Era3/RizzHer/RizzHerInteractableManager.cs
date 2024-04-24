using System;
using System.Collections.Generic;
using UnityEngine;

public class RizzHerInteractableManager : MonoBehaviour
{
    [SerializeField] private RizzHerHand _hand;
    [SerializeField] private List<GameObject> _leftObj;

    [SerializeField] private List<GameObject> _midObj;

    [SerializeField] private List<GameObject> _rightObj;

    [SerializeField] private List<ObstacleMovement> _ScrollingParents;

    public event Action<bool> GameEnd;

    private void Start()
    {
        _hand.OnGameEnd += EndGame;
        SpawnNewObj();
    }

    private void OnDisable()
    {
        _hand.OnGameEnd -= EndGame;
    }

    public void SpawnNewObj()
    {
        int OldSpawn = -1;
        int NbSpawn = UnityEngine.Random.Range(1, 3);
        int Value;
        GameObject TempObj = new GameObject();
        int idToActivate = -1;
        for (int i = 0; i < NbSpawn; i++)
        {

            do
            {
                Value = UnityEngine.Random.Range(0, 3);
            } while (Value == OldSpawn);
            OldSpawn = Value;
            switch (Value)
            {
                case 0:
                    if (_leftObj[0].activeSelf == true)
                    {
                        _leftObj[1].transform.position = new Vector3(-1.85f, 3.15f, 0);
                        _leftObj[1].SetActive(true);
                        TempObj = _leftObj[1];
                    }
                    else if (_leftObj[1].activeSelf == true)
                    {
                        _leftObj[2].transform.position = new Vector3(-1.85f, 3.15f, 0);
                        _leftObj[2].SetActive(true);
                        TempObj = _leftObj[2];

                    }
                    else if (_leftObj[2].activeSelf == true)
                    {
                        _leftObj[3].transform.position = new Vector3(-1.85f, 3.15f, 0);
                        _leftObj[3].SetActive(true);
                        TempObj = _leftObj[3];

                    }
                    else
                    {
                        _leftObj[0].transform.position = new Vector3(-1.85f, 3.15f, 0);
                        _leftObj[0].SetActive(true);
                        TempObj = _leftObj[0];

                    }
                    break;
                case 1:
                    if (_midObj[0].activeSelf == true)
                    {
                        _midObj[1].transform.position = new Vector3(0, 3.15f, 0);
                        _midObj[1].SetActive(true);
                        TempObj = _midObj[1];
                    }
                    else if (_midObj[1].activeSelf == true)
                    {
                        _midObj[2].transform.position = new Vector3(0, 3.15f, 0);
                        _midObj[2].SetActive(true);
                        TempObj = _midObj[2];
                    }
                    else if (_midObj[2].activeSelf == true)
                    {
                        _midObj[3].transform.position = new Vector3(0, 3.15f, 0);
                        _midObj[3].SetActive(true);
                        TempObj = _midObj[3];
                    }
                    else
                    {
                        _midObj[0].transform.position = new Vector3(0, 3.15f, 0);
                        _midObj[0].SetActive(true);
                        TempObj = _midObj[0];
                    }

                    break;
                case 2:
                    if (_rightObj[0].activeSelf == true)
                    {
                        _rightObj[1].transform.position = new Vector3(1.85f, 3.15f, 0);
                        _rightObj[1].SetActive(true);
                        TempObj = _rightObj[1];
                    }
                    else if (_rightObj[1].activeSelf == true)
                    {
                        _rightObj[2].transform.position = new Vector3(1.85f, 3.15f, 0);
                        _rightObj[2].SetActive(true);
                        TempObj = _rightObj[2];
                    }
                    else if (_rightObj[2].activeSelf == true)
                    {
                        _rightObj[3].transform.position = new Vector3(1.85f, 3.15f, 0);
                        _rightObj[3].SetActive(true);
                        TempObj = _rightObj[3];
                    }
                    else
                    {
                        _rightObj[0].transform.position = new Vector3(1.85f, 3.15f, 0);
                        _rightObj[0].SetActive(true);
                        TempObj = _rightObj[0];

                    }
                    break;
            }

            if (_ScrollingParents[0].gameObject.activeSelf == false)
            {
                TempObj.transform.SetParent(_ScrollingParents[0].transform);
                idToActivate = 0;
            }
            else if (_ScrollingParents[1].gameObject.activeSelf == false)
            {
                TempObj.transform.SetParent(_ScrollingParents[1].transform);
                idToActivate = 1;
            }
            else if (_ScrollingParents[2].gameObject.activeSelf == false)
            {
                TempObj.transform.SetParent(_ScrollingParents[2].transform);
                idToActivate = 2;
            }
            else
            {
                TempObj.transform.SetParent(_ScrollingParents[3].transform);
                idToActivate = 3;

            }



        }
        if (idToActivate != -1)
            _ScrollingParents[idToActivate].gameObject.SetActive(true);


    }


    private void EndGame(bool win)
    {

        //for (int i = 0; i < _leftObj.Count; i++)
        //{
        //    _leftObj[i].SetActive(false);
        //}
        //for (int i = 0; i < _midObj.Count; i++)
        //{
        //    _midObj[i].SetActive(false);
        //}
        //for (int i = 0; i < _rightObj.Count; i++)
        //{
        //    _rightObj[i].SetActive(false);
        //}
        for (int i = 0; i < _ScrollingParents.Count; i++)
        {
            _ScrollingParents[i].enabled = false;
        }
        GameEnd?.Invoke(win);
    }
}
