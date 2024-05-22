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

        Sprite tempSprite;
        var objColliderPoints = new Vector2[0];
        var tempChadColliderPoints = new Vector2[0];
        Vector3 tempScale = Vector3.zero;

        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                Random = UnityEngine.Random.Range(9, _mObjfirstFaster.Count);

                _mObjfirstFaster[Random].Real = true;


                objColliderPoints = _mObjfirstFaster[Random].Collider.points;

                tempChadColliderPoints = _mObjfirstFaster[_firstChadPos].Collider.points;

                tempSprite = _mObjfirstFaster[Random].SpriteRenderer.sprite;
                _mObjfirstFaster[Random].SpriteRenderer.sprite = _mObjfirstFaster[_firstChadPos].SpriteRenderer.sprite;
                _mObjfirstFaster[_firstChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjfirstFaster[Random].transform.localScale;
                _mObjfirstFaster[Random].transform.localScale = _mObjfirstFaster[_firstChadPos].transform.localScale;
                _mObjfirstFaster[_firstChadPos].transform.localScale = tempScale;

                _mObjfirstFaster[Random].Collider.points = tempChadColliderPoints;

                _mObjfirstFaster[_firstChadPos].Collider.points = objColliderPoints;

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

                objColliderPoints = _mObjsecondFaster[Random].Collider.points;

                tempChadColliderPoints = _mObjsecondFaster[_firstChadPos].Collider.points;

                tempSprite = _mObjsecondFaster[Random].SpriteRenderer.sprite;
                _mObjsecondFaster[Random].SpriteRenderer.sprite = _mObjsecondFaster[_secondChadPos].SpriteRenderer.sprite;
                _mObjsecondFaster[_secondChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjsecondFaster[Random].transform.localScale;
                _mObjsecondFaster[Random].transform.localScale = _mObjsecondFaster[_secondChadPos].transform.localScale;
                _mObjsecondFaster[_secondChadPos].transform.localScale = tempScale;

                _mObjsecondFaster[Random].Collider.points = tempChadColliderPoints;

                _mObjsecondFaster[_firstChadPos].Collider.points = objColliderPoints;

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


                objColliderPoints = _mObjOthersFaster[Random].Collider.points;

                tempChadColliderPoints = _mObjOthersFaster[_firstChadPos].Collider.points;

                tempSprite = _mObjOthersFaster[Random].SpriteRenderer.sprite;
                _mObjOthersFaster[Random].SpriteRenderer.sprite = _mObjOthersFaster[_OthersChadPos].SpriteRenderer.sprite;
                _mObjOthersFaster[_OthersChadPos].SpriteRenderer.sprite = tempSprite;



                tempScale = _mObjOthersFaster[Random].transform.localScale;
                _mObjOthersFaster[Random].transform.localScale = _mObjOthersFaster[_OthersChadPos].transform.localScale;
                _mObjOthersFaster[_OthersChadPos].transform.localScale = tempScale;

                _mObjOthersFaster[Random].Collider.points = tempChadColliderPoints;

                _mObjOthersFaster[_firstChadPos].Collider.points = objColliderPoints;

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
