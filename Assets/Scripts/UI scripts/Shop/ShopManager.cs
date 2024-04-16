using System.Collections.Generic;
using TMPro;
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

    [Header("Confirm Purchase")]
    [SerializeField] private ConfirmPurchase _mConfirmPurchase;
    [SerializeField] private ItemsSO _mItemBeingPurchased;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadAllPanels();
        CheckPurchasable();
        _mConfirmPurchase._mItemPurchased += HandleItemPurchased;
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

    public void HandleItemPurchased(ItemsSO item)
    {
        _mItemBeingPurchased = item;
        _mConfirmPurchase.SetItemInfo(item);
        _mConfirmPurchase.OpenPopUP();
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
                _mCoinsTemplates[i].Amount.text = "x" + coinSO.Amount.ToString();
            }
        }
    }

    public void PurchaseItem() 
    {
        Debug.Log(_mItemBeingPurchased);
        if (_mItemBeingPurchased.Type == ItemsSO.TYPE.COINS)
        {
            Coins coin = new Coins();
            CoinsSO coinSO = _mItemBeingPurchased as CoinsSO;
            coin.Amount = coinSO.Amount;
            Items items = coin;

            if (items != null)
            {
                Debug.Log(coin.Amount);
                items.Obtain();
            }
        }

        if (_mMoney.CurrentMoney <= _mItemBeingPurchased.Cost)
        {
            Debug.Log("Not enough money");
        }
        else
        {
            _mMoney.SubtractMoney(_mItemBeingPurchased.Cost);
            Debug.Log(_mMoney.CurrentMoney);
        }
    }

    public void HeartPlus(TMP_Text price)
    {

        if (_mMoney.CurrentMoney <= int.Parse(price.text))
        {
            Debug.Log("No Money");
        }
        else
        {
            _mMoney.SubtractMoney(int.Parse(price.text));
            GameManager.instance.Player.Hearts += 1;
        }
    }
}
