using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeItUpManager : MiniGameManager
{
    [SerializeField] private GameObject _waterParticleParent;
    [SerializeField] private GameObject _proteinParent;
    [SerializeField] private GameObject _protein;
    [SerializeField] private List<Rigidbody2D> _waterParticle;
    [SerializeField] private int _numOfProteins;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnProteins();
        
        for (int i = 0; i < _waterParticleParent.transform.childCount; i++)
        {
            var particleChild = _waterParticleParent.transform.GetChild(i).GetComponent<Rigidbody2D>();
            _waterParticle.Add(particleChild);
        }
    }

    void SpawnProteins()
    {
        for (int i = 0; i < _numOfProteins; i++)
        {
            var protein = Instantiate(_protein, new Vector3(0,-2.5f,0),quaternion.identity);
            protein.transform.SetParent(_proteinParent.transform);
            var randScale = Random.Range(0.15f, 0.4f);
            protein.transform.localScale = new Vector3(randScale, randScale, 1);
        }
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


