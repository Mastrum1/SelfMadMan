using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float Speed { get => _speed; set => _speed = value; }
    float _fasterLevel;
    [SerializeField] private GameObject _previous;
    public GameObject Previous { get => _previous; set => _previous = value; }
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                _fasterLevel = GameManager.instance.FasterLevel;
                break;
            default:
                _fasterLevel = (GameManager.instance.FasterLevel / 1.75f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime * _fasterLevel);
    }
}
