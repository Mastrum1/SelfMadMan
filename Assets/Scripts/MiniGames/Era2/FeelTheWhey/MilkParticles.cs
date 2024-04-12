using UnityEngine;

public class MilkParticles : MonoBehaviour
{
    private Vector3 _startPosition;
    public Vector3 StartPosition => _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Milk"))
        {
            transform.position = _startPosition;
        }
    }
}
