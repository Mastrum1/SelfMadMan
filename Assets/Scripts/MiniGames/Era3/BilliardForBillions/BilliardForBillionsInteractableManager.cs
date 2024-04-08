using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BilliardForBillionsInteractableManager : InteractableManager
{
    public event Action<bool> OnWin; 
    
    [SerializeField] private Hole _hole;
    [SerializeField] private GameObject _cueBall;
    [SerializeField] private GameObject _james;

    private void Start()
    {
        GenerateRandomHolePos();
    }

    private void Update()
    {
        _cueBall.transform.rotation = _james.transform.rotation;
    }

    private void GenerateRandomHolePos()
    {
        _hole.transform.position = new Vector3(Random.Range(-1.3f, 1.3f), 3.65f);
        _hole.OnCueBall += HandleEndGame;
    }

    private void HandleCueBallForce(float force)
    {
        var rigidbody2D = _cueBall.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(_cueBall.transform.up * force);
    }

    private void HandleEndGame()
    {
        OnWin?.Invoke(true);
    }

    private void OnDestroy()
    {
        _hole.OnCueBall -= HandleEndGame;
    }
}
