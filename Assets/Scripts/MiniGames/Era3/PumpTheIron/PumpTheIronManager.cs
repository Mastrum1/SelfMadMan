    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpTheIronManager : MiniGameManager
{
    [SerializeField] private PumpTheIronInteractableManager _mInteractableManager;

    [SerializeField] private List<GameObject> _mJamesPoses;

    [SerializeField] private Transform _mSpawnPoint;

    //[SerializeField] private List<GameObject> _mShadowState;

    [SerializeField] private List<GameObject> _mArrows;

    [SerializeField] private float _mArrowSpawnTime = 1f;

    private void Start()
    {
        _mInteractableManager.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnEnable()
    {
        StartCoroutine("SpawnArrow");
    }

    private void OnDisable()
    {
        _mInteractableManager.OnGameEnd -= OnGameEnd;
        StopCoroutine("SpawnArrow");
    }


    override public void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            OnGameEnd(true);
        }
    }

    public void LeftPose()
    {
        Debug.Log("Pose");
    }
    public void UpPose()
    {
        Debug.Log("Pose");
    }
    public void RightPose()
    {
        Debug.Log("Pose");
    }
    public void DownPose()
    {
        Debug.Log("Pose");
    }

    private IEnumerator SpawnArrow()
    {
        while (true)
        {
            int Activate = Random.Range(0, _mArrows.Count - 1);

            while (_mArrows[Activate].activeSelf == true)
            {
                Activate = Random.Range(0, _mArrows.Count - 1);
            }
            _mArrows[Activate].transform.position = _mSpawnPoint.position;
            _mArrows[Activate].SetActive(true);

            yield return new WaitForSeconds(_mArrowSpawnTime);

        }

    }
}
