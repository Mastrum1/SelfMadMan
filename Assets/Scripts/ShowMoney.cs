using TMPro;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] Money m_Money;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshPro.text = m_Money.CurrentMoney.ToString();
    }
}
