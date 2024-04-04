using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action OnClick;
   void OnPanelClicked()
    {
        OnClick?.Invoke();
    }
}
