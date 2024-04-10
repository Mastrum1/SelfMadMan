using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGameManager : MiniGameManager
{
    [SerializeField] PopUpSpawner spawner;

    // Update is called once per frame
    void Update()
    {
        if (_mTimer.timerValue == 0)
            EndMiniGame(false, miniGameScore);
    }

    public void  OnDownload(GameObject button)
    {
        if (!spawner.IsActivePopUp()) {
            //button.SetActive(false);
            EndMiniGame(true, miniGameScore);
        }
    }
}
