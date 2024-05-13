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
    [SerializeField] private List<FurnitureSO> _FurnituresSO;

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
        if (!isSpnning)
        {
            if (MoneyManager.Instance.CurrentMoney < 100)
            {
                Debug.Log("No Money");
            }
            else
            {
                MoneyManager.Instance.SubtractMoney(100);
                InitSpin();
                isSpnning = true;
                currentSpeed = initialSpeed;
            }
        }
    }

    private bool CheckIfAvailableMinigames()
    {
        int index = 0;
        int numOfMinigames = 0;
        foreach (var era in GameManager.instance.ErasData)
        {
            index += era.Unlocked ? 1 : 0;
        }
        for (int i = 0; i < index; i++)
        {
            foreach (var Minigame in MiniGameSelector.instance.AllMinigames[i])
            {
                numOfMinigames += Minigame.Locked ? 1 : 0;
            }
        }
        return numOfMinigames != 0;
    }
    void InitSpin()
    {
        _minigamesOnWheel = 0;
        _quarters.Shuffle();
        FillWithMinigames();
    }

    private bool CheckIfMinigameUnlocked(MinigameSO minigameSO)
    {
        int index = 0;

        foreach (var era in GameManager.instance.ErasData)
        {
            index += era.Unlocked ? 1 : 0;
        }

        for (int i = 0; i < index; i++)
        {
            foreach (var Minigame in MiniGameSelector.instance.AllMinigames[i])
            {
                if (Minigame.SceneName == minigameSO.MinigameScene)
                {
                    return !Minigame.Locked;
                }
            }
        }
        return false;
    }

    private bool CheckIfFurnitureUnlocked(FurnitureSO furnitureSO)
    {

            foreach (var Furniture in FurnitureManager.instance.FurnitureList)
            {
                if (Furniture.PrefabParent.name == furnitureSO.FurniturePrefab.name)
                {
                    return !Furniture.Locked;
                }
            }
        
        return false;
    }

    private bool CheckIfAvailablFurnitures()
    {
        int numOfFurnitures = 0;

        foreach (var Furniture in FurnitureManager.instance.FurnitureList)
        {
            numOfFurnitures += Furniture.Locked ? 1 : 0;
        }

        return numOfFurnitures != 0;
    }


    private void FillWithMinigames()
    {
        Debug.Log("With minigames");
        int M = CheckIfAvailableMinigames() ? 2 : 0;
        int F = CheckIfAvailablFurnitures() ? 2 : 0;

        Debug.Log("MINIGAMES : " + M);
        Debug.Log("FURNITURES : " + F);
        MinigameSO tempMinigame;
        FurnitureSO tempFunriture;
        for (int i = 0; i < M; i++)
        {
            do
            {
                int randomIndex = UnityEngine.Random.Range(0, _MinigamesSO.Count);
                tempMinigame = _MinigamesSO[randomIndex];
            } while (!GameManager.instance.ErasData[tempMinigame.Era - 1].Unlocked || CheckIfMinigameUnlocked(tempMinigame));
            _quarters[i].InitQuarter(tempMinigame);
        }
        for (int i = M; i < F + M; i++)
        {
            do
            {
                int randomIndex = UnityEngine.Random.Range(0, _FurnituresSO.Count);
                tempFunriture = _FurnituresSO[randomIndex];
            } while (CheckIfFurnitureUnlocked(tempFunriture));
            _quarters[i].InitQuarter(tempFunriture);
        }
        for (int i = M + F; i < 8; i++)
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
            case ItemsSO.TYPE.FURNITURE:
                FurnitureItem furniture = new FurnitureItem();
                FurnitureSO furnitureSO = quarter.Item as FurnitureSO;
                furniture.PrefabName = furnitureSO.FurniturePrefab.name;
                return furniture;
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

            if (_quarter.Item is CoinsSO)
            {
                StartCoroutine(ShopManager.Instance.MoveMoney(ShopManager.Instance.CoinAnim[1]));
            }
        }
    }
}
