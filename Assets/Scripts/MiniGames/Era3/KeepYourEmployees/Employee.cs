using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Employee : MonoBehaviour
{
    public Action<GameObject> OnTap;
    [SerializeField] private float _mSpeed;
    private Vector3 _mDirection = Vector3.left;
    private bool _mSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
           // _mDirection = Vector3.left;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_mDirection * _mSpeed * Time.deltaTime);
    }

    public void OnSelected()
    {
        if (!_mSelected) {
            OnTap?.Invoke(this.gameObject);
         //   this.gameObject.SetActive(false);
            _mSelected = true;
        }
    }

    private void OnEnable()
    {
        _mSelected = false;
    }
}
