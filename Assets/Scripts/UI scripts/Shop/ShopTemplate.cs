using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text TitleText { get => _mTitleText; private set => _mTitleText = value; }
    [SerializeField] public TMP_Text _mTitleText;

    public TMP_Text CostText { get => _mCostText; private set => _mCostText = value; }
    [SerializeField] public TMP_Text _mCostText;

    public TMP_Text Amount { get => _mAmount; private set => _mAmount = value; }    
    [SerializeField] private TMP_Text _mAmount;

    public Image ImageItem { get => _mImageItem; set => _mImageItem = value; }
    [SerializeField] private Image _mImageItem;

    public Image PurchaseBox { get => _mPurchaseBox; set => _mPurchaseBox = value; }
    [SerializeField] private Image _mPurchaseBox;

    public ItemsSO.TYPE Type { get => _mType; set => _mType = value; }
    [SerializeField] private ItemsSO.TYPE _mType;
}
