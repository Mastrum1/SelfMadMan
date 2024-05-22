using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootThemUpManager : MiniGameManager
{
    [SerializeField] private ShootThemUpInteractableManager _mInteractableManager;

    [SerializeField] private List<Transform> _mSpawn;

    [SerializeField] private List<GameObject> _Ecolo;

    [SerializeField] private List<GameObject> _Parachutistes;

    [SerializeField] private float _mEcoloSpawnTime = 1f;


    private void Start()
    {
        _mInteractableManager.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        StopCoroutine("SpawnEcolo");
        EndMiniGame(win, miniGameScore + _mInteractableManager.Score);
    }

    private void OnEnable()
    {
        StartCoroutine("SpawnEcolo");
    }

    private void OnDisable()
    {
        _mInteractableManager.OnGameEnd -= OnGameEnd;
        StopCoroutine("SpawnEcolo");
    }

    public override void Update()
    {
        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
        {
            Debug.Log("Time's up");
            OnGameEnd(true);
        }
    }

    private IEnumerator SpawnEcolo()
    {
        while (true)
        {
            int index = Random.Range(0, _mSpawn.Count);
            Debug.Log(index + "index");
            if (index == 2)
            {
                for (int i = 0; i < _Parachutistes.Count; i++)
                {
                    if (_Parachutistes[i].gameObject.activeSelf != true)
                    {
                        _Parachutistes[i].transform.position = _mSpawn[index].position;
                        _Parachutistes[i].gameObject.SetActive(true);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _Ecolo.Count; i++)
                {
                    if (_Ecolo[i].gameObject.activeSelf != true)
                    {
                        _Ecolo[i].transform.position = _mSpawn[index].position;
                        if (index == 1)
                        {
                            _Ecolo[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            _Ecolo[i].transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        _Ecolo[i].gameObject.SetActive(true);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(_mEcoloSpawnTime);

        }

    }

}
