using System;
using System.Collections;
using UnityEngine;

public class JamesPool : MonoBehaviour
{
    [SerializeField] private GameObject _queue;
    [SerializeField] private GameObject _cueBall;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private GameObject _maxPosLeft;
    [SerializeField] private GameObject _maxPosRight;
    [SerializeField] private GameObject _queueStartPos;
    [SerializeField] private GameObject _queueEndPos;

    private bool _isHolding;
    private bool _isPulling;
    private void Update()
    {
        // Calculate the direction to look at the _cueBall
        var direction = _cueBall.transform.position - transform.position;
        var angle1 = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle1, Vector3.forward);
    }

    public void OnDrag(Vector3 dragPosition)
    {
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
        if (_isHolding) return;
        
        _isHolding = true;
        StartCoroutine(MovingQueue());
    }

    private IEnumerator MovingQueue()
    {
        var moveSpeed = 0.5f;
        var threshold = 0.01f;

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

            yield return null; // Use null to wait for the next frame
        }
    }

    public void StopQueue()
    {
        _isHolding = false;
    }
}
