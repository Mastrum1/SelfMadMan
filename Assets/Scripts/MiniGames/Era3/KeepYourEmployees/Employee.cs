using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Employee : MonoBehaviour
{
    public Action<GameObject> OnTap;
    [SerializeField] private float _mSpeed;
    private Vector3 _mDirection = Vector3.left;
    [SerializeField] private Animator _mAnim;
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
        if (!_mSelected)
            transform.Translate(_mDirection * _mSpeed * Time.deltaTime);
    }

    public void OnSelected()
    {
        if (!_mSelected) {
            _mSelected = true;
            _mAnim.Play("Base Layer.EmployeeDead", 0, 0.25f);
            StartCoroutine(DisableEmployee());
            OnTap?.Invoke(this.gameObject);
        }
    }

    IEnumerator DisableEmployee()
    {
        yield return new WaitForSeconds(0.30f);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _mSelected = false;
    }
}
