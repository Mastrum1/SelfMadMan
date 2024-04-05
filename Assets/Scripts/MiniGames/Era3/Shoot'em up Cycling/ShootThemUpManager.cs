using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootThemUpManager : MiniGameManager
{
    [SerializeField] private ShootThemUpInteractableManager _mInteractableManager;

    [SerializeField] private List<Transform> _mSpawn;

    [SerializeField] private GameObject _mParents;

    [SerializeField] private float _mEcoloSpawnTime = 1f;


    private void Start()
    {
        _mInteractableManager.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnEnable()
    {
        StartCoroutine("SpawnButtons");
    }

    private void OnDisable()
    {
        _mInteractableManager.OnGameEnd -= OnGameEnd;
        StopCoroutine("SpawnButtons");
    }

    public override void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            OnGameEnd(true);
        }
    }

    private IEnumerator SpawnButtons()
    {
        while (true)
        {
            int index = Random.Range(0, _mSpawn.Count);
            for (int i = 0; i < _mParents.transform.childCount; i++)
            {
                var addChild = _mParents.transform.GetChild(i);
                if (addChild.gameObject.activeSelf == true)
                {
                    continue;
                }
                else
                {
                    addChild.transform.position = _mSpawn[index].position;
                    if(index == 1)
                    {
                        addChild.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        addChild.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    addChild.gameObject.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(_mEcoloSpawnTime);

        }

    }

}
