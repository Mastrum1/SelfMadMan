using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    private int _activeChart;

    void Start()
    {
        DrawGraph();
        _cryptoIManager.OnPostItClicked += CheckCryptoGraphPressed;

    }

    void DrawGraph()
    {
        _activeChart = Random.Range(0, cryptoCharts.Length);
        cryptoChartOutput.sprite = cryptoCharts[_activeChart];
    }

    public void CheckCryptoGraphPressed(PostIt postIt)
    {

        if (cryptoChartOutput.sprite == postIt.CorrespondingGraph)
        {
            winEffect.transform.position = postIt.transform.position;
            winEffect.SetActive(true);
            print("Win");
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
