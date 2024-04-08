using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Money")]
    [SerializeField] private Money _mMoney;

    [Header("Item Shop SO")]
    [SerializeField] private Items[] _SpinWheel;
    [SerializeField] private CoinsSO[] _mCoins;
    [SerializeField] private ItemsSO[] _mFurnitures;
    [SerializeField] private ItemsSO[] _mPowerUp;

    [Header("Coins Template")]
    [SerializeField] private List<ShopTemplate> _mCoinsTemplates;
    [SerializeField] private List<ShopTemplate> _mFurnituresTemplates;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadAllPanels();
        //CheckPurchasable();
    }

    /*public void CheckPurchasable()
    {
        *//*CheckPurchasablePerType(_mShopItems);
        CheckPurchasablePerType(_mFurnitures);
        CheckPurchasablePerType(_mPowerUp);*//*
    }*/

    /*public void CheckPurchasablePerType(ItemsSO[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            
        }
    }*/

    public void LoadAllPanels()
    {
        LoadPanels(_mCoins);
    }

    public void LoadPanels(ItemsSO[] item)
    {
        for (int i = 0; i < item.Length; i++) 
        {
            if (item[i].Type == ItemsSO.TYPE.COINS)
            {
                _mCoinsTemplates[i].TitleText.text = item[i].name;
                _mCoinsTemplates[i].CostText.text = item[i].Cost.ToString();
                _mCoinsTemplates[i].ImageItem.sprite = item[i].Icon;
                _mCoinsTemplates[i].Type = item[i].Type;
                CoinsSO coinSO = (CoinsSO)item[i];
                _mCoinsTemplates[i].Amount.text = coinSO.Amount.ToString();
                
            }
        }
    }

   public void PurchaseItem(ItemsSO coin) 
    {
        if (ItemsSO.TYPE.COINS == coin.Type)
        {
            CoinsSO coinSO = (CoinsSO)coin;
            _mMoney.AddMoney(coinSO.Amount);
        }
    }
}
