using TMPro;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] Money _mMoney;

    // Update is called once per frame
    void Update()
    {
        m_TextMeshPro.text = GameManager.instance.GetComponent<Player>().Money.ToString();
    }
}
