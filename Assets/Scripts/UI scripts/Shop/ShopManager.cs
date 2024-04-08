using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Money")]
    [SerializeField] private Money _mMoney;

    [Header("Item Shop SO")]
    [SerializeField] private Items[] _SpinWheel;
    [SerializeField] private ItemsSO[] _mCoins;
    [SerializeField] private ItemsSO[] _mFurnitures;
    [SerializeField] private ItemsSO[] _mPowerUp;

    [Header("Coins Template")]
    [SerializeField] private List<ShopTemplate> _mCoinsTemplates;
    [SerializeField] private List<ShopTemplate> _mFurnituresTemplates;
    /*[Header("Templates Panels")]
    [SerializeField] private GameObject _mSpinWheelContainer;
    [SerializeField] private GameObject _mTemplateContainer;
    [SerializeField] private GameObject _mShopFurnituresContainer;
    [SerializeField] private GameObject _mShopPowerUpContainer;
    [SerializeField] private GameObject _mTemplatePrefab;*/

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
        /*CheckPurchasablePerType(_mShopItems, _mTemplateContainer);
        CheckPurchasablePerType(_mFurnitures, _mShopFurnituresContainer);
        CheckPurchasablePerType(_mPowerUp, _mShopPowerUpContainer);*/
    }

    public void CheckPurchasablePerType(ItemsSO[] item, GameObject container)
    {
        for (int i = 0; i < item.Length; i++)
        {
            ShopTemplate TemplateInfos = container.transform.GetChild(i).GetComponent<ShopTemplate>();
            if (_mMoney.CurrentMoney >= item[i].Cost)
            {
                Color newColor = TemplateInfos.PurchaseBox.color;
                newColor.a = 1f;
                TemplateInfos.PurchaseBox.color = newColor;
            }
            else if (_mMoney.CurrentMoney < item[i].Cost)
            {
                Color newColor = TemplateInfos.PurchaseBox.color;
                newColor.a = 0.5f;
                TemplateInfos.PurchaseBox.color = newColor;
            }
        }
    }
    
    public void PurchaseItem(Items[] item, int index, int cost)
    {
        if (_mMoney.CurrentMoney >= cost)
        {
            _mMoney.CurrentMoney = _mMoney.CurrentMoney -= cost;
            CheckPurchasable() ;
            Debug.Log("Remaining coins: " + _mMoney.CurrentMoney);
        }
        else
        {
            Debug.Log("Insufficient coins to purchase item at index: " + index);
        }
    }

    public void OnImageClicked(Items[] item, int index, int cost)
    {
        PurchaseItem(item, index, cost);
    }

    public void LoadAllPanels()
    {
        LoadPanels(_mCoins);
        LoadPanels(_mFurnitures);
        LoadPanels(_mPowerUp);
    }

    public void LoadPanels(ItemsSO[] item)
    {
        for (int i = 0; i < item.Length; i++) 
        {
            _mCoinsTemplates[i].TitleText.text = item[i].name;
            _mCoinsTemplates[i].CostText.text = item[i].Cost.ToString();
            _mCoinsTemplates[i].ImageItem.sprite = item[i].Icon;
            _mCoinsTemplates[i].Type = item[i].Type;

            /*if (item[i] is Coins)
            {
                // Cast the item to Coins type to access the Amount property
                Coins coinItem = (Coins)item[i];
                _mCoinsTemplates[i].Amount.text = coinItem.Amount.ToString();
            }*/
        }
    }
}
