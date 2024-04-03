using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    [SerializeField] private Transform _contentTransform;
    [SerializeField] private Arrow _arrow;


    [SerializeField] private List<ItemsSO> _itemsSOs;
    [SerializeField] private List<Quarter> _quarters;
    private Quarter _quarter;
    public bool isSpnning = false;
    public float initialSpeed = 500.0f;
    public float decelerationRate = 50.0f;
    private float currentSpeed;

    void Start()
    {
        InitSpin();
        isSpnning = true;
        currentSpeed = initialSpeed;
    }

    void InitSpin()
    {
        foreach (var quarter in _quarters)
        {
            int randomIndex = UnityEngine.Random.Range(0, _itemsSOs.Count);
            quarter.InitQuarter(_itemsSOs[randomIndex]);
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
                minigame.SceneName = minigameSO.name;
                return minigame;
            default:
                return null;
        }
    }

    void ClaimObject()
    {
        _quarter = _arrow.FetchQuarterData();
        isSpnning = false;

        Items item = CreateItemObject(_quarter);
        if (item != null)
        {
            item.Obtain();
        }

    }
}
