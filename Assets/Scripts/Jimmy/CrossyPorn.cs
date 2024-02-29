using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrossyRoad : MiniGameManager
{
    [SerializeField] private GameObject mPornAdds;
    [SerializeField] private int mNumOfAdds;
    public float miniGameTime;
    private void Awake()
    {
        _mTimer.ResetTimer(miniGameTime);
    }

    void Start()
    {
        SpawnPornAdds(mNumOfAdds);
    }
    
    void Update()
    {
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        else
        {
            _mTimer.UpdateTimer();
        }
    }

    void SpawnPornAdds(int numOfAdds)
    {
        for (int i = 0; i < numOfAdds; i++)
        {
            Instantiate(mPornAdds, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 3.5f), 0.0f), Quaternion.identity);
        }
    }
}
