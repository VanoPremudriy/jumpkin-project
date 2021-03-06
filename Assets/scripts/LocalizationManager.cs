using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/*!
	\brief Класс отвечающий за локализацию игры
*/

public class LocalizationManager : MonoBehaviour
{
    private string currentLanguage;
    private Dictionary<string, string> localizedText;
    public static bool isReady = false;

    ///Изменение текста
    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    void Awake() ///Метод смены языка
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
            else if (Application.systemLanguage == SystemLanguage.Chinese)
            {
                PlayerPrefs.SetString("Language", "ch_CH");
            }
            else if (Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "ua_UA");
            }
        }
        currentLanguage = PlayerPrefs.GetString("Language");

        LoadLocalizedText(currentLanguage);
    }

    public void LoadLocalizedText(string langName) ///Метод смены языка
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";

        string dataAsJson;

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }

            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        PlayerPrefs.SetString("Language", langName);
        isReady = true;
        OnLanguageChanged?.Invoke();
    }

    public string GetLocalizedValue(string key) ///Перевод конкретного текста
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }

    public string CurrentLanguage
    {
        get
        {
            return currentLanguage;
        }
        set
        {

            PlayerPrefs.SetString("Language", value);
            currentLanguage = PlayerPrefs.GetString("Language");
            LoadLocalizedText(currentLanguage);
        }
    }
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }
}