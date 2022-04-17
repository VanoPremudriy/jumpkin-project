using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*!
	\brief Класс отвечающий за локализацию игры
*/

public class LocalizedText : MonoBehaviour
{
    ///Ключ текста
    [SerializeField] private string key;

    ///Менеджер локализации
    private LocalizationManager localizationManager;

    ///Текст
    private Text text; 

    void Awake()///Запуск менеджера
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        localizationManager.OnLanguageChanged += UpdateText;
    }

    void Start() /// Стартовый метод
    {
        UpdateText();
    }

    private void OnDestroy() ///Метод смены текста
    {
        localizationManager.OnLanguageChanged -= UpdateText;
    }

    virtual protected void UpdateText() ///Метод обновления текста
    {
        if (gameObject == null) return;

        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        text.text = localizationManager.GetLocalizedValue(key);
    }
}