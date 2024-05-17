using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopTheFansInteractablesManager : MonoBehaviour
{
    public event Action OnJamesTouched;

    [SerializeField] private List<FansHand> _hands;

    [SerializeField] private List<BoxCollider2D> _spawners;

    [SerializeField] private float _timeToWait = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            _hands[i].JamesTouched += EndGame;
        }
        StartCoroutine(SpawnHands());
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
        StopCoroutine(SpawnHands());
    }

    IEnumerator SpawnHands()
    {
        int random;
        while (true)
        {
            do
            {
                random = UnityEngine.Random.Range(0, _hands.Count);
            } while (_hands[random].gameObject.activeSelf == true);

            FansHand fanHand = _hands[random];
            Bounds bounds = _spawners[UnityEngine.Random.Range(0, _spawners.Count)].bounds;

            float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

            Vector3 randomPoint = new Vector3(randomX, randomY, 0);
            fanHand.gameObject.transform.position = randomPoint;
            fanHand.gameObject.SetActive(true);

            yield return new WaitForSeconds(_timeToWait);

        }
    }
}
