using System;
using UnityEngine;

public class JamesPool : MonoBehaviour
{
    [SerializeField] private GameObject _queue;
    [SerializeField] private GameObject _cueBall;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private GameObject _maxPosLeft;
    [SerializeField] private GameObject _maxPosRight;

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
        
        var directionToDrag = dragPosition - _cueBall.transform.position;
        var angleToDrag = Mathf.Atan2(directionToDrag.y, directionToDrag.x) * Mathf.Rad2Deg;
        var newPosition = _cueBall.transform.position + Quaternion.Euler(0f, 0f, angleToDrag) * Vector3.right * _radius;
        transform.position = newPosition;
    }
}
