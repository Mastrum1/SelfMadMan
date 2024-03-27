using UnityEngine;

public class Protein : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _proteibRb;
    
    private float _resistance;
    private Vector3 _scale;
    private float _shakeForce;
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
            Destroy(this);
        }
    }

    public void ReduceScale(Vector3 force)
    {
        var speed = 0.15f;
        _shakeForce = (force.x * speed + force.y * speed) / 2;
        Debug.Log(_shakeForce);
        _proteibRb.AddForce(new Vector2(force.x * speed,force.y * speed));
        
        _scale -= new Vector3(_shakeForce / _resistance, _shakeForce / _resistance);
    }
}
