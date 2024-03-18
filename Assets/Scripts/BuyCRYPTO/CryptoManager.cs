using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CryptoManager : MiniGameManager
{
    public float miniGameTime;
    [SerializeField] private Sprite[] cryptoCharts;
    [SerializeField] private Image cryptoChartOutput;
    [SerializeField] private CryptoInteractableManager _cryptoIManager;
    
    private int _activeChart;

    void Awake()
    {
        DrawGraph();
        _cryptoIManager.OnPostItClicked += CheckCryptoGraphPressed;
    }
    
    void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            EndMiniGame(false, miniGameScore);
        }
    }

    void DrawGraph()
    {
        _activeChart = Random.Range(0, cryptoCharts.Length);
        cryptoChartOutput.sprite = cryptoCharts[_activeChart];
    }

    public void CheckCryptoGraphPressed(Sprite sprite)
    {
        if (cryptoChartOutput.sprite == sprite)
        {
            print("Win");
            EndMiniGame(true, miniGameScore);
        }
        else
        {
            print("Loose");
            EndMiniGame(false, miniGameScore);
        }
    }

    private void OnDestroy()
    {
        _cryptoIManager.OnPostItClicked -= CheckCryptoGraphPressed;
    }
}
