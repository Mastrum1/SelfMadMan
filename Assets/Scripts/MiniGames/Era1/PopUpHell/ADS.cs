using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] private GameObject _mCloseButton;

    void Start()
    {
        int i = Random.Range(0, _mSpawnPoints.Count);
        _mCloseButton.transform.position = _mSpawnPoints[i].transform.position;
    }

    void Update()
    {
        
    }

    public void OnDelete()
    {
        this.transform.gameObject.SetActive(false);
    }
}
