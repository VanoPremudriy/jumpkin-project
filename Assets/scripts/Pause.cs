using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*!
	\brief Класс отвечающий за паузу игры
*/
public class Pause : MonoBehaviour
{
    ///Менеджер локализаци
    [SerializeField] private LocalizationManager localizationManager;

    ///Вспомогательная логическая переменная
    public static bool isGamePause = false;

    ///Интерфейс паузы
    public GameObject pauseMenuUI;

    ///Интерфейс игры
    public GameObject gameInterface;

    ///Тыква
    public GameObject[] pump;

    ///Кастомный предмет
    public GameObject[] Custom1;

    ///Кастомный предмет 2
    public GameObject[] Custom2;

    ///Музыка
    [SerializeField] private AudioSource MusicAudioSource;

    ///Эффекты
    [SerializeField] private AudioSource[] EffectsAudioSource;
    void Start() ///Стартовый метод
    {
        MusicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        pump[PlayerPrefs.GetInt("Skin")].SetActive(true);
        Custom1[PlayerPrefs.GetInt("Custom1")].SetActive(true);
        Custom2[PlayerPrefs.GetInt("Custom2")].SetActive(true);
        if (PlayerPrefs.GetInt("isMusic") == 0) MusicAudioSource.volume =0;
        else MusicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        resume();
        GameObject.FindGameObjectWithTag("EndGameMenu").SetActive(false);
    }

    void Update() ///Метод обновления кадров
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
        localizationManager.CurrentLanguage = PlayerPrefs.GetString("Language");
    }

    void resume() ///Выход из паузы
    {
        pauseMenuUI.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
        isGamePause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void pause() ///Пауза
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        gameInterface.SetActive(false);
        Time.timeScale = 0f;
        isGamePause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void loadMenu() ///Выход из паузы
    {
        pauseMenuUI.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
        isGamePause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void quitGame() ///Вызод из игры
    {
        Application.LoadLevel(0);
    }
}
