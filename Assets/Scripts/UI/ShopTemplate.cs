using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text TitleText { get => _mTitleText; set => _mTitleText = value; }
    [SerializeField] public TMP_Text _mTitleText;

    public TMP_Text CostText { get => _mCostText; set => _mCostText = value; }
    [SerializeField] public TMP_Text _mCostText;

    public Image ImageItem { get => _mImageItem; set => _mImageItem = value; }
    [SerializeField] private Image _mImageItem;
}
