using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyAdd : MonoBehaviour
{
    public float Dir { get => _dir; set => _dir = value; }
    [SerializeField] private float _dir;
    public float TimeTilMove { get => _timeTilMove; set => _timeTilMove = value; }
    [SerializeField] private float _timeTilMove;
    
    [SerializeField] private Rigidbody2D _rigid2d;
    [SerializeField] private float _force;

    public Vector3 OrinalPos { get; set; }

    void Start()
    {
        OrinalPos = transform.position;
        StartCoroutine(MoveAfterTime(TimeTilMove));
    }

    IEnumerator MoveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _force = Random.Range(60, 150);
        _rigid2d.AddForce(new Vector2(_force * Dir, 0));
    }
}
