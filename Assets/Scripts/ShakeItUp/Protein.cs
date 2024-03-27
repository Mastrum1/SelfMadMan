using System;
using UnityEngine;

public class Protein : MonoBehaviour
{
    public event Action OnDeath; 
    
    [SerializeField] private Rigidbody2D _proteibRb;
    
    private float _resistance;
    public float Resistance { get => _resistance; set => _resistance = value; }
    private Vector3 _scale;

    void Start()
    {
        _scale = transform.localScale;
    }
    
    void Update()
    {
        if (_scale.x < 0.05f)
        {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void ReduceScale(Vector3 force)
    {
        var speed = 0.15f;
        _proteibRb.AddForce(new Vector2(force.x * speed, force.y * speed));
        
        var shakeForce = Mathf.Abs((force.x * speed + force.y * speed) / 2);
        if (shakeForce < _resistance) return;

        _scale.x -= shakeForce / 10;
        _scale.y -= shakeForce / 10;
        
        transform.localScale = _scale;
    }
}
