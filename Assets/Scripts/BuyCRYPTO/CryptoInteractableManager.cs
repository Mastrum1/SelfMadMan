using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CryptoInteractableManager : InteractableManager
{
    // Start is called before the first frame update
    [SerializeField] private PostIt[] postIts;
    [SerializeField] private Transform[] placeHolders;

    public event Action<Sprite> OnPostItClicked;

    private void Awake()
    {

        postIts = postIts.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).ToArray();
        for(int i = 0; i < placeHolders.Length; i++)
        {
            postIts[i].transform.position = placeHolders[i].transform.position;
            Debug.Log("Subscribing to event for " + postIts[i].name);
            postIts[i].PostItClicked += DetectClick;
        }
    }

    private void DetectClick(PostIt postIt)
    {
        Debug.Log("Post-it pressed: " + postIt.name + ", corresponding graph: " + postIt.CorrespondingGraph.name);
    }

    private void OnDestroy()
    {
        foreach(PostIt postIt in postIts)
        {
            postIt.PostItClicked -= DetectClick;
        }
    }
}
