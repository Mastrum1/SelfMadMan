using System.Collections.Generic;
using UnityEngine;

public class ShakeItUpManager : MiniGameManager
{
    [SerializeField] private ShakeItUpInteractableManager _interactableManager;
    [SerializeField] private GameObject _waterParticleParent;
    [SerializeField] private List<Rigidbody2D> _waterParticle;
    
    void Start()
    {
        for (int i = 0; i < _waterParticleParent.transform.childCount; i++)
        {
            var particleChild = _waterParticleParent.transform.GetChild(i).GetComponent<Rigidbody2D>();
            _waterParticle.Add(particleChild);
        }

        _interactableManager.OnGameEnd += OnGameEnd;
    }
    
    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }
    
    public void ChangeGravityOrientation(Quaternion orientation)
    {
        Vector3 euler = orientation.eulerAngles;
        euler.z = 0;
        Quaternion flattenedQuaternion = Quaternion.Euler(euler);

        float angle = Mathf.Atan2(flattenedQuaternion.y, flattenedQuaternion.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        float radians = angle * Mathf.Deg2Rad;

        Vector2 newGravityDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        Physics2D.gravity = newGravityDirection.normalized * Physics2D.gravity.magnitude;
    }

    public void ApplyAccelerometerForce(Vector3 force)
    {
        var speed = 15;

        foreach (var particle in _waterParticle)
        {
            particle.AddForce(new Vector2(force.x * speed,force.y * speed));
        }
    }
}


