using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CryptoManager : MiniGameManager
{
    public float miniGameTime;
    [SerializeField] private Sprite[] cryptoCharts;
    [SerializeField] private Button[] cryptoGraphs;
    [SerializeField] private Image cryptoChartOutput;
    
    private Image _activeCryptoChart;
    private int _activeChart;
    private int _buttonNum;
    void Awake()
    {
        _mTimer.ResetTimer(miniGameTime);
        DrawGraph();
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

    public void CheckCryptoGraphPressed(Button button)
    {
        print("clicked");
        for (int i = 0; i < cryptoGraphs.Length; i++)
        {
            if (button == cryptoGraphs[i])
            {
                _buttonNum = i;
            }
        }

        if (_activeChart == _buttonNum)
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
}
