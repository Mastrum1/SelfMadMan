using System;
using System.Collections;
using UnityEngine;

public class JamesPool : MonoBehaviour
{
    [SerializeField] private GameObject _queue;
    [SerializeField] private GameObject _cueBall;
    [SerializeField] private GameObject _fillBar;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private GameObject _maxPosLeft;
    [SerializeField] private GameObject _maxPosRight;
    [SerializeField] private GameObject _queueStartPos;
    [SerializeField] private GameObject _queueEndPos;
    [SerializeField] private GameObject _fillBarStartPos;
    [SerializeField] private GameObject _fillBarMaxPos;
    [SerializeField] private GameObject _shootLine;
    [SerializeField] private SpriteRenderer _shootLineSpriteRenderer;
        
    private bool _hasShot;
    private bool _isHolding;
    private bool _isPulling;

    private void Start()
    {
        _shootLineSpriteRenderer.material.SetVector("_Fade_Distance", 
            new Vector4(4.6f, -23 + GameManager.instance.FasterLevel));
    }

    private void Update()
    {
        if (!_isHolding) return;
        var direction = _cueBall.transform.position - transform.position;
        var angle1 = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle1, Vector3.forward);
    }

    public void OnDrag(Vector3 dragPosition)
    {
        if (_hasShot) return;
        if (dragPosition.x <= _maxPosLeft.transform.position.x) return;
        if (dragPosition.x >= _maxPosRight.transform.position.x) return;
        if (dragPosition.y >= _maxPosRight.transform.position.y) return;

        var directionToDrag = dragPosition - _cueBall.transform.position;
        var angleToDrag = Mathf.Atan2(directionToDrag.y, directionToDrag.x) * Mathf.Rad2Deg;
        var newPosition = _cueBall.transform.position + Quaternion.Euler(0f, 0f, angleToDrag) * Vector3.right * _radius;
        transform.position = newPosition;
        _queue.transform.rotation = transform.rotation;
    }

    public void MoveQueue()
    {
        if (_hasShot) return;
        if (_isHolding) return;
        
        _isHolding = true;
        StartCoroutine(MovingQueue());
    }

    private IEnumerator MovingQueue()
    {
        const float moveSpeed = 0.2f;
        const float threshold = 0.01f;

        while (_isHolding)
        {
            var position = _queue.transform.position;

            var targetPosition = !_isPulling ? _queueEndPos.transform.position : _queueStartPos.transform.position;
            
            position = Vector3.MoveTowards(position, targetPosition, moveSpeed * Time.deltaTime);
            
            _queue.transform.position = position;
            
            if (Vector3.Distance(position, targetPosition) < threshold)
            {
                _isPulling = !_isPulling;
            }
            
            var distanceCovered = Vector3.Distance(_queueStartPos.transform.position, position);
            var totalDistance = Vector3.Distance(_queueStartPos.transform.position, _queueEndPos.transform.position);
            var percentage = distanceCovered / totalDistance;

            MoveFillBar(percentage);
            
            yield return null;
        }
    }

    private void MoveFillBar(float percentage)
    {
        var targetY = Mathf.Lerp(_fillBarStartPos.transform.position.y, _fillBarMaxPos.transform.position.y, percentage);
        
        var position = _fillBar.transform.position;
        position.y = targetY;
        _fillBar.transform.position = position;
    }

    public void StopQueue()
    {
        _isHolding = false;
        var force = Vector3.Distance(_queueStartPos.transform.position, _queue.transform.position);
        _shootLine.SetActive(false);
        _cueBall.GetComponent<Rigidbody2D>().AddForce(transform.up * (force * 1000));
        _cueBall.transform.GetChild(0).gameObject.SetActive(true);
        _queue.transform.position = _queueStartPos.transform.position;
        _hasShot = true;
    }
}
