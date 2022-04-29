using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public GameObject gameSettingsUI;

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

    ///Языки
    string[] languages = {"ru_RU", "en_US", "ch_CH", "ua_UA"};
    int langCount;

    ///Ползунок уровня громкости музыки
    [SerializeField] private Slider MusicSlider;

    ///Ползунок уровня громкости эффектов
    [SerializeField] private Slider EffectsSlider;

    ///Вкл/Вкл музыка
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject endGameMenu;
    void Start() ///Стартовый метод
    {
         for (int i=0; i < pump.Length; i ++){
            pump[i].SetActive(false);
        }
         for (int i=0; i < Custom1.Length; i ++){
            Custom1[i].SetActive(false);
        }
         for (int i=0; i < Custom2.Length; i ++){
            Custom2[i].SetActive(false);
        }

        MusicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        pump[PlayerPrefs.GetInt("Skin")].SetActive(true);
        Custom1[PlayerPrefs.GetInt("Custom1")].SetActive(true);
        Custom2[PlayerPrefs.GetInt("Custom2")].SetActive(true);
        if (PlayerPrefs.HasKey("MusicVolume")) MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("EffectsVolume")) EffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        if (PlayerPrefs.GetInt("isMusic") == 1)
        MusicToggle.isOn = true;
        else MusicToggle.isOn = false;
        for (int i=0; i < EffectsAudioSource.Length;i++){
            EffectsAudioSource[i].volume = PlayerPrefs.GetFloat("EffectsVolume");
        }
        resume();
        endGameMenu.SetActive(false);
    }

    void Update() ///Метод обновления кадров
    {

        if (!MusicToggle.isOn) MusicAudioSource.volume =0;
        else MusicAudioSource.volume = MusicSlider.value;

        if (Input.GetKeyDown(KeyCode.Escape) && !gameSettingsUI.activeSelf && !endGameMenu.activeSelf && !gameOverPanel.activeSelf)
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


    public void loadSettings(){
        pauseMenuUI.SetActive(false);
        gameSettingsUI.SetActive(true);
    }

    public void backFromSettings(){
        pauseMenuUI.SetActive(true);
        gameSettingsUI.SetActive(false);
        int isMusic;
        if (MusicToggle.isOn) isMusic = 1;
        else isMusic = 0;
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsSlider.value);
        PlayerPrefs.SetInt("isMusic",isMusic);
         for (int i=0; i < EffectsAudioSource.Length;i++){
            EffectsAudioSource[i].volume = PlayerPrefs.GetFloat("EffectsVolume");
        }
    }

    public void PlayAgain(){
        SceneManager.LoadScene(2);
    }


    public void firstLocalization() ///Изменение языка
    {
        if (langCount == 0) langCount = languages.Length - 1;
        else langCount--;
        localizationManager.CurrentLanguage = languages[langCount];
    }
    public void secondLocalization() ///Изменение языка
    {
        if ( langCount == languages.Length - 1) langCount =0;
        else langCount++;
        localizationManager.CurrentLanguage = languages[langCount];
    }





    public void quitGame() ///Вызод из игры
    {
        Application.LoadLevel(0);
    }
}
