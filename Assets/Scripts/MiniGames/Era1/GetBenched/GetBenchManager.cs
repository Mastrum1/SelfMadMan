using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBenchManager : MiniGameManager
{
    [SerializeField] private GetBenchedInteractableManager _mInteractableManager;

    [SerializeField] private List<GameObject> _mJamesState;

    [SerializeField] private List<GameObject> _mShadowState;

    [SerializeField] private List<GameObject> _mButtons;

    [SerializeField] private List<TapWithTimer> _mButtonsScript;


    [SerializeField] private List<ChangeSpawn> _mButtonsChangeSpawn;

    [SerializeField] private List<TextMeshProUGUI> _mButtonsText;

    [SerializeField] private BoxCollider2D _mSpawnBounds;

    private float _mMinX;
    private float _mMinY;
    private float _mMaxX;
    private float _mMaxY;

    private AudioManager _audioManager;

    private bool _mIsSpawning = false;

    private int _mNumberToShow;


    [SerializeField] private float _mCircleSpawnTime = 1f;

    private int _mIndexToActivate = -1;

    private int _mNbRep = 0;

    public override void Awake()
    {
        base.Awake();
        _mInteractableManager.OnGameEnd += OnGameEnd;
        _mInteractableManager.OnChangeSpawnState += ChangeState;
    }

    private void Start()
    {
        _mMinX = _mSpawnBounds.bounds.min.x;
        _mMinY = _mSpawnBounds.bounds.min.y;
        _mMaxX = _mSpawnBounds.bounds.max.x;
        _mMaxY = _mSpawnBounds.bounds.max.y;
        _audioManager = AudioManager.Instance;

    }

    void OnGameEnd(bool win)
    {
        StopCoroutine("SpawnButtons");
        EndMiniGame(win, miniGameScore);
    }

    private void OnEnable()
    {
        StartCoroutine("SpawnButtons");
    }

    private void OnDisable()
    {
        _mInteractableManager.OnGameEnd -= OnGameEnd;
        StopCoroutine("SpawnButtons");
    }


    override public void Update()
    {
        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
        {
            for (int i = 0; i < _mButtons.Count; i++)
            {

                _mButtons[i].SetActive(false);
            }
            StopCoroutine("SpawnButtons");
            OnGameEnd(true);
        }
    }

    public void ChangeJames()
    {
        Amount++;
        if (_mJamesState[0].activeSelf == true)
        {
            _mJamesState[0].SetActive(false);

            _mShadowState[0].SetActive(false);

            _mJamesState[1].SetActive(true);

            _mShadowState[1].SetActive(true);
        }
        else
        {
            _mJamesState[0].SetActive(true);

            _mShadowState[0].SetActive(true);

            _mJamesState[1].SetActive(false);

            _mShadowState[1].SetActive(false);

            _mNbRep++;
        }
    }

    private void ChangeState()
    {
        _mIsSpawning = false;
    }
    private IEnumerator SpawnButtons()
    {
        while (true)
        {
            if (!_mIsSpawning)
            {
                _mIndexToActivate++;
                if (_mIndexToActivate > _mButtons.Count - 1)
                {
                    _mIndexToActivate = 0;
                }
                if (_mButtons[_mIndexToActivate].activeSelf == true)
                {
                    yield return new WaitForSeconds(_mCircleSpawnTime / GameManager.instance.FasterLevel);
                }
                else
                {

                    _mNumberToShow++;
                    _mButtonsText[_mIndexToActivate].text = _mNumberToShow.ToString();
                    _mButtons[_mIndexToActivate].SetActive(true);
                    _mButtons[_mIndexToActivate].transform.position = new Vector3(
                    Random.Range(_mMinX, _mMaxX),
                    Random.Range(_mMinY, _mMaxY),
                    -2
                    );
                    _mIsSpawning = true;
                }
                yield return new WaitForSeconds(_mCircleSpawnTime / GameManager.instance.FasterLevel);
            }
            else
            {
                yield return new WaitForSeconds(_mCircleSpawnTime / GameManager.instance.FasterLevel);
            }

        }

    }
}
