using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawn : MonoBehaviour
{

    [SerializeField] private BoxCollider2D _mSpawnBounds;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Buttons"))
        {
            gameObject.transform.position = new Vector3(
                   Random.Range(_mSpawnBounds.bounds.min.x, _mSpawnBounds.bounds.max.x),
                   Random.Range(_mSpawnBounds.bounds.min.y, _mSpawnBounds.bounds.max.y),
                   gameObject.transform.position.z
                   );
        }
    }
    private void Update()
    {

        this.enabled = false;

    }
}
