using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite;

    private ItemsSO _item;
    public ItemsSO Item { get => _item; set => _item = value; }

    public void InitQuarter(ItemsSO item)
    {
        Debug.Log("dez");
        _item = item;
        _itemSprite.sprite = _item.Icon;
    }
}