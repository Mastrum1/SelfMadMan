using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cardashianCard : MonoBehaviour
{

    [SerializeField] private bool _mIsDifferent = false;

    [SerializeField] GameObject _mCanvas;
    private string _mList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField] private List<TextMeshProUGUI> _mTexts;
    [SerializeField] private Image _mLogo;

    [SerializeField] private int _mNbErreur = 3;

    public bool Different
    {
        get { return _mIsDifferent; }
        set { _mIsDifferent = value; }
    }

    public event Action<bool> OnGameEnd;

    public void ChangeText()
    {
        int Random = UnityEngine.Random.Range(0, _mCanvas.transform.childCount);
        if (Random != 4)
        {
            for (int i = 0; i < _mNbErreur; i++)
            {
                int RandomCharacter;
                do
                {
                    RandomCharacter = UnityEngine.Random.Range(0, _mTexts[Random].text.Length);
                }
                while (_mTexts[Random].text[RandomCharacter] == ' ');
                Debug.Log(_mTexts[Random].text);

                int RandomCharToAdd = UnityEngine.Random.Range(0, _mList.Length);

                char CharToAdd = _mList[RandomCharToAdd];

                if (_mTexts[Random].text[RandomCharacter] == char.ToLower(_mTexts[Random].text[RandomCharacter]))
                {
                    CharToAdd = char.ToLower(_mList[RandomCharToAdd]);
                }

                string resultString;

                if (RandomCharacter == 0)
                {
                    resultString = CharToAdd + _mTexts[Random].text.Substring(RandomCharacter + 1);

                }
                else if (RandomCharacter == _mTexts[Random].text.Length - 1)
                {
                    resultString = _mTexts[Random].text.Substring(RandomCharacter) + CharToAdd;
                }
                else
                {
                    resultString = _mTexts[Random].text.Substring(0, RandomCharacter) + CharToAdd + _mTexts[Random].text.Substring(RandomCharacter + 1);
                }

                _mTexts[Random].text = resultString;

            }
        }
        else
        {
            Debug.Log(_mLogo.sprite.name);
        }

    }

    public void OnTap()
    {
        if (_mIsDifferent)
        {
            OnGameEnd?.Invoke(true);
        }
        else
        {
            OnGameEnd?.Invoke(false);
        }
    }
}
