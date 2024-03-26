using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _mLevel;

    private int _mXp;

    private int _mMoney;

    [SerializeField] private ScriptableObject[] _mInventory;

    [SerializeField] private ScriptableObject[] _mItemLocked;

    [SerializeField] private ScriptableObject[] _mQuestActive;

    [SerializeField] private ScriptableObject[] _mQuestLocked;

    [SerializeField] private ScriptableObject[] _mQuestUnlocked;
}
