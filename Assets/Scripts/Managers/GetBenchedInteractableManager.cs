using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBenchedInteractableManager : MonoBehaviour
{
    public event Action<bool> OnGameEnd;
    public event Action OnChangeSpawnState;

    [SerializeField] private List<TapWithTimer> _mTapWithTimers;
    [SerializeField] private List<ChangeSpawn> _mChangeSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < _mTapWithTimers.Count; i++)
        {
            _mTapWithTimers[i].OnLoose += HandleEndGame;
        }
        for (int i = 0; i < _mChangeSpawn.Count; i++)
        {
            _mChangeSpawn[i].ChangeSpawnState += HandleChangeSpawn;
        }
    }

    void HandleEndGame(bool win)
    {
        for (int i = 0; i < _mTapWithTimers.Count; i++)
        {
            _mTapWithTimers[i].StopTorus = true;
        }
        OnGameEnd?.Invoke(win);

    }

    void HandleChangeSpawn(ChangeSpawn script)
    {
        OnChangeSpawnState?.Invoke();

        script.EnableObject();
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _mTapWithTimers.Count; i++)
        {
            _mTapWithTimers[i].OnLoose += HandleEndGame;
        }
        for (int i = 0; i < _mChangeSpawn.Count; i++)
        {
            _mChangeSpawn[i].ChangeSpawnState += HandleChangeSpawn;
        }
    }

}
