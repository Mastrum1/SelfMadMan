using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Road : MonoBehaviour
{
    
    [SerializeField] private GameObject _dirtyAd;
    [SerializeField] private GameObject _adHolder;
    [SerializeField] private GameObject _adSpawnPoint;
    
    public List<OnCollide> Ads
    {
        get => _ads;
        set => _ads = value;
    }
    private List<OnCollide> _ads = new List<OnCollide>();
    
    
    void Awake()
    {
        SpawnDirtyAd();
    }

    void SpawnDirtyAd()
    {
        if (_dirtyAd == null)
        {
            Debug.LogError("No dirty ads assigned to the array.");
            return;
        }
            
        var rand = Random.Range(-1, 1);
        int dir = rand >= 0 ? 1 : -1;

        var ad = Instantiate(_dirtyAd, new Vector3(_adSpawnPoint.transform.position.x * dir,transform.position.y,-1), Quaternion.identity);
        var adScript = ad.GetComponent<DirtyAdd>();
        adScript.Dir = -dir;
        adScript.TimeTilMove = Random.Range(0.1f, 3f);
        ad.transform.SetParent(_adHolder.transform); 
        _ads.Add(ad.GetComponent<OnCollide>());
        Debug.Log(_ads.Count);
    }
}
