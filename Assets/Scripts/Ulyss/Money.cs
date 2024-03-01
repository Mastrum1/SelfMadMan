using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Public
    public Text MoneyAmount;

    // Private
    private int _mCurrentMoney = 0;
    private string _mMoneyPlayerPrefsKey = "PlayerMoney";  // Key for PlayerPrefs
    private string _mEncryptionKey = "SelfMadManAbsoluteEncryptionKeyMyBoy"; // Key code for encryption

    void Start()
    {
        LoadMoney(); // Load PlayerPrefs
    }

    void Update()
    {
        SubtractMoney(10);
        MoneyAmount.text = "Money: " + _mCurrentMoney;

        if (_mCurrentMoney < 0) { _mCurrentMoney = 0; }
    }

    public void AddMoney(int MoneyToAdd)
    {
        _mCurrentMoney += MoneyToAdd;
        SaveMoney();
    }

    public void SubtractMoney(int MoneyToRemove)
    {
        if (_mCurrentMoney - MoneyToRemove < 0)
        {
            Debug.Log("No Money");
        }

        else
        {
            _mCurrentMoney -= MoneyToRemove;
            SaveMoney();
        }
    }

    public int GetMoney()
    {
        return _mCurrentMoney;
    }

    public int SetMoney(int MoneyToBe)
    {
        _mCurrentMoney = MoneyToBe;
        SaveMoney(); // Saves in PlayerPrefs after alteration
        return _mCurrentMoney;
    }

    private void SaveMoney()
    {
        string dataToSave = _mCurrentMoney.ToString();

        string encryptedData = EncryptData(dataToSave, _mEncryptionKey);

        PlayerPrefs.SetString(_mMoneyPlayerPrefsKey, encryptedData);
        PlayerPrefs.Save();
    }

    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey(_mMoneyPlayerPrefsKey))
        {
            string encryptedData = PlayerPrefs.GetString(_mMoneyPlayerPrefsKey);

            string decryptedData = DecryptData(encryptedData, _mEncryptionKey);

            if (int.TryParse(decryptedData, out _mCurrentMoney))
            {
                Debug.Log("Money loaded: " + _mCurrentMoney);
            }
            else
            {
                Debug.LogError("Failed to parse money value from decrypted data.");
            }
        }
    }

    private string EncryptData(string data, string key)
    {
        // XOR encryption with a key
        char[] dataChars = data.ToCharArray();
        char[] keyChars = key.ToCharArray();

        for (int i = 0; i < dataChars.Length; i++)
        {
            dataChars[i] = (char)(dataChars[i] ^ keyChars[i % keyChars.Length]);
        }

        return new string(dataChars);
    }

    private string DecryptData(string data, string key)
    {
        // XOR decryption is the same as encryption
        return EncryptData(data, key);
    }
}