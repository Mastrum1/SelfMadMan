using UnityEngine;

public class GetTheFlourGameManager : MiniGameManager
{
    [SerializeField] private GetTheFlourInteractablemanager _interactablemanager;

    private void Start()
    {
        _interactablemanager.OnLoseGame += EndGame;
    }
    
    public override void Update()
    {
        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
            EndMiniGame(true, miniGameScore + _interactablemanager.Score);
    }
    
    private void EndGame(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactablemanager.OnLoseGame -= EndGame;
    }
}
