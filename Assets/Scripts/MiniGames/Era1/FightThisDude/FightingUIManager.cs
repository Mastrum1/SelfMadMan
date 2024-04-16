using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUIManager : MonoBehaviour
{
    [SerializeField] public CompletionBar Bar;
    [SerializeField] public Sprite[] FightSprites;
    [SerializeField] private Image _fightImage;

    private int _index = 0;
    public void OnFightImageChange()
    {
        _index++;
        _fightImage.overrideSprite = FightSprites[_index % FightSprites.Length];
    }
}
