using UnityEngine;

public class CueBall : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody;
    
    void Update()
    {
        transform.up = _rigidbody.velocity;
    }
}
