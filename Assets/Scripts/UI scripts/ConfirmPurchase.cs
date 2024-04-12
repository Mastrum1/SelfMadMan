using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConfirmPurchase : MonoBehaviour
{
    public event Action<ItemsSO> _mItemPurchased;
    [SerializeField] private TMPro.TMP_Text _mItemPrice;
    [SerializeField] private Image _mItemIcon;
    [SerializeField] private Animator _mConfirmAnim;

    private ItemsSO _mItem;

    public void SetItemInfo(ItemsSO item)
    {
        _mItem = item;
        _mItemPrice.text = item.Cost.ToString();
        _mItemIcon.sprite = item.Icon;

        if (item.Icon == null)
            _mItemIcon.gameObject.SetActive(false);
        else
            _mItemIcon.gameObject.SetActive(true);
    }

    public void OnPurchaseConfirmed()
    {
        _mItemPurchased.Invoke(_mItem);
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
