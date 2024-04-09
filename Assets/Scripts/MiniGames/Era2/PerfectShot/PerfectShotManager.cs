using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectShotManager : MiniGameManager
{

    [SerializeField] private PerfectShotInteractableManager _interactableManager;
    [SerializeField] private PerfectShotUIManager _UIManager;

    void Start()
    {
        Debug.Log("yo");
        _interactableManager.OnGameEnded += HandleGameEnd;
    }

    // Update is called once per frame
    public void HandleGameEnd(bool win)
    {
        EndMiniGame(win, 0);
        if (win)
            _UIManager.OnWin();
    }

    private void OnDestroy()
    {
        _interactableManager.OnGameEnded -= HandleGameEnd;
    }
}
