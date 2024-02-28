using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public delegate void MiniGameEndHandler(bool won, int score);
    public event MiniGameEndHandler OnMiniGameEnd;

    public int miniGameScore;
    public bool win;
    public bool end;

    public void EndMiniGame(bool won, int score)
    {
        // Trigger the event
        OnMiniGameEnd?.Invoke(won, score);
    }

}
