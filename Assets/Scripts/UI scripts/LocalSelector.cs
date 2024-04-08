using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalSelector : MonoBehaviour
{
    static LocalSelector _mInstance;

    private bool active = false;
    
    public static LocalSelector Instance { get =>_mInstance; } 

    private void Start()
    {
        if (_mInstance == null)
        {
            _mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }

    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(Setlocate(localeID));
        Debug.Log(LocalizationSettings.AvailableLocales.Locales[localeID]);
    }

    IEnumerator Setlocate(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        active = false;
    }
}
