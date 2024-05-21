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

    public Image ImageItem { get => _mImageItem; private set => _mImageItem = value; }
    [SerializeField] private Image _mImageItem;

    public Sprite Nothing { get => _mNothingImage; private set => _mNothingImage = value; }
    [SerializeField] private Sprite _mNothingImage;

    public Image PurchaseBox { get => _mPurchaseBox; private set => _mPurchaseBox = value; }
    [SerializeField] private Image _mPurchaseBox;

    public ItemsSO.TYPE Type { get => _mType; set => _mType = value; }
    [SerializeField] private ItemsSO.TYPE _mType;

    public BoxCollider2D TemplateBox { get => _mTemplateBox; private set => _mTemplateBox = value; }
    [SerializeField] private BoxCollider2D _mTemplateBox;

    public ItemsSO ItemInfo { get => _mItemSO; set => _mItemSO = value; }
    [SerializeField] private ItemsSO _mItemSO;

    public bool Purchasable;

    private void OnEnable()
    {
        if (MoneyManager.Instance.CurrentMoney <= TextToInt(Amount))
        {
            Purchasable = false;
        }
        else
        {
            Purchasable = true;
        }

        ONOFF();
    }

    public void ONOFF()
    {
        if (Purchasable)
        {
            _mPurchaseBox.color = new Color(1, 1, 1, 1f);
            _mTemplateBox.enabled = true;
        }
        else if (!Purchasable)
        {
            _mPurchaseBox.color = new Color(1, 1, 1, 0.5f);
            _mTemplateBox.enabled = false;
        }
    }

    private int TextToInt(TMP_Text tmpText)
    {
        int result = 0;
        if (tmpText != null)
        {
            int.TryParse(tmpText.text, out result);
        }
        return result;
    }
}
