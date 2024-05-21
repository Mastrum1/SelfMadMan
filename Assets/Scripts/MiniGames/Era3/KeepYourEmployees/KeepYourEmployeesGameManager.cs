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
    [SerializeField] GameObject _mClickAnim;
    private List<GameObject> _mEmployees;
    private float _mAverageSpawnRate;
    private int _mNbRemain;
    private bool _mIsEnd = false;
    int _mHit = 0;
    Dictionary<int, List<float>> mSpawnPoints;
    private Vector3 _mHandPos;

    void Start()
    {
        _mHandPos = new Vector3(0, 0, 0);
        mSpawnPoints = new Dictionary<int, List<float>>();
        mSpawnPoints.Add(0, new List<float>());
        mSpawnPoints.Add(1, new List<float>());
        mSpawnPoints.Add(2, new List<float>());
        mSpawnPoints[0].Add(0.6f);
        mSpawnPoints[0].Add(0.2f);
        mSpawnPoints[0].Add(-9);
        mSpawnPoints[1].Add(0.75f);
        mSpawnPoints[1].Add(0.35f);
        mSpawnPoints[1].Add(-8);
        mSpawnPoints[2].Add(-0.3f);
        mSpawnPoints[2].Add(0.5f);
        mSpawnPoints[2].Add(-7);

        _mEmployees = new List<GameObject>();
        _mAverageSpawnRate = _mTimer.TimerValue / (2 * _mNbEmployees);
        for (int i = 0; i < _mNbEmployees; i++) {
            Vector3 _mPos = new Vector3(0, 0, 0);
            GameObject tmp = Instantiate(_employeesPrefab, _mPos, Quaternion.identity, _mParent.transform);
            tmp.SetActive(false);
            _mEmployees.Add(tmp);
        }
        _mNbRemain = _mEmployees.Count;
        StartCoroutine(EmployeeSpawner());
    }

    public void UpdatePos(Vector3 pos)
    {
        pos.z = 0;
        _mHandPos = pos;
    }

    public void PlayTapAnim(Vector3 pos)
    {
        if (_mClickAnim.activeSelf) _mClickAnim.SetActive(false); 
        _mClickAnim.transform.position = _mHandPos; //new Vector3(pos.x, pos.y, 0);
        _mClickAnim.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        _mNb.text = "<size=1.5em>" + (_mHit).ToString() + "</size>/" + _mEmployees.Count.ToString();
    }

    IEnumerator EmployeeSpawner()
    {
        while (!_mIsEnd) {
            yield return new WaitForSeconds(  _mAverageSpawnRate);
            GameObject mEmployee = GetEmployee();
            if (mEmployee != null && !_mIsEnd && _gameIsPlaying) {
                Employee mEmp = mEmployee.GetComponent<Employee>();
                mEmployee.SetActive(true);

                int i =  Random.Range(0, 3);
                int x =  (Random.value < 0.5) ? -4 : 5;
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

    void OnEmployeeCaught(GameObject employee)
    {
        _mNbRemain--;
        _mHit++;
        PlayTapAnim(employee.transform.position);
        //employee.SetActive(false);
        employee.GetComponent<Employee>().OnTap -= OnEmployeeCaught;
        if (_mHit == _mEmployees.Count) {   
            _mIsEnd = true;
            for (int i = 0; i < _mNbEmployees; i++)
                if (_mEmployees[i].activeInHierarchy && _mEmployees[i] != employee ) {
                    _mEmployees[i].GetComponent<Employee>().OnTap -= OnEmployeeCaught;
                    _mEmployees[i].SetActive(false);
                }
            EndMiniGame(true, miniGameScore);
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < _mNbEmployees; i++)
            if (_mEmployees[i].activeInHierarchy)
                _mEmployees[i].GetComponent<Employee>().OnTap -= OnEmployeeCaught;
    }
}
