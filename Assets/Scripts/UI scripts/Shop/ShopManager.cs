using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Money")]
    [SerializeField] private Money _mMoney;

    public ItemsSO[] ShopItems { get => _mShopItems; }
    public ItemsSO[] Furnitures { get => _mFurnitures; }
    public ItemsSO[] PowerUp { get => _mPowerUp; }

    [Header("Item Shop SO")]
    [SerializeField] private Items[] _SpinWheel;
    [SerializeField] private ItemsSO[] _mShopItems;
    [SerializeField] private ItemsSO[] _mFurnitures;
    [SerializeField] private ItemsSO[] _mPowerUp; 

    public GameObject TemplateContainer { get => _mTemplateContainer; }
    public GameObject ShopFurnituresContainer { get => _mShopFurnituresContainer; }
    public GameObject ShopPowerUpContainer { get => _mShopPowerUpContainer; }

    [Header("Templates Panels")]
    [SerializeField] private GameObject _mSpinWheelContainer;
    [SerializeField] private GameObject _mTemplateContainer;
    [SerializeField] private GameObject _mShopFurnituresContainer;
    [SerializeField] private GameObject _mShopPowerUpContainer;
    [SerializeField] private GameObject _mTemplatePrefab;

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
        CheckPurchasablePerType(_mShopItems, _mTemplateContainer);
        CheckPurchasablePerType(_mFurnitures, _mShopFurnituresContainer);
        CheckPurchasablePerType(_mPowerUp, _mShopPowerUpContainer);
    }

    public void CheckPurchasablePerType(ItemsSO[] item, GameObject container)
    {
       /* for (int i = 0; i < item.Length; i++)
        {
            ShopTemplate TemplateInfos = container.transform.GetChild(i).GetComponent<ShopTemplate>();
            if (_mMoney.CurrentMoney >= item[i]. Cost) 
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
        }*/
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
        /*LoadPanels(_mShopItems, _mTemplateContainer);
        LoadPanels(_mFurnitures, _mShopFurnituresContainer);
        LoadPanels(_mPowerUp, _mShopPowerUpContainer);*/
    }

    public void LoadPanels(Items[] item, GameObject container)
    {
        for (int i = 0; i < item.Length; i++) 
        {
         /*   GameObject Templates = Instantiate(_mTemplatePrefab, container.transform);

            // Get the ShopTemplate component
            ShopTemplate TemplatesInfo = Templates.GetComponent<ShopTemplate>();
            TemplatesInfo.TitleText.text = item[i].ItemName;
            TemplatesInfo.CostText.text = item[i].Cost.ToString();
            TemplatesInfo.ImageItem.sprite = item[i].Look;
            TemplatesInfo.Index = i;
            TemplatesInfo.Cost = item[i].Cost;
            TemplatesInfo.Type = item[i].Type;*/
        }
    }
}
