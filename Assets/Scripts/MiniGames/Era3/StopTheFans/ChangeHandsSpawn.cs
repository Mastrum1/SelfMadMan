using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeHandsSpawn : MonoBehaviour
{
    public event Action<ChangeHandsSpawn> ChangeSpawnState;

    private BoxCollider2D _mSpawnBounds;
    public BoxCollider2D SpawnBounds { get => _mSpawnBounds; set => _mSpawnBounds = value; }

    [SerializeField] private SpriteRenderer _mObjectSpriteRenderer;
    [SerializeField] private FansHand _fansHand;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private BoxCollider2D _HandCollider;

    Coroutine _mRespawnCoroutine;

    private int _mIndex;

    private float _mMinX;
    private float _mMinY;
    private float _mMaxX;
    private float _mMaxY;

    private int _mColliderCount = 0;

    private void OnEnable()
    {
        _mMinX = _mSpawnBounds.bounds.min.x;
        _mMinY = _mSpawnBounds.bounds.min.y;
        _mMaxX = _mSpawnBounds.bounds.max.x;
        _mMaxY = _mSpawnBounds.bounds.max.y;

        _HandCollider.enabled = false;

        GetComponent<BoxCollider2D>().enabled = true;
        _mRespawnCoroutine = StartCoroutine(RespawnButton());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FansHand") && collision.gameObject != _Parent)
        {
            _mColliderCount++;
            _Parent.transform.position = new Vector3(
                   UnityEngine.Random.Range(_mMinX, _mMaxX),
                   UnityEngine.Random.Range(_mMinY, _mMaxY),
                   -2
                   );
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FansHand") && collision.gameObject != _Parent)
        {
            _mColliderCount--;
        }
    }


    public void CheckCollision()
    {
        if (_mColliderCount == 0)
        {
            Debug.Log("EnableToSpawn");
            StopCoroutine(_mRespawnCoroutine);
            ChangeSpawnState?.Invoke(this);
            _HandCollider.enabled = true;
            _mColliderCount = 0;
            _fansHand.ActiveObject();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void EnableObject()
    {
        _mObjectSpriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        StopCoroutine(_mRespawnCoroutine);
    }

    private IEnumerator RespawnButton()
    {
        yield return new WaitForSeconds(0.05f);
        while (true)
        {
            Debug.Log("Respawn");
            CheckCollision();
            yield return null;

        }
    }

}
