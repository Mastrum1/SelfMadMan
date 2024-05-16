using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielInteractable : MonoBehaviour
{

    [SerializeField] private int _StepNumber;
    [SerializeField] private Canvas _Canvas;
    // Start is called before the first frame update
    void Awake()
    {
        TutorialManager.instance.StepAddCanvas(_Canvas, _StepNumber);
        _Canvas.sortingOrder = 1;
    }

    // Update is called once per frame
    public void OnStepComplete()
    {
        if (TutorialManager.instance.InTutorial)
            TutorialManager.instance.NextStep();
    }
}
