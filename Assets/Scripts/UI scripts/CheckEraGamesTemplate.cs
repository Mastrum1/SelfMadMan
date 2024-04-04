using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckEraGamesTemplate : MonoBehaviour
{
    public TMP_Text EraGameName { get => _mEraGameName; set => _mEraGameName = value; }
    [SerializeField] private TMP_Text _mEraGameName;

    public Image EraGameIcon { get => _mEraGameIcon; set => _mEraGameIcon = value; }
    [SerializeField] private Image _mEraGameIcon;

    public Image EraGameBorder { get => _mEraGameBorder; set => _mEraGameBorder = value; }
    [SerializeField] private Image _mEraGameBorder;

    public bool GameLocked { get => _mGameLocked; set => _mGameLocked = value; }
    [SerializeField] private bool _mGameLocked;

    public Sprite EraGameBorderLocked { get => _mEraGameBorderLocked; set => _mEraGameBorderLocked = value; }
    [SerializeField] private Sprite _mEraGameBorderLocked;

    public Sprite EraGameBorderUnlocked { get => _mEraGameBorderUnlocked; set => _mEraGameBorderUnlocked = value; }
    [SerializeField] private Sprite _mEraGameBorderUnlocked;
}
