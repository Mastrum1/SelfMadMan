using System;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItUpManager : MiniGameManager
{
    [SerializeField] private ShakeItUpInteractableManager _interactableManager;
    [SerializeField] private GameObject _waterParticleParent;
    [SerializeField] private List<Rigidbody2D> _waterParticle;
     
    private Quaternion _defaultOrientation;
    private Vector2 _defaultGravityDirection;
    private bool _isAccelerometer;

    private void Start()
    {
        _defaultOrientation = Input.gyro.attitude;
        _defaultGravityDirection = GetGravityDirection(_defaultOrientation);
        
        for (var i = 0; i < _waterParticleParent.transform.childCount; i++)
        {
            var particleChild = _waterParticleParent.transform.GetChild(i).GetComponent<Rigidbody2D>();
            _waterParticle.Add(particleChild);
        }

        _interactableManager.OnGameEnd += OnGameEnd;
    }

    public void DisableShake()
    {
        _isAccelerometer = false;
    }
    
    private void OnGameEnd(bool win)
    {
        if (win)
        {
            Amount = _interactableManager.NumProteinDead;
        }
        EndMiniGame(win, miniGameScore);
    }
    
    public void ChangeGravityOrientation(Quaternion orientation)
    {
        var euler = orientation.eulerAngles;
        euler.z = 0;
        var flattenedQuaternion = Quaternion.Euler(euler);

        var angle = Mathf.Atan2(flattenedQuaternion.y, flattenedQuaternion.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        var radians = angle * Mathf.Deg2Rad;

        var newGravityDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        Physics2D.gravity = newGravityDirection.normalized * Physics2D.gravity.magnitude;
    }
    
    private Vector2 GetGravityDirection(Quaternion orientation)
    {
        var euler = orientation.eulerAngles;
        euler.z = 0;
        var flattenedQuaternion = Quaternion.Euler(euler);

        var angle = Mathf.Atan2(flattenedQuaternion.y, flattenedQuaternion.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        var radians = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public void ApplyAccelerometerForce(Vector3 force)
    {
        const int speed = 15;

        foreach (var particle in _waterParticle)
        {
            particle.AddForce(new Vector2(force.x * speed,force.y * speed));
        }
    }

    private void OnDisable()
    {
        Physics2D.gravity = _defaultGravityDirection.normalized * Physics2D.gravity.magnitude;
    }
}


