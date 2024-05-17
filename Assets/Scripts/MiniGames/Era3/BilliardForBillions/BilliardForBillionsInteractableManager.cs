using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BilliardForBillionsInteractableManager : InteractableManager
{
    public event Action<bool> OnWin; 
    
    [SerializeField] private Hole _hole;
    [SerializeField] private GameObject _cueBall;
    [SerializeField] private GameObject _james;
    [SerializeField] private GameObject _leftPos;
    [SerializeField] private GameObject _rightPos;

    private bool _onWin;
    private bool _isRight;
    private void Start()
    {
        GenerateRandomHolePos();
        StartCoroutine(MoveHole());
    }

    private IEnumerator MoveHole()
    {
        
        const float moveSpeed = 0.4f;
        const float threshold = 0.01f;

        while (!_onWin)
        {
            var position = _hole.transform.position;

            var targetPosition = !_isRight ? _leftPos.transform.position : _rightPos.transform.position;
            
            position = Vector3.MoveTowards(position, targetPosition, (moveSpeed + (float)GameManager.instance.FasterLevel/20) * Time.deltaTime);
            
            _hole.transform.position = position;
            
            if (Vector3.Distance(position, targetPosition) < threshold)
            {
                _isRight = !_isRight;
            }

            yield return null;
        }
    }
    
    private void GenerateRandomHolePos()
    {
        _hole.transform.position = new Vector3(Random.Range(_leftPos.transform.position.x, _rightPos.transform.position.x), _hole.transform.position.y);
        _hole.OnCueBall += HandleEndGame;
    }

    private void HandleEndGame()
    {
        _onWin = true;
        _cueBall.transform.position = _hole.transform.position;
        _cueBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(CueBallAnim());
        OnWin?.Invoke(true);
    }

    private IEnumerator CueBallAnim()
    {
        while (_cueBall.activeSelf)
        {
            _cueBall.transform.localScale -= new Vector3(0.2f * Time.deltaTime, 0.2f * Time.deltaTime);
            if (_cueBall.transform.localScale.x < 0.01f)
            {
                _cueBall.SetActive(false);
            }
            yield return null;
        }
    }

    private void OnDestroy()
    {
        _hole.OnCueBall -= HandleEndGame;
    }
}
