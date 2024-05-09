using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeepYourEmployeesGameManager : MiniGameManager
{
    // Start is called before the first frame update

    [SerializeField] int _mNbEmployees = 5;
    [SerializeField] GameObject _employeesPrefab;
    [SerializeField] GameObject _mParent;
    private List<GameObject> _mEmployees;
    private float _mAverageSpawnRate;
    private int _mNbRemain;
    private bool _mIsEnd = false;

    void Start()
    {
        _mEmployees = new List<GameObject>();
        _mAverageSpawnRate = _mTimer.TimerValue / (1.5f * _mNbEmployees);
        for (int i = 0; i < _mNbEmployees; i++) {
            Vector3 _mPos = new Vector3(0, 0, 0);
            GameObject tmp = Instantiate(_employeesPrefab, _mPos, Quaternion.identity, _mParent.transform);
            tmp.SetActive(false);
            _mEmployees.Add(tmp);
        }
        _mNbRemain = _mEmployees.Count;
        StartCoroutine(EmployeeSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    IEnumerator EmployeeSpawner()
    {
        while (!_mIsEnd) {
            yield return new WaitForSeconds(  _mAverageSpawnRate);
            GameObject mEmployee = GetEmployee();
            if (mEmployee != null && !_mIsEnd) {
                Employee mEmp = mEmployee.GetComponent<Employee>();
                mEmployee.SetActive(true);
                mEmployee.transform.position = GetRandomPosition();
                mEmp.OnTap += OnEmployeeCaught;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        int x =  (Random.value < 0.5) ? -3 : 3;
        int y =  Random.Range(1, -2);
        int z = 0;
        return new Vector3(x, y, z);
    }

    GameObject GetEmployee()
    {
        for (int i = 0; i < _mNbEmployees; i++) {
            if (!_mEmployees[i].activeInHierarchy)
                return _mEmployees[i];
        }
        return null;
    }

    void OnEmployeeCaught()
    {
        _mNbRemain--;
        if (_mNbRemain == 0) {
            _mIsEnd = true;
            EndMiniGame(true, miniGameScore);
        }
    }

    void OnDestroy()
    {

    }
}
