using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopTheFansInteractablesManager : MonoBehaviour
{
    public event Action OnJamesTouched;

    [SerializeField] private List<FansHand> _hands;

    [SerializeField] private List<ChangeHandsSpawn> _handsSpawn;

    [SerializeField] private List<BoxCollider2D> _spawners;

    private bool _isSpawning = false;

    [SerializeField] private float _timeToWait = 1f;

    public int Score => _score;
    private int _score;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            _hands[i].JamesTouched += EndGame;
            _hands[i].OnClicked += IncrementHandsClicked;
        }
        for (int i = 0; i < _handsSpawn.Count; i++)
        {
            _handsSpawn[i].ChangeSpawnState += HandleChangeSpawn;
        }
        StartCoroutine(SpawnHands());
    }

    private void IncrementHandsClicked()
    {
        _score += 10;
    }
    
    public void EndGame()
    {
        OnJamesTouched?.Invoke();
    }


    public void StopAllHands()
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            _hands[i].StartMoving = false;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            _hands[i].JamesTouched -= EndGame;
        }
        for (int i = 0; i < _handsSpawn.Count; i++)
        {
            _handsSpawn[i].ChangeSpawnState -= HandleChangeSpawn;
        }
        StopCoroutine(SpawnHands());
    }

    void HandleChangeSpawn(ChangeHandsSpawn script)
    {
        _isSpawning = false;

        script.EnableObject();
    }

    IEnumerator SpawnHands()
    {
        int random;
        while (true)
        {
            if (!_isSpawning)
            {

                random = UnityEngine.Random.Range(0, _hands.Count);
                if (_hands[random].gameObject.activeSelf == true)
                    yield return new WaitForSeconds(_timeToWait / GameManager.instance.FasterLevel);
                else
                {

                    FansHand fanHand = _hands[random];
                    int randomSpawner = UnityEngine.Random.Range(0, _spawners.Count);
                    BoxCollider2D collider = _spawners[randomSpawner];
                    Bounds bounds = _spawners[randomSpawner].bounds;

                    float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
                    float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

                    Vector3 randomPoint = new Vector3(randomX, randomY, -2);
                    fanHand.gameObject.transform.position = randomPoint;
                    fanHand.SpawnerIndex = randomSpawner;
                    fanHand.SpawnPos = _spawners[randomSpawner].gameObject.transform.position;
                    _handsSpawn[random].SpawnBounds = collider;
                    fanHand.gameObject.SetActive(true);

                    _isSpawning = true;
                    yield return new WaitForSeconds(_timeToWait / GameManager.instance.FasterLevel);
                }
            }
            else
            {
                yield return new WaitForSeconds(_timeToWait / GameManager.instance.FasterLevel);
            }

        }
    }
}
