using System;
using System.Collections;
using UnityEngine;

public class Protein : MonoBehaviour
{
    public event Action OnDeath; 
    
    [SerializeField] private Rigidbody2D _proteibRb;
    [SerializeField] private GameObject _deathEffect;
    
    private float _resistance;
    public float Resistance { get => _resistance; set => _resistance = value; }
    private Vector3 _scale;

    private void Start()
    {
        _scale = transform.localScale;
    }

    public void ReduceScale(Vector3 force)
    {
        var speed = force.magnitude/0.2f;
        _proteibRb.AddForce(new Vector2(force.x * speed, force.y * speed));
        
        var shakeForce = Mathf.Abs((force.x * speed + force.y * speed) / 2);
        Debug.Log("ShakeForce: " + shakeForce);
        if (shakeForce < _resistance) return;

        _scale.x -= shakeForce / 800;
        _scale.y -= shakeForce / 800;
        
        transform.localScale = _scale;
        
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (!(_scale.x < 0.05f) || _deathEffect.activeSelf) return;
        
        _deathEffect.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        OnDeath?.Invoke();
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
