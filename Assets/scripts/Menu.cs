using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
/*!
	\brief Класс отвечающий за меню игры
*/
public class Menu : MonoBehaviour
{
    ///Менеджер локализациии
    [SerializeField] private LocalizationManager localizationManager;

    ///Поворачиваемый объект
    public GameObject go;
    
    ///Тыква
    public GameObject[] pump;

    ///Кастомные предметы
    public GameObject[] custom1;

    /// Вектор для поворота
    public Vector3 localAxis;

    ///Поле для поворота
    public float deltaAngle;

    ///Скорость поворота
    public float speedRotation;

    ///Скин персонажа
    private int skin = 0;

    ///Счетчик кастомных предметов
    private int customCount1 = 0;

    ///Кастомные предметы 2
    public GameObject[] custom2;

    ///Счетчик кастомных предметов 2
    private int customCount2 = 0;
    
    ///Игрок
    public GameObject player;

    ///Вспомогательное логическое поле
    bool isPlayerInMenu = true;

    ///Языки
    string[] languages = {"ru_RU", "en_US", "ch_CH", "ua_UA"};
    int langCount;

    ///Ползунок уровня громкости музыки
    [SerializeField] private Slider MusicSlider;

    ///Ползунок уровня громкости эффектов
    [SerializeField] private Slider EffectsSlider;

    ///Вкл/Вкл музыка
    [SerializeField] private Toggle MusicToggle;
    
    ///Музыка
    [SerializeField] AudioSource MusicAudioSource;

    void Start() ///Стартовый метод
    {
        if (PlayerPrefs.HasKey("MusicVolume")) MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("EffectsVolume")) EffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        if (PlayerPrefs.HasKey("isMusic")){
            if (PlayerPrefs.GetInt("isMusic") == 1)
            MusicToggle.isOn = true;
            else MusicToggle.isOn = false;
        } 

        Time.timeScale = 1f;
        customCount1 = 0;

         if (PlayerPrefs.HasKey("Skin")) skin = PlayerPrefs.GetInt("Skin");
        pump[skin].SetActive(true);
        
        if (PlayerPrefs.HasKey("Custom1")) customCount1 = PlayerPrefs.GetInt("Custom1");
        custom1[customCount1].SetActive(true);

        customCount2 = 0;
        if (PlayerPrefs.HasKey("Custom2")) customCount2 = PlayerPrefs.GetInt("Custom2");
        custom2[customCount2].SetActive(true);

        langCount = System.Array.IndexOf(languages, PlayerPrefs.GetString("Language"));
        localizationManager.CurrentLanguage = languages[langCount];
    }


    void Update() ///Метод обновления кадров
    {
        if (!MusicToggle.isOn) MusicAudioSource.volume =0;
        else MusicAudioSource.volume = MusicSlider.value;

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 10f);
    }

    public void newGame() ///Начало игры
    {
        int isMusic;
        if (MusicToggle.isOn) isMusic = 1;
        else isMusic = 0;
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsSlider.value);
        PlayerPrefs.SetInt("isMusic",isMusic);
        SceneManager.LoadScene(1);
    }


    public void Settings() ///Настройки
    {
        if (isPlayerInMenu)
        {
            DeltaRotate(go, localAxis, deltaAngle, speedRotation);
            player.transform.position = new Vector3(1602.726f, 761.636f, -192.4529f);
            player.transform.rotation = Quaternion.Euler(0,200,0); //0, 0, -57.837f
            isPlayerInMenu = false;
        }
        else
        {
            DeltaRotate(go, localAxis, -deltaAngle, speedRotation);
            player.transform.position = new Vector3(1597.794f, 762.51f, -185.218f);
            player.transform.rotation = Quaternion.Euler(0,-220,0); //= Quaternion.Euler(0, 0, 138.334f);
            isPlayerInMenu = true;
        }
            int isMusic;
            if (MusicToggle.isOn) isMusic = 1;
            else isMusic = 0;
            PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
            PlayerPrefs.SetFloat("EffectsVolume", EffectsSlider.value);
            PlayerPrefs.SetInt("isMusic",isMusic);
    }

    public void Customization() ///Кастомизация
    {
        DeltaRotate(go, localAxis, deltaAngle*2, speedRotation);
        //DeltaRotate(lig, localAxis, deltaAngle, speedRotation);
        if (isPlayerInMenu)
        {
            player.transform.position = new Vector3(1594.716f, 761.63f, -196.027f);
            player.transform.rotation = Quaternion.Euler(0,280,0); //0, 0, -57.837f
            isPlayerInMenu = false;
        }
        else
        {
            player.transform.position = new Vector3(1597.794f, 762.76f, -185.218f);
            player.transform.rotation = Quaternion.Euler(0,-220,0); //= Quaternion.Euler(0, 0, 138.334f);
            isPlayerInMenu = true;
        }
    }


    void DeltaRotate(GameObject go, Vector3 localAxis, float deltaAngle, float speedRotation) ///Метод поворота камеры
    {
        StartCoroutine(c_RotateDelta(go, localAxis, deltaAngle, speedRotation));
    }

    IEnumerator c_RotateDelta(GameObject go, Vector3 localAxis, float deltaAngle, float speedRotation) ///Метод поворота камеры
    {
        float total = 0.0f;

        if (deltaAngle < 0.0f)
        {
            localAxis = -localAxis;
            deltaAngle = -deltaAngle;
        }

        while (true)
        {
            float d = speedRotation * Time.deltaTime;

            total += d;

            if (total > deltaAngle)
            {
                go.transform.Rotate(localAxis, deltaAngle - (total - d));

                yield break;
            }
            else
                go.transform.Rotate(localAxis, d);

            yield return null;
        }
    }


    public void firstSkin() ///Изменение скина
    {
        pump[skin].SetActive(false);
        if (skin == 0) skin = pump.Length - 1;
        else skin--;
        pump[skin].SetActive(true);
        PlayerPrefs.SetInt("Skin", skin);
    }

    public void secondSkin() ///Изменение скина
    {
        pump[skin].SetActive(false);
        if (skin == pump.Length -1) skin = 0;
        else skin++;
        pump[skin].SetActive(true);
        PlayerPrefs.SetInt("Skin", skin);
    }


    public void firstCustom1() ///Изменение кастомной вещи
    {
        custom1[customCount1].SetActive(false);
        if (customCount1 == 0) customCount1 = custom1.Length - 1;
        else customCount1--;
        custom1[customCount1].SetActive(true);
        PlayerPrefs.SetInt("Custom1", customCount1);

    }
    public void secondCustom1() ///Изменение кастомной вещи
    {
        custom1[customCount1].SetActive(false);
        if (customCount1 == custom1.Length - 1) customCount1 = 0;
        else customCount1++;
        custom1[customCount1].SetActive(true);
        PlayerPrefs.SetInt("Custom1", customCount1);
    }

    public void firstCustom2() ///Изменение кастомной вещи 2
    {
        custom2[customCount2].SetActive(false);
        if (customCount2 == 0) customCount2 = custom2.Length - 1;
        else customCount2--;
        custom2[customCount2].SetActive(true);
        PlayerPrefs.SetInt("Custom2", customCount2);

    }
    public void secondCustom2() ///Изменение кастомной вещи 2
    {
        custom2[customCount2].SetActive(false);
        if (customCount2 == custom2.Length - 1) customCount2 = 0;
        else customCount2++;
        custom2[customCount2].SetActive(true);
        PlayerPrefs.SetInt("Custom2", customCount2);
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

    public void Exit() ///Выход
    {
        Application.Quit();
    }
}
