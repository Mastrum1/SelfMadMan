using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField] public UnityEvent<Vector3> _mOnDragAndDrop;
}
