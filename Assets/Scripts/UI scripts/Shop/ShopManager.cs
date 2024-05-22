using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Item Shop SO")]
    [SerializeField] private CoinsSO[] _mCoins;

    public ItemsSO[] Furnitures { get => _mFurnitures; }
    [SerializeField] private ItemsSO[] _mFurnitures;
    [SerializeField] private ItemsSO[] _mPowerUp;

    [Header("Shop Template")]
    [SerializeField] private List<ShopTemplate> _mCoinsTemplates;

    public List<ShopTemplate> FurnituresTemplates { get => _mFurnituresTemplates; }
    [SerializeField] private List<ShopTemplate> _mFurnituresTemplates;

    [Header("Confirm Purchase")]
    [SerializeField] private ConfirmPurchase _mConfirmPurchase;
    public ShopTemplate ItemBeingPurchased {  get => _mItemBeingPurchased; }
    [SerializeField] private ShopTemplate _mItemBeingPurchased;

    [Header("Pulse of the buttons")]
    [SerializeField] private LeanPulseScale _mSpinButtonPulse;

    [Header("FurnitureManager")]
    [SerializeField] private FurnitureManager _mFurnitureManager;

    public GameObject[] CoinAnim { get => _mCoinAnim; set => _mCoinAnim = value; }
    [Header("Coin Animation")]
    [SerializeField] private GameObject[] _mCoinAnim;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadFurniture(_mFurnitures);
        LoadAllPanels();
        Pulse();
        _mConfirmPurchase._mItemPurchased += HandleItemPurchased;
    }

    public void HandleItemPurchased(ShopTemplate item)
    {
        _mItemBeingPurchased = item;
        _mConfirmPurchase.SetItemInfo(item);
        _mConfirmPurchase.OpenPopUP();
    }

    public void LoadAllPanels()
    {
        if (TutorialManager.instance.InTutorial && (TutorialManager.instance.StepNbr == 3))
        {
            MoneyManager.Instance.AddMoney(100);
            StartCoroutine(ShopManager.Instance.MoveMoney(ShopManager.Instance.CoinAnim[2]));
            TutorialManager.instance.StepInit();
        }
        LoadPanels(_mCoins);
        //
    }

    public void LoadPanels(ItemsSO[] item)
    {
        for (int i = 0; i < item.Length; i++) 
        {
            if (item[i].Type == ItemsSO.TYPE.COINS)
            {
                _mCoinsTemplates[i].ItemInfo = item[i];
                _mCoinsTemplates[i].TitleText.text = item[i].ItemName;
                _mCoinsTemplates[i].CostText.text = item[i].Cost.ToString();
                _mCoinsTemplates[i].ImageItem.sprite = item[i].Icon;
                _mCoinsTemplates[i].Type = item[i].Type;
                CoinsSO coinSO = (CoinsSO)item[i];
                _mCoinsTemplates[i].Amount.text = "x" + coinSO.Amount.ToString();
            }
        }
    }

    public void LoadFurniture(ItemsSO[] items)
    { 
        List<ItemsSO> availableItems = new List<ItemsSO>(items);

        for (int i = 0; i < _mFurnituresTemplates.Count; i++)
        {
            // Check if there are any available items left
            if (availableItems.Count > 0)
            {
                // Randomly select an item from the available items list
                int randomIndex = Random.Range(0, availableItems.Count);
                ItemsSO selectedItem = availableItems[randomIndex];

                // Check if the selected item is furniture
                if (selectedItem.Type == ItemsSO.TYPE.FURNITURE)
                {
                    FurnitureSO furnitureSO = selectedItem as FurnitureSO;

                    foreach (var fur in _mFurnitureManager.FurnitureList)
                    {
                        if (furnitureSO.FurniturePrefab.name == fur.PrefabParent.name)
                        {
                            if (fur.Locked)
                            {
                                // Assign item info to the furniture template
                                _mFurnituresTemplates[i].ItemInfo = selectedItem;
                                _mFurnituresTemplates[i].TitleText.text = selectedItem.ItemName;
                                _mFurnituresTemplates[i].CostText.text = selectedItem.Cost.ToString();
                                _mFurnituresTemplates[i].ImageItem.sprite = selectedItem.Icon;
                                _mFurnituresTemplates[i].Type = selectedItem.Type;
                                _mFurnituresTemplates[i].ONOFF();

                                // Remove the selected item from the available items list
                                availableItems.RemoveAt(randomIndex);
                            }
                            else if (!fur.Locked)
                            {
                                do
                                {
                                    randomIndex = Random.Range(0, availableItems.Count);
                                    selectedItem = availableItems[randomIndex];
                                }
                                while (CheckIfFurnitureUnlocked(selectedItem as FurnitureSO));

                                _mFurnituresTemplates[i].TitleText.text = "";
                                _mFurnituresTemplates[i].ImageItem.sprite = _mFurnituresTemplates[i].Nothing;
                                _mFurnituresTemplates[i].Purchasable = false;
                                _mFurnituresTemplates[i].ONOFF();
                            }
                        }
                    }
                }
            }
        }
    }

    public void CheckUnlocked(ItemsSO item)
    {
        if (item.Type == ItemsSO.TYPE.FURNITURE)
        {
            for (int i = 0; i < _mFurnituresTemplates.Count; i++)
            {
                FurnitureSO furni = _mFurnituresTemplates[i].ItemInfo as FurnitureSO;

                foreach (var fur in _mFurnitureManager.FurnitureList)
                {
                    if (fur.PrefabParent.name == furni.FurniturePrefab.name && item != null)
                    {
                        if (fur.Locked)
                        {
                            _mFurnituresTemplates[i].Purchasable = true;
                            _mFurnituresTemplates[i].ONOFF();
                        }
                        if (!fur.Locked)
                        {
                            _mFurnituresTemplates[i].Purchasable = false;
                            _mFurnituresTemplates[i].ONOFF();
                        }
                    }
                }
            }
        }
    }

    public void PurchaseItem() 
    {
        Debug.Log(_mItemBeingPurchased);
        if (_mItemBeingPurchased.Type == ItemsSO.TYPE.COINS)
        {
            Coins coin = new Coins();
            CoinsSO coinSO = _mItemBeingPurchased.ItemInfo as CoinsSO;
            coin.Amount = coinSO.Amount;
            Items items = coin;

            if (items != null)
            {
                Debug.Log(coin.Amount);
                MoneyManager.Instance.AddMoney(coin.Amount);
            }
        }

        if (MoneyManager.Instance.CurrentMoney < _mItemBeingPurchased.ItemInfo.Cost)
        {
            Debug.Log("Not enough money");
            //Play a invlid buzz sound
        }
        else if (_mItemBeingPurchased.Type == ItemsSO.TYPE.FURNITURE) 
        {
            FurnitureItem fur = new FurnitureItem();
            FurnitureSO furSO = _mItemBeingPurchased.ItemInfo as FurnitureSO;
            fur.PrefabName = furSO.FurniturePrefab.name;
            Debug.Log(fur.PrefabName);
            Items items = fur;

            if (items != null) 
            {
                fur.Obtain();
                SubsMoney();
                _mItemBeingPurchased.ImageItem.sprite = _mItemBeingPurchased.Nothing;
                _mItemBeingPurchased.Purchasable = false;
                _mItemBeingPurchased.ONOFF();
            }
        }
    }

    public void HeartPlus(TMP_Text price)
    {

        if (MoneyManager.Instance.CurrentMoney < int.Parse(price.text))
        {
            Debug.Log("No Money");
            //Play a invlid buzz sound
        }
        else
        {
            MoneyManager.Instance.SubtractMoney(int.Parse(price.text));
            GameManager.instance.Player.Hearts += 1;
        }
    }

    public void Pulse()
    {
        if (MoneyManager.Instance.CurrentMoney >= 100)
        {
            _mSpinButtonPulse.enabled = true;
        }
        else
        {
            _mSpinButtonPulse.enabled = false;
        }
    }

    private bool CheckIfFurnitureUnlocked(FurnitureSO furnitureSO)
    {
        foreach (var fur in _mFurnitureManager.FurnitureList)
        {
            if (furnitureSO.FurniturePrefab.name == fur.PrefabParent.name)
            {
                return fur.Locked;
            }
        }
        return false;
    }

    private void SubsMoney()
    {
        MoneyManager.Instance.SubtractMoney(_mItemBeingPurchased.ItemInfo.Cost);
        StartCoroutine(MoveMoney(_mCoinAnim[0]));
        Debug.Log(MoneyManager.Instance.CurrentMoney);
    }

    public IEnumerator MoveMoney(GameObject particule)
    {
        particule.SetActive(true);
        yield return new WaitForSeconds(3f);
        particule.SetActive(false);
    }
}
