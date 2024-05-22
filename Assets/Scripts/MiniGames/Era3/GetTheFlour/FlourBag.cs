using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlourBag : MonoBehaviour
{
    public event Action OnLose;
    public event Action OnBagGrabbed;
    
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
            OnBagGrabbed?.Invoke();
            _James.GetComponent<VFXScaleUp>().OnObjectClicked();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            StartCoroutine(Respawn());
            return;
        }

        if (other.CompareTag("Ground"))
        {
            OnLose?.Invoke();
        }
    }

    IEnumerator Respawn()
    {
        transform.position = new Vector3(Random.Range(_leftMaxSpawn.transform.position.x, _rightMaxSpawn.transform.position.x), _rightMaxSpawn.transform.position.y);
        yield return new WaitForSeconds(Random.Range(0.01f, 5f));
        _rigidbody.AddForce(new Vector2(0,-100 + -GameManager.instance.FasterLevel * 10)); 
    }

    private void OnDestroy()
    {
        StopCoroutine(Respawn());
    }
}
