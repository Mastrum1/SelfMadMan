using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    public event Action<PostIt> PostItClicked;
    [SerializeField] public Sprite CorrespondingGraph;
    // Start is called before the first frame update

    public void OnPostItClicked()
    {
        PostItClicked.Invoke(this);
    }
}
