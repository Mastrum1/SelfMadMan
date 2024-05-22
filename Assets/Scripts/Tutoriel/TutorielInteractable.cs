using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielInteractable : MonoBehaviour
{

    [SerializeField] private int _StepNumber;
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private RectTransform _RectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        TutorialManager.instance.StepAddDataButton(_Canvas, _RectTransform, _StepNumber);
        _Canvas.sortingOrder = 1;
    }

    // Update is called once per frame
    public void OnStepComplete()
    {
        if (TutorialManager.instance.InTutorial)
            TutorialManager.instance.NextStep();
    }
}
