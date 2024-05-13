using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPurchase : MonoBehaviour
{
    public event Action<ShopTemplate> _mItemPurchased;
    [SerializeField] private TMPro.TMP_Text _mItemPrice;
    [SerializeField] private Image _mItemIcon;
    [SerializeField] private Animator _mConfirmAnim;

    private ShopTemplate _mShopTemplate;
    private ItemsSO _mItem;

    public void SetItemInfo(ShopTemplate item)
    {
        _mItem = item.ItemInfo;
        _mItemPrice.text = item.ItemInfo.Cost.ToString();
        _mItemIcon.sprite = item.ItemInfo.Icon;

        if (item.ItemInfo.Icon == null)
            _mItemIcon.gameObject.SetActive(false);
        else
            _mItemIcon.gameObject.SetActive(true);
    }

    public void OnPurchaseConfirmed()
    {
        _mItemPurchased.Invoke(_mShopTemplate);
    }

    public void OpenPopUP()
    {
        _mConfirmAnim.SetBool("Open", true);
    }

    public void ClosePopUP()
    {
        _mConfirmAnim.SetBool("Open", false);
    }
}
