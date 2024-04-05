using UnityEngine;

public class MilkParticles : MonoBehaviour
{
    private Vector2 _startPosition;
    public Vector2 StartPosition => _startPosition;

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
