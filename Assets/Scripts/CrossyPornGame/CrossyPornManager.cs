using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrossyPornManager : MiniGameManager
{
    [SerializeField] private GameObject mPornAdds;
    [SerializeField] private int mNumOfAdds;
    [SerializeField] private CrossyPornInteractableManager mInteractableManager;
    public float miniGameTime;
    private void Awake()
    {
        SpawnPornAdds(mNumOfAdds);
        mInteractableManager.OnGameEnd += OnGameEnd;
        _mTimer.ResetTimer(miniGameTime);
    }


    
    void Update()
    {
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            OnGameEnd(false);
        }
        else
        {
            _mTimer.UpdateTimer();
        }
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    void SpawnPornAdds(int numOfAdds)
    {
        for (int i = 0; i < numOfAdds; i++)
        {
            GameObject ad = Instantiate(mPornAdds, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 3.5f), 0.0f), Quaternion.identity);
            ad.transform.SetParent(mInteractableManager.pornAddParent.transform);
        }
    }

    private void OnDestroy()
    {
        mInteractableManager.OnGameEnd -= OnGameEnd;
    }
}
