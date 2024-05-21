using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private RizzHerInteractableManager _RizzHerInteractable;
    float _fasterLevel;
    // Start is called before the first frame update

    private void OnEnable()
    {
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                _fasterLevel = GameManager.instance.FasterLevel;
                break;
            default:
                _fasterLevel = (GameManager.instance.FasterLevel / 1.75f);
                break;
        }
    }

    public void FadeChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ObstacleScript obj = transform.GetChild(i).GetComponent<ObstacleScript>();
            obj.FadeOut(obj, _RizzHerInteractable);
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float counter = 0;
        while (counter < 1f)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(0, 5.68f, 0);
        StopCoroutine(FadeOutCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime * _fasterLevel);
    }
}
