using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansHand : MonoBehaviour
{
    [SerializeField] private GameObject _james;
    // Start is called before the first frame update
    [SerializeField] private float _speed = 1f;
    private bool _startMoving = false;
    public bool StartMoving { get => _startMoving; set => _startMoving = value; }
    [SerializeField] private float _timeToWait = 1f;
    private bool _taped = false;

    public event Action JamesTouched;
    void Start()
    {

    }

    private void OnEnable()
    {
        Vector3 direction = _james.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f; // Ajustez cette valeur selon l'orientation de votre sprite
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.Translate(Vector3.up * 0.5f);
        StartCoroutine(AwaitBeforeMoving());

    }

    public void OnObjectClicked()
    {
        _taped = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StopTheFansJames"))
        {
            JamesTouched?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_startMoving && !_taped)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }
    }

    IEnumerator AwaitBeforeMoving()
    {
        yield return new WaitForSeconds(_timeToWait);
        if (!_taped)
            _startMoving = true;
    }
}
