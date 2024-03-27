using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShakeItUpManager : MiniGameManager
{
    [SerializeField] private GameObject _waterParticleParent;

    private List<GameObject> _waterParticle;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _waterParticleParent.transform.childCount; i++)
        {
            var particleChild = _waterParticleParent.transform.GetChild(i).gameObject;
            _waterParticle.Add(particleChild);
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
        Debug.Log(force);

        foreach (var particle in _waterParticle)
        {
            particle.transform.Translate(force);
        }
    }
}


