using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckEraGamesTemplate : MonoBehaviour
{
    public TMP_Text EraGameName { get => _mEraGameName; private set => _mEraGameName = value; }
    [SerializeField] private TMP_Text _mEraGameName;

    public Image EraGameIcon { get => _mEraGameIcon; private set => _mEraGameIcon = value; }
    [SerializeField] private Image _mEraGameIcon;

    public Image EraGameBorder { get => _mEraGameBorder; private set => _mEraGameBorder = value; }
    [SerializeField] private Image _mEraGameBorder;
}
