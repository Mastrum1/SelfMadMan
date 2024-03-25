using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int _mCoins;
    [SerializeField] private Money _mMoney;

    [Header("Item Shop SO")]
    [SerializeField] private Items[] _mShopItems;

    [Header("Templates Panels")]
    [SerializeField] private GameObject _mTemplateContainer;
    [SerializeField] private GameObject _mTemplatePrefab;

    private void Start()
    {
        LoadPanels();

        if (_mCoins != _mMoney.CurrentMoney)
            _mCoins = _mMoney.CurrentMoney;
        else
            return;
    }

    private void Update()
    {
        
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < _mShopItems.Length; i++)
        {
            ShopTemplate TemplatesInfo = _mTemplateContainer.transform.GetChild(i).GetComponent<ShopTemplate>();
           /* if (_mCoins >= _mShopItems[i].Cost)
                
            else*/
        }
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
        }
    }
}
