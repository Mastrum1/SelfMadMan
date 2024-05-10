using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class KeepYourEmployeesGameManager : MiniGameManager
{
    // Start is called before the first frame update

    [SerializeField] int _mNbEmployees = 5;
    [SerializeField] GameObject _employeesPrefab;
    [SerializeField] GameObject _mParent;
    [SerializeField] TMP_Text _mNb;
    private List<GameObject> _mEmployees;
    private float _mAverageSpawnRate;
    private int _mNbRemain;
    private bool _mIsEnd = false;
    Dictionary<int, List<float>> mSpawnPoints;

    void Start()
    {
        mSpawnPoints = new Dictionary<int, List<float>>();
        mSpawnPoints.Add(0, new List<float>());
        mSpawnPoints.Add(1, new List<float>());
        mSpawnPoints.Add(2, new List<float>());
        //mSpawnPoints[0].Add(0.8f);
        //mSpawnPoints[0].Add(0.2f);
        mSpawnPoints[0].Add(0.7f);
        mSpawnPoints[0].Add(0.35f);
        mSpawnPoints[0].Add(1);
        mSpawnPoints[1].Add(0.25f);
        mSpawnPoints[1].Add(0.45f);
        mSpawnPoints[1].Add(2);
        mSpawnPoints[2].Add(-0.9f);
        mSpawnPoints[2].Add(0.5f);
        mSpawnPoints[2].Add(3);

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
        _mNb.text =  (_mEmployees.Count - _mNbRemain).ToString() + "/" + _mEmployees.Count.ToString();
    }

    IEnumerator EmployeeSpawner()
    {
        while (!_mIsEnd) {
            yield return new WaitForSeconds(  _mAverageSpawnRate);
            GameObject mEmployee = GetEmployee();
            if (mEmployee != null && !_mIsEnd) {
                Employee mEmp = mEmployee.GetComponent<Employee>();
                mEmployee.SetActive(true);

                int i =  Random.Range(0, 3);
                int x =  (Random.value < 0.5) ? -3 : 3;
                float y =  mSpawnPoints[i][0];
                float scale = mSpawnPoints[i][1];
                mEmployee.GetComponent<SpriteRenderer>().sortingOrder = (int)mSpawnPoints[i][2];
                mEmployee.transform.localScale = new Vector3(scale, scale, scale);
                mEmployee.transform.position = new Vector3(x, y, 0);
                mEmp.OnTap += OnEmployeeCaught;
            }
        }
    }

    Vector3 GetRandomPosition()
    {

        int i =  Random.Range(0, 3);
        int x =  (Random.value < 0.5) ? -3 : 3;
        float y =  mSpawnPoints[i][0];
        float z = mSpawnPoints[i][1];
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
        for (int i = 0; i < _mNbEmployees; i++)
            _mEmployees[i].GetComponent<Employee>().OnTap -= OnEmployeeCaught;
    }
}
