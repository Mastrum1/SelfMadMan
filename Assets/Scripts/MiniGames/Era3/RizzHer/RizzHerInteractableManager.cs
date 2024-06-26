using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RizzHerInteractableManager : MonoBehaviour
{
    [SerializeField] private RizzHerHand _hand;

    [SerializeField] private ObstacleMovement _OtherHand;

    [SerializeField] private List<GameObject> _leftObj;
    [SerializeField] private List<GameObject> _leftChildObj;

    [SerializeField] private List<GameObject> _midObj;
    [SerializeField] private List<GameObject> _midChildObj;

    [SerializeField] private List<GameObject> _rightObj;
    [SerializeField] private List<GameObject> _rightChildObj;

    [SerializeField] private List<ObstacleMovement> _ScrollingParents;

    bool _EndGame = false;

    public event Action<bool> GameEnd;

    private void Start()
    {
        _hand.OnGameEnd += EndGame;
        SpawnNewObj();
    }

    private void OnDisable()
    {
        _hand.OnGameEnd -= EndGame;
        StopCoroutine(waitBeforeSpawnHand());
    }

    public void EndGame()
    {
        _EndGame = true;
        StartCoroutine(waitBeforeSpawnHand());
    }

    public void SpawnNewObj()
    {
        if (_EndGame)
            _OtherHand.gameObject.SetActive(true);
        else
        {
            int OldSpawn = -1;
            int NbSpawn = UnityEngine.Random.Range(1, 3);
            int Value;
            GameObject TempObj = new GameObject();
            int idToActivate = -1;
            int RandomObj;

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
                        do
                        {
                            RandomObj = UnityEngine.Random.Range(0, _leftObj.Count);
                        } while (_leftObj[RandomObj].activeSelf == true);

                        _leftObj[RandomObj].transform.position = new Vector3(-1.58f, 5.68f, 0);
                        if (RandomObj != 4 && RandomObj != 5)
                        {
                            float random = UnityEngine.Random.Range(0, 8);
                            float rotation = 0;

                            switch (random)
                            {
                                case 0:
                                    rotation = 45;
                                    break;
                                case 1:
                                    rotation = 90;
                                    break;
                                case 2:
                                    rotation = 135;
                                    break;
                                case 3:
                                    rotation = 180;
                                    break;
                                case 4:
                                    rotation = 225;
                                    break;
                                case 5:
                                    rotation = 270;
                                    break;
                                case 6:
                                    rotation = 315;
                                    break;
                                case 7:
                                    rotation = 360;
                                    break;
                            }
                            _leftChildObj[RandomObj].transform.rotation = Quaternion.Euler(0, 0, rotation);
                            Debug.Log(rotation);

                        }

                        _leftObj[RandomObj].SetActive(true);

                        TempObj = _leftObj[RandomObj];

                        break;
                    case 1:
                        do
                        {
                            RandomObj = UnityEngine.Random.Range(0, _midObj.Count);
                        } while (_midObj[RandomObj].activeSelf == true);

                        _midObj[RandomObj].transform.position = new Vector3(0, 5.68f, 0);
                        if (RandomObj != 4 && RandomObj != 5)
                        {
                            float random = UnityEngine.Random.Range(0, 8);
                            float rotation = 0;
                            switch (random)
                            {
                                case 0:
                                    rotation = 45;
                                    break;
                                case 1:
                                    rotation = 90;
                                    break;
                                case 2:
                                    rotation = 135;
                                    break;
                                case 3:
                                    rotation = 180;
                                    break;
                                case 4:
                                    rotation = 225;
                                    break;
                                case 5:
                                    rotation = 270;
                                    break;
                                case 6:
                                    rotation = 315;
                                    break;
                                case 7:
                                    rotation = 360;
                                    break;
                            }
                            Debug.Log(rotation);

                            _midChildObj[RandomObj].transform.rotation = Quaternion.Euler(0, 0, rotation);
                        }
                        _midObj[RandomObj].SetActive(true);
                        TempObj = _midObj[RandomObj];

                        break;
                    case 2:
                        do
                        {
                            RandomObj = UnityEngine.Random.Range(0, _rightObj.Count);
                        } while (_rightObj[RandomObj].activeSelf == true);

                        _rightObj[RandomObj].transform.position = new Vector3(1.58f, 5.68f, 0);
                        if (RandomObj != 4 && RandomObj != 5)
                        {
                            float random = UnityEngine.Random.Range(0, 8);
                            float rotation = 0;
                            switch (random)
                            {
                                case 0:
                                    rotation = 45;
                                    break;
                                case 1:
                                    rotation = 90;
                                    break;
                                case 2:
                                    rotation = 135;
                                    break;
                                case 3:
                                    rotation = 180;
                                    break;
                                case 4:
                                    rotation = 225;
                                    break;
                                case 5:
                                    rotation = 270;
                                    break;
                                case 6:
                                    rotation = 315;
                                    break;
                                case 7:
                                    rotation = 360;
                                    break;
                            }
                            _rightChildObj[RandomObj].transform.rotation = Quaternion.Euler(0, 0, rotation);
                        }

                        _rightObj[RandomObj].SetActive(true);
                        TempObj = _rightObj[RandomObj];

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
    }

    private IEnumerator waitBeforeSpawnHand()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnNewObj();
        StopCoroutine(waitBeforeSpawnHand());
    }


    private void EndGame(bool win)
    {
        for (int i = 0; i < _ScrollingParents.Count; i++)
        {
            _ScrollingParents[i].enabled = false;
        }
        _OtherHand.enabled = false;

        GameEnd?.Invoke(win);
    }
}
