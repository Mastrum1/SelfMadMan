using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CryptoManager : MiniGameManager
{
    public float miniGameTime;
    [SerializeField] private Sprite[] cryptoCharts;
    [SerializeField] private Image cryptoChartOutput;
    [SerializeField] private CryptoInteractableManager _cryptoIManager;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private GameObject winEffect;
    private AudioManager _audioManager;

    private int _activeChart;

    private void Start()
    {
        DrawGraph();
        _cryptoIManager.OnPostItClicked += CheckCryptoGraphPressed;
        _audioManager = AudioManager.Instance;
    }

    private void DrawGraph()
    {
        _activeChart = Random.Range(0, cryptoCharts.Length);
        cryptoChartOutput.sprite = cryptoCharts[_activeChart];
    }

    private void CheckCryptoGraphPressed(PostIt postIt)
    {
        _audioManager.PlaySFX(0);
        if (cryptoChartOutput.sprite == postIt.CorrespondingGraph)
        {
            winEffect.transform.position = postIt.transform.position;
            winEffect.SetActive(true);
            print("Win");
            Amount++;
            EndMiniGame(true, miniGameScore);
        }
        else
        {
            print("Loose");
            EndMiniGame(false, miniGameScore);
        }
        foreach (var postIts in _cryptoIManager.PostIts)
        {
            Debug.Log(postIts);
            postIts.SpriteRenderer.material = postIts.CorrespondingGraph == cryptoChartOutput.sprite ? winMaterial : loseMaterial;
        }

    }

    private void OnDestroy()
    {
        _cryptoIManager.OnPostItClicked -= CheckCryptoGraphPressed;
    }
}
