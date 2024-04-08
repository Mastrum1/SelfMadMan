using System;
using UnityEngine;

public class JamesPool : MonoBehaviour
{
    [SerializeField] private GameObject _cueBall;

    private void Update()
    {
        // Calculate the direction to look at the _cueBall
        var direction = _cueBall.transform.position - transform.position;
        var angle1 = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle1, Vector3.forward);
    }

    public void OnDrag(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, transform.position.y);
    }
}
