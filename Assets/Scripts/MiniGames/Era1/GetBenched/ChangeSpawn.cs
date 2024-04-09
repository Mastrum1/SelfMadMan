using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawn : MonoBehaviour
{
    private float _mTimer = 0; 
    [SerializeField] private BoxCollider2D _mSpawnBounds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Buttons"))
        {
            gameObject.transform.position = new Vector3(
                   Random.Range(_mSpawnBounds.bounds.min.x, _mSpawnBounds.bounds.max.x),
                   Random.Range(_mSpawnBounds.bounds.min.y, _mSpawnBounds.bounds.max.y),
                   gameObject.transform.position.z
                   );
            _mTimer = 0;
        }
    }
    private void Update()
    {
        _mTimer += Time.deltaTime;
        if(_mTimer >= 1f)
        {
            TapWithTimer script = gameObject.GetComponent<TapWithTimer>();
            script.Torus.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            script.Number.enabled = true;
            this.enabled = false;
            script.StopTorus = false;
        }
    }
}
