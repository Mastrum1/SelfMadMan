using TMPro;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] Money _mMoney;
    int _mCurrentMoney;

    // Update is called once per frame
    void Update()
    {
        _mCurrentMoney = _mMoney.CurrentMoney;
        m_TextMeshPro.text = _mCurrentMoney.ToString();
    }
}
