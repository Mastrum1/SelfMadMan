using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyRoadManager : MiniGameManager
{
    [SerializeField] private GameObject mDirtyAdds;
    [SerializeField] private int mNumOfAdds;
    [SerializeField] private DirtyRoadInteractableManager mInteractableManager;
    public float miniGameTime;
    private void Awake()
    {
        base.Awake();
        SpawnPornAdds(mNumOfAdds);
        mInteractableManager.OnGameEnd += OnGameEnd;
       
    }


    
    void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            OnGameEnd(false);
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
            GameObject ad = Instantiate(mDirtyAdds, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 3.5f), 0.0f), Quaternion.identity);
            ad.transform.SetParent(mInteractableManager.dirtyAddParent.transform);
        }
    }

    private void OnDestroy()
    {
        mInteractableManager.OnGameEnd -= OnGameEnd;
    }
}
