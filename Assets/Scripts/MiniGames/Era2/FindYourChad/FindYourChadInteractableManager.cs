using System;
using System.Collections.Generic;
using UnityEngine;

public class FindYourChadInteractableManager : MonoBehaviour
{

    public event Action<bool> GameEnd;
    [SerializeField] private int NbObjToSpawn = 2;

    [SerializeField] private List<FindYourChadObj> _mObjfirstFaster;
    [SerializeField] private int _firstChadPos = 11;

    [SerializeField] private List<FindYourChadObj> _mObjsecondFaster;
    [SerializeField] private int _secondChadPos = 11;

    [SerializeField] private List<FindYourChadObj> _mObjOthersFaster;
    [SerializeField] private int _OthersChadPos = 14;

    private void Start()
    {
        int Random = 0;
        Vector2 objColliderOffset = Vector2.zero;
        Vector2 objColliderSize = Vector2.zero;
        Vector2 tempChadColliderOffset = Vector2.zero;
        Vector2 tempChadColliderSize = Vector2.zero;
        Sprite tempSprite;
        Vector3 tempScale = Vector3.zero;

        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                Random = UnityEngine.Random.Range(9, _mObjfirstFaster.Count);

                _mObjfirstFaster[Random].Real = true;

                objColliderOffset = _mObjfirstFaster[Random].Collider.offset;
                objColliderSize = _mObjfirstFaster[_firstChadPos].Collider.size;

                tempChadColliderOffset = _mObjfirstFaster[_firstChadPos].Collider.offset;
                tempChadColliderSize = _mObjfirstFaster[_firstChadPos].Collider.size;

                tempSprite = _mObjfirstFaster[Random].SpriteRenderer.sprite;
                _mObjfirstFaster[Random].SpriteRenderer.sprite = _mObjfirstFaster[_firstChadPos].SpriteRenderer.sprite;
                _mObjfirstFaster[_firstChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjfirstFaster[Random].transform.localScale;
                _mObjfirstFaster[Random].transform.localScale = _mObjfirstFaster[_firstChadPos].transform.localScale;
                _mObjfirstFaster[_firstChadPos].transform.localScale = tempScale;

                _mObjfirstFaster[Random].Collider.offset = tempChadColliderOffset;
                _mObjfirstFaster[Random].Collider.size = tempChadColliderSize;

                _mObjfirstFaster[_firstChadPos].Collider.offset = objColliderOffset;
                _mObjfirstFaster[_firstChadPos].Collider.size = objColliderSize;

                for (int id = 0; id < _mObjfirstFaster.Count; id++)
                {
                    if (id == Random)
                    {
                        _mObjfirstFaster[id].OnGameEnd += EndGame;
                        _mObjfirstFaster[id].gameObject.SetActive(true);

                        continue;
                    }
                    else
                    {
                        _mObjfirstFaster[id].Real = false;
                        _mObjfirstFaster[id].OnGameEnd += EndGame;
                        _mObjfirstFaster[id].gameObject.SetActive(true);
                    }

                }

                break;
            case 2:
                Random = UnityEngine.Random.Range(5, _mObjsecondFaster.Count);

                _mObjsecondFaster[Random].Real = true;

                objColliderOffset = _mObjsecondFaster[Random].Collider.offset;
                objColliderSize = _mObjsecondFaster[_secondChadPos].Collider.size;

                tempChadColliderOffset = _mObjsecondFaster[_secondChadPos].Collider.offset;
                tempChadColliderSize = _mObjsecondFaster[_secondChadPos].Collider.size;

                tempSprite = _mObjsecondFaster[Random].SpriteRenderer.sprite;
                _mObjsecondFaster[Random].SpriteRenderer.sprite = _mObjsecondFaster[_secondChadPos].SpriteRenderer.sprite;
                _mObjsecondFaster[_secondChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjsecondFaster[Random].transform.localScale;
                _mObjsecondFaster[Random].transform.localScale = _mObjsecondFaster[_secondChadPos].transform.localScale;
                _mObjsecondFaster[_secondChadPos].transform.localScale = tempScale;

                _mObjsecondFaster[Random].Collider.offset = tempChadColliderOffset;
                _mObjsecondFaster[Random].Collider.size = tempChadColliderSize;

                _mObjsecondFaster[_secondChadPos].Collider.offset = objColliderOffset;
                _mObjsecondFaster[_secondChadPos].Collider.size = objColliderSize;

                for (int id = 0; id < _mObjsecondFaster.Count; id++)
                {
                    if (id == Random)
                    {
                        _mObjsecondFaster[id].OnGameEnd += EndGame;
                        _mObjsecondFaster[id].gameObject.SetActive(true);

                        continue;
                    }
                    else
                    {
                        _mObjsecondFaster[id].Real = false;
                        _mObjsecondFaster[id].OnGameEnd += EndGame;
                        _mObjsecondFaster[id].gameObject.SetActive(true);
                    }

                }

                break;
            default:
                Random = UnityEngine.Random.Range(5, _mObjOthersFaster.Count);

                _mObjOthersFaster[Random].Real = true;

                objColliderOffset = _mObjOthersFaster[Random].Collider.offset;
                objColliderSize = _mObjOthersFaster[_OthersChadPos].Collider.size;

                tempChadColliderOffset = _mObjOthersFaster[_OthersChadPos].Collider.offset;
                tempChadColliderSize = _mObjOthersFaster[_OthersChadPos].Collider.size;

                tempSprite = _mObjOthersFaster[Random].SpriteRenderer.sprite;
                _mObjOthersFaster[Random].SpriteRenderer.sprite = _mObjOthersFaster[_OthersChadPos].SpriteRenderer.sprite;
                _mObjOthersFaster[_OthersChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjOthersFaster[Random].transform.localScale;
                _mObjOthersFaster[Random].transform.localScale = _mObjOthersFaster[_OthersChadPos].transform.localScale;
                _mObjOthersFaster[_OthersChadPos].transform.localScale = tempScale;

                _mObjOthersFaster[Random].Collider.offset = tempChadColliderOffset;
                _mObjOthersFaster[Random].Collider.size = tempChadColliderSize;

                _mObjOthersFaster[_OthersChadPos].Collider.offset = objColliderOffset;
                _mObjOthersFaster[_OthersChadPos].Collider.size = objColliderSize;

                for (int id = 0; id < _mObjOthersFaster.Count; id++)
                {
                    if (id == Random)
                    {
                        _mObjOthersFaster[id].OnGameEnd += EndGame;
                        _mObjOthersFaster[id].gameObject.SetActive(true);

                        continue;
                    }
                    else
                    {
                        _mObjOthersFaster[id].Real = false;
                        _mObjOthersFaster[id].OnGameEnd += EndGame;
                        _mObjOthersFaster[id].gameObject.SetActive(true);
                    }

                }

                break;
        }

    }

    private void OnDisable()
    {
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                for (int id = 0; id < _mObjfirstFaster.Count; id++)
                {
                    _mObjOthersFaster[id].OnGameEnd -= EndGame;
                }
                break;
            case 2:
                for (int id = 0; id < _mObjsecondFaster.Count; id++)
                {
                    _mObjOthersFaster[id].OnGameEnd -= EndGame;
                }
                break;
            default:
                for (int id = 0; id < _mObjOthersFaster.Count; id++)
                {
                    _mObjOthersFaster[id].OnGameEnd -= EndGame;
                }
                break;
        }
    }



    private void EndGame(bool win)
    {
        GameEnd?.Invoke(win);
    }
}
