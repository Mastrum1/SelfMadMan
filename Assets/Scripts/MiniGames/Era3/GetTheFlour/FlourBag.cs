using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlourBag : MonoBehaviour
{
    public event Action OnLose;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _leftMaxSpawn;
    [SerializeField] private GameObject _rightMaxSpawn;
    [SerializeField] private GameObject _James;
    private void OnEnable()
    {
        StartCoroutine(Respawn());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("James"))
        {
            _James.GetComponent<VFXScaleUp>().OnObjectClicked();
            StartCoroutine(Respawn());
            return;
        }
        
        OnLose?.Invoke();
    }

    IEnumerator Respawn()
    {
        transform.position = new Vector3(Random.Range(_leftMaxSpawn.transform.position.x, _rightMaxSpawn.transform.position.x), _rightMaxSpawn.transform.position.y);
        yield return new WaitForSeconds(Random.Range(0.2f, 3f));
        _rigidbody.AddForce(new Vector2(0,-100 + -GameManager.instance.FasterLevel * 10)); 
    }

    private void OnDestroy()
    {
        StopCoroutine(Respawn());
    }
}
