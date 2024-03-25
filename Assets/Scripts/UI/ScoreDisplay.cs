using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _mscoreText;

    void Start()
    {
        _mscoreText.text = GameManager.instance.DisplayScore();
    }
}
