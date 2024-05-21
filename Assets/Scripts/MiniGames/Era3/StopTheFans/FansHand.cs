using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansHand : MonoBehaviour
{
    [SerializeField] private GameObject _james;

    [SerializeField] private GameObject _hitEffect;

    private Vector3 _spawnPos;
    public Vector3 SpawnPos { get => _spawnPos; set => _spawnPos = value; }
    // Start is called before the first frame update
    [SerializeField] private float _speed = 1f;
    private bool _startMoving = false;
    public bool StartMoving { get => _startMoving; set => _startMoving = value; }
    [SerializeField] private float _timeToWait = 1f;
    private bool _taped = false;
    [SerializeField] private float _timeToWaitBeforeDespawn = 0.5f;

    [SerializeField] private ParticleSystem _particleSystem;

    private float _tempSpeed = 0;
    [SerializeField] private float _SlowSpeed = 0.3f;

    [SerializeField] private float _runOutSpeed = 4f;

    [SerializeField] private float scaleUpDuration = 0.5f;
    [SerializeField] private float scaleDownDuration = 0.5f;
    [SerializeField] private float scaleUpFactor = 1.3f;
    private bool isScaling = false;

    private int _SpawnerIndex;
    public int SpawnerIndex { get => _SpawnerIndex; set => _SpawnerIndex = value; }

    private Vector3 originalScale;
    public event Action JamesTouched;
    void Start()
    {
        originalScale = transform.localScale;
    }


    public void ActiveObject()
    {
        _taped = false;
        _particleSystem.gameObject.SetActive(false);
        _hitEffect.SetActive(false);
        Vector3 direction = _james.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f; // Ajustez cette valeur selon l'orientation de votre sprite
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        StartCoroutine(FirstMovement());
    }

    public void OnObjectClicked()
    {
        _hitEffect.SetActive(true);
        _taped = true;
        _particleSystem.gameObject.SetActive(true);
        StartCoroutine(AwaitBeforDespawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StopTheFansJames"))
        {
            JamesTouched?.Invoke();
            StartCoroutine(Pulse());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isScaling)
        {
            // Scale the object smoothly
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 2f);
        }
        if (!_taped)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }
    }

    IEnumerator AwaitBeforeMoving()
    {
        _tempSpeed = _speed;
        _speed = _SlowSpeed;
        yield return new WaitForSeconds(_timeToWait);
        _speed = _tempSpeed;
    }

    IEnumerator AwaitBeforDespawn()
    {
        yield return new WaitForSeconds(_timeToWaitBeforeDespawn);
        switch (_SpawnerIndex)
        {
            case 0:
                while (transform.position.x >= _spawnPos.x)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _runOutSpeed);
                    yield return null;
                }
                gameObject.SetActive(false);
                break;
            case 1:
                while (transform.position.x <= _spawnPos.x)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _runOutSpeed);
                    yield return null;
                }
                gameObject.SetActive(false);
                break;
            case 2:
                while (transform.position.y >= _spawnPos.y)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _runOutSpeed);
                    yield return null;
                }
                gameObject.SetActive(false);
                break;
            case 3:
                while (transform.position.y <= _spawnPos.y)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * _runOutSpeed);
                    yield return null;
                }
                gameObject.SetActive(false);
                break;
        }

    }

    IEnumerator FirstMovement()
    {

        switch (_SpawnerIndex)
        {
            case 0:
                while (transform.position.x <= -3f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                    yield return null;
                }
                StartCoroutine(AwaitBeforeMoving());

                break;
            case 1:
                while (transform.position.x >= 3f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                    yield return null;
                }
                StartCoroutine(AwaitBeforeMoving());

                break;
            case 2:
                while (transform.position.y <= -5.5f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                    yield return null;
                }
                StartCoroutine(AwaitBeforeMoving());

                break;
            case 3:
                while (transform.position.y >= 5.5)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                    yield return null;
                }
                StartCoroutine(AwaitBeforeMoving());

                break;
        }




    }

    IEnumerator Pulse()
    {
        while (true)
        {
            isScaling = true;

            // Scale up to 1.3
            float timer = 0f;
            Vector3 targetScale = originalScale * scaleUpFactor;

            while (timer < scaleUpDuration)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / scaleUpDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            // Scale down smoothly
            timer = 0f;
            while (timer < scaleDownDuration)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timer / scaleDownDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale; // Ensure the scale is exactly the original scale at the end
            isScaling = false;
        }
    }
}
