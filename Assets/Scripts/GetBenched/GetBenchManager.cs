using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBenchManager : MiniGameManager
{

    [SerializeField]
    private GameObject _mInteractables;

    [SerializeField]
    private List<GameObject> _mJamesState;

    [SerializeField] private List<GameObject> _mButtons;

    [SerializeField] private BoxCollider2D _mSpawnBounds;

    private int _mIndexToActivate = -1;

    private int _mNbRep = 0;

    private Coroutine _mCoroutine;

    public float time = 10f;

    [SerializeField]
    private float _mCircleSpawnTime = 1f;

    private void OnEnable()
    {
        StartCoroutine("SpawnButtons");
    }

    private void OnDisable()
    {
        StopCoroutine("SpawnButtons");
    }


    public void ChangeJames()
    {
        if (_mJamesState[0].activeSelf == true)
        {
            _mJamesState[0].SetActive(false);
            _mJamesState[1].SetActive(true);
        }
        else
        {
            _mJamesState[0].SetActive(true);
            _mJamesState[1].SetActive(false);
            _mNbRep++;
        }
    }

    private IEnumerator SpawnButtons()
    {
        while (true)
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

                _mButtons[_mIndexToActivate].transform.position = new Vector3(
                    Random.Range(_mSpawnBounds.bounds.min.x, _mSpawnBounds.bounds.max.x),
                    Random.Range(_mSpawnBounds.bounds.min.y, _mSpawnBounds.bounds.max.y),
                    _mButtons[_mIndexToActivate].transform.position.z
                    );

                _mButtons[_mIndexToActivate].SetActive(true);
            }
            yield return new WaitForSeconds(_mCircleSpawnTime);

        }

    }
}
