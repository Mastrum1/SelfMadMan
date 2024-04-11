using UnityEngine;

public class MilkQuad : MonoBehaviour
{
    [SerializeField] private GameObject _milk;

    private bool _isHolding;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("MilkParticles")) return;
        
        if (!_isHolding) return;

        if (!_milk.gameObject.activeSelf) _milk.gameObject.SetActive(true);

        transform.localScale += new Vector3(0, (1.2f + (float)GameManager.instance.FasterLevel/60) * Time.deltaTime, 0);
    }

    public void OnHold(bool hold)
    {
        _isHolding = hold;
    }
}
