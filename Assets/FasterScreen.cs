using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FasterScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private VideoPlayer m_Bike;
    [SerializeField] private GameObject m_Prefab1;
    [SerializeField] private GameObject m_Prefab2;

    private void Awake()
    {
        m_Prefab.SetActive(false);
        m_Prefab1.SetActive(false);
        m_Prefab2.SetActive(false);
        switch (GameManager.instance.Era)
        {
            case 0:

                StartCoroutine(ChadAnimation(m_Prefab));
                break;
            case 1:
                StartCoroutine(ChadAnimation(m_Prefab1));
                break;
            case 2:
                StartCoroutine(ChadAnimation(m_Prefab2));
                break;
        }

    }
    IEnumerator ChadAnimation(GameObject go)
    {
        if (go == m_Prefab)
        {
            StartCoroutine(PrepareVideoCoroutine());
        }
        else
            go.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        mySceneManager.instance.UnloadPreciseScene(mySceneManager.instance.FasterScreen);

    }

    IEnumerator PrepareVideoCoroutine()
    {
        while (!m_Bike.isPrepared)
        {
            m_Bike.Prepare();
            yield return new WaitForEndOfFrame();
        }
        m_Bike.Play();
        m_Prefab.SetActive(true );
    }
}
