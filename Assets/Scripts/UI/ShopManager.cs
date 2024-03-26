using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Money")]
    [SerializeField] private Money _mMoney;

    public Items[] ShopItems { get => _mShopItems; }
    [Header("Item Shop SO")]
    [SerializeField] private Items[] _mShopItems;

    public GameObject TemplateContainer { get => _mTemplateContainer; }
    [Header("Templates Panels")]
    [SerializeField] private GameObject _mTemplateContainer;
    [SerializeField] private GameObject _mTemplatePrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadPanels();
        CheckPurchasable();
    }

    public void CheckPurchasable()
    {
        for (int i = 0; i < _mShopItems.Length; i++)
        {
            ShopTemplate TemplateInfos = _mTemplateContainer.transform.GetChild(i).GetComponent<ShopTemplate>();
            if (_mMoney.CurrentMoney >= _mShopItems[i].Cost) 
            {
                Color newColor = TemplateInfos.PurchaseBox.color;
                newColor.a = 1f; // 1f represents fully visible, 0f would be fully transparent
                TemplateInfos.PurchaseBox.color = newColor;
            }
            else if (_mMoney.CurrentMoney < _mShopItems[i].Cost)
            {
                Color newColor = TemplateInfos.PurchaseBox.color;
                newColor.a = 0.5f; // 1f represents fully visible, 0f would be fully transparent
                TemplateInfos.PurchaseBox.color = newColor;
            }
        }
    }
    
    public void PurchaseItem(int index, int cost)
    {
        if (_mMoney.CurrentMoney >= cost)
        {
            _mMoney.CurrentMoney = _mMoney.CurrentMoney -= cost;
            CheckPurchasable() ;
            Debug.Log("Purchased item at index: " + index);
            Debug.Log("Remaining coins: " + _mMoney.CurrentMoney);
        }
        else
        {
            Debug.Log("Insufficient coins to purchase item at index: " + index);
        }
    }

    public void OnImageClicked(int index, int cost)
    {
        PurchaseItem(index, cost);
    }

    public void LoadPanels()
    {
        for (int i = 0; i < _mShopItems.Length; i++) 
        {
            GameObject Templates = Instantiate(_mTemplatePrefab, _mTemplateContainer.transform);
            ShopTemplate TemplatesInfo = Templates.GetComponent<ShopTemplate>();
            TemplatesInfo.TitleText.text = _mShopItems[i].ItemName;
            TemplatesInfo.CostText.text = _mShopItems[i].Cost.ToString();
            TemplatesInfo.ImageItem.sprite = _mShopItems[i].Look;
            TemplatesInfo.Index = i;
            TemplatesInfo.Cost = _mShopItems[i].Cost;
        }
    }
}
