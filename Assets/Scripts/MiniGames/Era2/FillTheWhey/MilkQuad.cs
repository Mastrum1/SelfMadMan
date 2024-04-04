using UnityEngine;

public class MilkQuad : MonoBehaviour
{
    [SerializeField] private GameObject _milk;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("MilkParticles")) return;

        if (!_milk.gameObject.activeSelf) _milk.gameObject.SetActive(true);

        transform.localScale += new Vector3(0, 1.2f * Time.deltaTime, 0);
    }
}
