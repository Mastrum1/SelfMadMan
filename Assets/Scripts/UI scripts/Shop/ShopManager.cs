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

    [Header("Shop Template")]
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
        CheckPurchasable();
    }

    public void CheckPurchasable()
    {
        CheckPurchasableCoins(_mCoins);
    }

    public void CheckPurchasableCoins(ItemsSO[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (_mMoney.CurrentMoney >= item[i].Cost)
            {
                Color newColor = _mCoinsTemplates[i].PurchaseBox.color;
                newColor.a = 1f;
                _mCoinsTemplates[i].PurchaseBox.color = newColor;
                _mCoinsTemplates[i].TemplateBox.enabled = true;
            }
            else
            {
                Color newColor = _mCoinsTemplates[i].PurchaseBox.color;
                newColor.a = 0.5f;
                _mCoinsTemplates[i].PurchaseBox.color = newColor;
                _mCoinsTemplates[i].TemplateBox.enabled = false;
            }
        }
    }

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

    public void PurchaseItem(ItemsSO item) 
    {
        if (ItemsSO.TYPE.COINS == item.Type)
        {
            CoinsSO coinSO = (CoinsSO)item;
            _mMoney.AddMoney(coinSO.Amount);
        }
    }
}
