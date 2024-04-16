using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeSpawn : MonoBehaviour
{
    public event Action<ChangeSpawn> ChangeSpawnState;

    [SerializeField] private BoxCollider2D _mSpawnBounds;
    [SerializeField] private SpriteRenderer _mObjectSpriteRenderer;
    [SerializeField] private SpriteRenderer _mTorusSpriteRenderer;
    [SerializeField] private TapWithTimer _mTapWithTimer;
    [SerializeField] private TextMeshProUGUI _mText;
    [SerializeField] private GameObject _Parent;

    Coroutine _mRespawnCoroutine;

    private int _mIndex;

    private float _mMinX;
    private float _mMinY;
    private float _mMaxX;
    private float _mMaxY;

    private int _mColliderCount = 0;

    private void Start()
    {
        _mMinX = _mSpawnBounds.bounds.min.x;
        _mMinY = _mSpawnBounds.bounds.min.y;
        _mMaxX = _mSpawnBounds.bounds.max.x;
        _mMaxY = _mSpawnBounds.bounds.max.y;
    }
    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        _mRespawnCoroutine = StartCoroutine(RespawnButton());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buttons") && collision.gameObject != _Parent)
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
        if (collision.gameObject.CompareTag("Buttons") && collision.gameObject != _Parent)
        {
            _mColliderCount--;
        }
    }


    public void CheckCollision()
    {
        if (_mColliderCount == 0)
        {
            StopCoroutine(_mRespawnCoroutine);
            ChangeSpawnState?.Invoke(this);
            _mColliderCount = 0;
            _mTapWithTimer.StopTorus = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void EnableObject()
    {
        _mTorusSpriteRenderer.enabled = true;
        _mObjectSpriteRenderer.enabled = true;
        _mTapWithTimer.Number.enabled = true;
        _mTorusSpriteRenderer.gameObject.SetActive(true);
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
            CheckCollision();
            yield return null;

        }
    }

}
