using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quarter : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;

    private ItemsSO _item;
    public ItemsSO Item { get => _item; set => _item = value; }

    public void InitQuarter(ItemsSO item)
    {
        _item = item;
        _itemSprite.overrideSprite = _item.Icon;
    }
}