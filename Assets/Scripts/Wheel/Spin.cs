using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
public class Spin : MonoBehaviour
{

    [SerializeField] private Transform _contentTransform;
    [SerializeField] private Arrow _arrow;

    [SerializeField] private List<ItemsSO> _itemsSOs;
    [SerializeField] private List<MinigameSO> _MinigamesSO;

    [SerializeField] private List<Quarter> _quarters;

    [SerializeField] private GameObject _mPopupObtained;
    [SerializeField] private PopUpObtained _mPopupObtainedScript;

    private Quarter _quarter;
    public bool isSpnning = false;
    public float initialSpeed = 500.0f;
    public float decelerationRate = 50.0f;
    private float currentSpeed;
    private float _minigamesOnWheel;



    void Start()
    {
        InitSpin();
    }

    public void StartSpinning()
    {
        isSpnning = true;
        currentSpeed = initialSpeed;
    }
    void InitSpin()
    {
        _minigamesOnWheel = 0;
        _quarters.Shuffle();
        for (int i = 0; i < 2; i++)
        {
            MinigameSO temp;
            do
            {
                int randomIndex = UnityEngine.Random.Range(0, _MinigamesSO.Count);
                temp = _MinigamesSO[randomIndex];

            } while (!GameManager.instance.ErasData[temp.Era - 1].Unlocked);
            _quarters[i].InitQuarter(temp);
        }
        for (int i = 2; i < 8; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _itemsSOs.Count);
            _quarters[i].InitQuarter(_itemsSOs[randomIndex]);
        }
    }



    void Update()
    {
        if (isSpnning)
        {
            _contentTransform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
            currentSpeed -= decelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Max(0.0f, currentSpeed);

            if (currentSpeed == 0.0f)
                ClaimObject();
        }

    }
    public Items CreateItemObject(Quarter quarter)
    {
        switch (quarter.Item.Type)
        {
            case ItemsSO.TYPE.COINS:
                Coins coin = new Coins();
                CoinsSO coinSO = quarter.Item as CoinsSO;
                coin.Amount = coinSO.Amount;
                return coin;
            case ItemsSO.TYPE.MINIGAME:
                MinigameItem minigame = new MinigameItem();
                MinigameSO minigameSO = quarter.Item as MinigameSO;
                minigame.SceneName = minigameSO.MinigameScene;
                return minigame;
            default:
                return null;
        }
    }

    void ClaimObject()
    {
        _quarter = _arrow.FetchQuarterData();
        isSpnning = false;

        _mPopupObtained.SetActive(true);
        _mPopupObtainedScript.OnObtainPopup(_quarter.Item);

        Items item = CreateItemObject(_quarter);
        if (item != null)
        {
            item.Obtain();
        }


    }
}
