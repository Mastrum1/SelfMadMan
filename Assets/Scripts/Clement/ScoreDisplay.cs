using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _mscoreText;

    void FixedUpdate()
    {
        _mscoreText.text = GameManager.instance.DisplayScore();
    }
}
