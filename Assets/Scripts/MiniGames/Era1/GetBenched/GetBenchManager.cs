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

    [SerializeField] private List<CircleCollider2D> _mButtonsCollider;

    [SerializeField] private List<TextMeshProUGUI> _mButtonsText;

    [SerializeField] private BoxCollider2D _mSpawnBounds;

    private bool _mIsSpawning = false;

    private int _mNumberToShow;


    [SerializeField] private float _mCircleSpawnTime = 1f;

    private int _mIndexToActivate = -1;

    private int _mNbRep = 0;


    private void Start()
    {
        _mInteractableManager.OnGameEnd += OnGameEnd;
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
        if (_mTimer.TimerValue == 0)
        {
            Debug.Log("Time's up");
            OnGameEnd(true);
        }
    }

    public void ChangeJames()
    {
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

    private void CheckCollide()
    {
        _mIsSpawning = true;
        bool ChangeSpawn = false;

        for (int i = 0; i < _mButtons.Count - 1; i++)
        {
            if (i == _mIndexToActivate)
            {
                continue;
            }
            if (!_mButtonsScript[i].StopTorus && _mButtons[i].activeSelf)
            {
                CircleCollider2D collider = _mButtons[_mIndexToActivate].GetComponent<CircleCollider2D>();
                if (collider.transform.position.x - collider.bounds.extents.x < _mButtonsCollider[i].transform.position.x + _mButtonsCollider[i].bounds.extents.x && collider.transform.position.x - collider.bounds.extents.x > _mButtonsCollider[i].transform.position.x - _mButtonsCollider[i].bounds.extents.x ||
                    collider.transform.position.x + collider.bounds.extents.x < _mButtonsCollider[i].transform.position.x + _mButtonsCollider[i].bounds.extents.x && collider.transform.position.x + collider.bounds.extents.x > _mButtonsCollider[i].transform.position.x - _mButtonsCollider[i].bounds.extents.x ||
                    collider.transform.position.y - collider.bounds.extents.y < _mButtonsCollider[i].transform.position.y + _mButtonsCollider[i].bounds.extents.y && collider.transform.position.y - collider.bounds.extents.y > _mButtonsCollider[i].transform.position.y - _mButtonsCollider[i].bounds.extents.y ||
                    collider.transform.position.y + collider.bounds.extents.y < _mButtonsCollider[i].transform.position.y + _mButtonsCollider[i].bounds.extents.y && collider.transform.position.y + collider.bounds.extents.y > _mButtonsCollider[i].transform.position.y - _mButtonsCollider[i].bounds.extents.y)
                {
                    ChangeSpawn = true;
                    break;
                }
            }
        }
        if (ChangeSpawn)
        {
            _mButtons[_mIndexToActivate].transform.position = new Vector3(
               Random.Range(_mSpawnBounds.bounds.min.x, _mSpawnBounds.bounds.max.x),
               Random.Range(_mSpawnBounds.bounds.min.y, _mSpawnBounds.bounds.max.y),
               _mButtons[_mIndexToActivate].transform.position.z
               );
            CheckCollide();


        }
        else
        {
            _mIsSpawning = false;
            _mButtons[_mIndexToActivate].GetComponent<SpriteRenderer>().enabled = true;
            _mButtonsText[_mIndexToActivate].enabled = true;
            _mButtonsScript[_mIndexToActivate].Torus.GetComponent<SpriteRenderer>().enabled = true;
            _mButtonsScript[_mIndexToActivate].StopTorus = false;
        }
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
                    yield return new WaitForSeconds(_mCircleSpawnTime);
                }
                else
                {

                    _mNumberToShow++;
                    _mButtonsText[_mIndexToActivate].text = _mNumberToShow.ToString();
                    _mButtons[_mIndexToActivate].SetActive(true);
                    CheckCollide();
                }
                yield return new WaitForSeconds(_mCircleSpawnTime);
            }
            else
            {
                yield return null;
            }

        }

    }
}
