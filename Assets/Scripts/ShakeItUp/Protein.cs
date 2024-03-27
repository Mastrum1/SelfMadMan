using System;
using Unity.VisualScripting;
using UnityEngine;

public class Protein : MonoBehaviour
{
    public event Action OnDeath; 
    
    [SerializeField] private Rigidbody2D _proteibRb;
    [SerializeField] private float _resistance;
    
    private Vector3 _scale;
    // Start is called before the first frame update
    void Start()
    {
        _scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_scale.x < 0.05f)
        {
            OnDeath?.Invoke();
            Destroy(this);
        }
    }

    public void ReduceScale(Vector3 force)
    {
        var speed = 0.15f;
        _proteibRb.AddForce(new Vector2(force.x * speed, force.y * speed));
        
        var shakeForce = Mathf.Abs((force.x * speed + force.y * speed) / 2);
        if (shakeForce < _resistance) return;

        _scale.x -= Mathf.Clamp(_scale.x - (shakeForce / _resistance * 100), 0f, 0.02f);
        _scale.y -= Mathf.Clamp(_scale.y - (shakeForce / _resistance * 100), 0f, 0.02f);
        //Debug.Log(_scale + " Scale");
        transform.localScale = _scale;
    }
}
