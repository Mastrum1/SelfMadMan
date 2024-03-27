using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AutoSceneDeleter : MonoBehaviour
{
    [Scene] public string CurrentScene;
    [SerializeField] private float _delay;

    void Start()
    {
        StartCoroutine(DeleteAfter());
    }

    IEnumerator DeleteAfter()
    {
        yield return new WaitForSeconds(_delay);
        mySceneManager.instance.UnloadPreciseScene(CurrentScene);
    }

}
