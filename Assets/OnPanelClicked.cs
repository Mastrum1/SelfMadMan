using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPanelClicked : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (TutorialManager.instance.FinalStepBool)
            TutorialManager.instance.EndTutorial();
    }
}
