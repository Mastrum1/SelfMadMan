using Coffee.UISoftMask;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpObtained : MonoBehaviour
{

    private enum Type
    {
        COINS, MINIGAME, DECORATION
    }
    [SerializeField] private TMP_Text _mObtainedObject;
    [SerializeField] private TMP_Text _mTypeOfObtainable;
    [SerializeField] private TMP_Text _mUnlocked;

    [SerializeField] private Color[] _mUnlockedColor;
    [SerializeField] private Color[] _mTextColor;
    [SerializeField] private Sprite[] _mTypeOfPopup;


    [SerializeField] private Image _PopUpImage;
    [SerializeField] private Image _ObtainImage;
    [SerializeField] private Image _mMinigameImage;

    [SerializeField] private SoftMask _mSoftMaskMinigame;

    [SerializeField] private Animator _popupObtainedAnimator;
    private void OnEnable()
    {
        _mMinigameImage.enabled = false;
        _mSoftMaskMinigame.enabled = false;
        _popupObtainedAnimator.SetBool("Open", true);
    }
    public void OnObtainPopup(ItemsSO item)
    {
        switch (item.Type)
        {
            case ItemsSO.TYPE.COINS:
                _mTypeOfObtainable.text = "YOU WIN";
                _mUnlocked.text = "MADCOINS!";
                _ObtainImage.overrideSprite = item.Icon;
                _PopUpImage.overrideSprite = _mTypeOfPopup[(int)Type.COINS];
                CoinsSO temp = item as CoinsSO;
                _mObtainedObject.text = temp.Amount + " MADCOINS";
                _mObtainedObject.color = _mTextColor[(int)Type.COINS];
                _mUnlocked.color = _mUnlockedColor[(int)Type.COINS];
                break;

            case ItemsSO.TYPE.MINIGAME:
                _mMinigameImage.enabled = true;
                _mSoftMaskMinigame.enabled = true;
                _ObtainImage.overrideSprite = item.Icon;
                _PopUpImage.overrideSprite = _mTypeOfPopup[(int)Type.MINIGAME];
                _mTypeOfObtainable.text = "MINI-GAME";
                _mUnlocked.text = "UNLOCKED!";
                _mObtainedObject.text = item.ItemName;
                _mObtainedObject.color = _mTextColor[(int)Type.MINIGAME];
                _mUnlocked.color = _mUnlockedColor[(int)Type.MINIGAME];
                break;
            case ItemsSO.TYPE.FURNITURE:
                _mMinigameImage.enabled = true;
                _mSoftMaskMinigame.enabled = true;
                _ObtainImage.overrideSprite = item.Icon;
                _PopUpImage.overrideSprite = _mTypeOfPopup[(int)Type.DECORATION];
                _mTypeOfObtainable.text = "FURNITURE";
                _mUnlocked.text = "UNLOCKED!";
                _mObtainedObject.text = item.ItemName;
                _mObtainedObject.color = _mTextColor[(int)Type.DECORATION];
                _mUnlocked.color = _mUnlockedColor[(int)Type.DECORATION];
                break;
        }
    }

    public void ClosePopup()
    {

        _popupObtainedAnimator.SetBool("Open", false);
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        if(TutorialManager.instance.InTutorial) { TutorialManager.instance.StepInit(); }
    }



}
