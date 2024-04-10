using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGameManager : MiniGameManager
{
    [SerializeField] PopUpSpawner spawner;

    // Update is called once per frame
    public void  OnDownload(GameObject button)
    {
        if (!spawner.IsActivePopUp()) {
            EndMiniGame(true, miniGameScore);
        }
    }
}
