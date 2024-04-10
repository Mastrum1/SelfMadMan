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
    [SerializeField] public PostIt[] PostIts { get => postIts; }
    [SerializeField] private Transform[] placeHolders;

    public event Action<PostIt> OnPostItClicked;

    private void Awake()
    {

        postIts = postIts.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).ToArray();
        for(int i = 0; i < placeHolders.Length; i++)
        {
            postIts[i].transform.position = placeHolders[i].transform.position;
            postIts[i].PostItClicked += DetectClick;
        }
    }

    private void DetectClick(PostIt postIt)
    {
            OnPostItClicked.Invoke(postIt);
    }

    private void OnDestroy()
    {
        foreach(PostIt postIt in postIts)
        {
            postIt.PostItClicked -= DetectClick;
        }
    }
}
