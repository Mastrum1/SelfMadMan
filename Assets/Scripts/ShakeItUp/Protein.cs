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
        _resistance = 10f;
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
        Debug.Log("t nul");
        var speed = 0.15f;
        //Debug.Log(_shakeForce);
        _proteibRb.AddForce(new Vector2(force.x * speed, force.y * speed));
        
        var shakeForce = Mathf.Abs((force.x * speed + force.y * speed) / 2);
        if (shakeForce < _resistance) return;

        _scale.x -= Mathf.Clamp(_scale.x - (shakeForce / _resistance * 100), 0f, 0.02f);
        _scale.y -= Mathf.Clamp(_scale.y - (shakeForce / _resistance * 100), 0f, 0.02f);
        Debug.Log(_scale + " Scale");
        //transform.localScale = _scale;
    }
}
