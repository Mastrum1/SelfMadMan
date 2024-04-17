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
        _mInteractableManager.ChangePos += ChangePos;
    }

    void OnGameEnd(bool win)
    {
        StopCoroutine("SpawnArrow");

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
        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
        {
            StopCoroutine("SpawnArrow");
            _mInteractableManager.DespawnObjects();
            _mInteractableManager.DisableAllSwipe();
            Debug.Log("Time's up");
            OnGameEnd(true);
        }
    }

    public void ChangePos(string Dir)
    {
        switch (Dir)
        {
            case "Left":
                _mJamesPoses[0].SetActive(false);
                _mJamesPoses[1].SetActive(true);
                _mJamesPoses[2].SetActive(false);
                _mJamesPoses[3].SetActive(false);
                break;
            case "Up":
                _mJamesPoses[0].SetActive(false);
                _mJamesPoses[1].SetActive(false);
                _mJamesPoses[2].SetActive(true);
                _mJamesPoses[3].SetActive(false);
                break;
            case "Right":
                _mJamesPoses[0].SetActive(false);
                _mJamesPoses[1].SetActive(false);
                _mJamesPoses[2].SetActive(false);
                _mJamesPoses[3].SetActive(true);
                break;
            default:
                break;
        }
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
