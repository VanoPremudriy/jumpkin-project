using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private PlayerData playerData;
    public GameObject[] pump2;
    public GameObject ch;
    public GameObject go;
    public GameObject lig;
    public GameObject[] pump;
    public GameObject[] custom1;
    public Vector3 localAxis;
    public float deltaAngle;
    public float speedRotation;
    bool isSet = false;
    bool isAct = true;
    //public GameObject pauseMenuUI;
    private int skin = 0;
    private int customCount1 = 0;
    public GameObject[] custom2;
    private int customCount2 = 0;
    public GameObject player;
    bool isPlayerInMenu = true;
    string[] languages = {"ru_RU", "en_US", "ch_CH", "ua_UA"};
    int langCount;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider EffectsSlider;
    [SerializeField] private Toggle MusicToggle;

    [SerializeField] AudioSource MusicAudioSource;

    //public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume")) MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("EffectsVolume")) EffectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        if (PlayerPrefs.HasKey("isMusic")){
            if (PlayerPrefs.GetInt("isMusic") == 1)
            MusicToggle.isOn = true;
            else MusicToggle.isOn = false;
        } 
        //pauseMenuUI.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            if (isAct)
            {
                ch.SetActive(false);
                isAct = false;
            }
            else
            {
                ch.SetActive(true);
                isAct = true;
            }
        }

        if (!MusicToggle.isOn) MusicAudioSource.volume =0;
        else MusicAudioSource.volume = MusicSlider.value;

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 10f);
    }

    public void newGame()
    {
        int isMusic;
        if (MusicToggle.isOn) isMusic = 1;
        else isMusic = 0;
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsSlider.value);
        PlayerPrefs.SetInt("isMusic",isMusic);
        Application.LoadLevel(1);
    }

    public void setSet()
    {
        isSet = true;
    }

    public void Settings()
    {
       // anim.Stop();
        //DeltaRotate(lig, localAxis, deltaAngle, speedRotation);
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

    public void Customization(){
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


    void DeltaRotate(GameObject go, Vector3 localAxis, float deltaAngle, float speedRotation)
    {
        StartCoroutine(c_RotateDelta(go, localAxis, deltaAngle, speedRotation));
    }

    IEnumerator c_RotateDelta(GameObject go, Vector3 localAxis, float deltaAngle, float speedRotation)
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

    public void changeText()
    {
        
    }

    public void firstSkin()
    {
        pump[skin].SetActive(false);
        if (skin == 0) skin = pump.Length - 1;
        else skin--;
        pump[skin].SetActive(true);
        PlayerPrefs.SetInt("Skin", skin);
    }

    public void secondSkin()
    {
        pump[skin].SetActive(false);
        if (skin == pump.Length -1) skin = 0;
        else skin++;
        pump[skin].SetActive(true);
        PlayerPrefs.SetInt("Skin", skin);
    }


    public void firstCustom1()
    {
        custom1[customCount1].SetActive(false);
        if (customCount1 == 0) customCount1 = custom1.Length - 1;
        else customCount1--;
        custom1[customCount1].SetActive(true);
        PlayerPrefs.SetInt("Custom1", customCount1);

    }
    public void secondCustom1()
    {
        custom1[customCount1].SetActive(false);
        if (customCount1 == custom1.Length - 1) customCount1 = 0;
        else customCount1++;
        custom1[customCount1].SetActive(true);
        PlayerPrefs.SetInt("Custom1", customCount1);
    }

    public void firstCustom2()
    {
        custom2[customCount2].SetActive(false);
        if (customCount2 == 0) customCount2 = custom2.Length - 1;
        else customCount2--;
        custom2[customCount2].SetActive(true);
        PlayerPrefs.SetInt("Custom2", customCount2);

    }
    public void secondCustom2()
    {
        custom2[customCount2].SetActive(false);
        if (customCount2 == custom2.Length - 1) customCount2 = 0;
        else customCount2++;
        custom2[customCount2].SetActive(true);
        PlayerPrefs.SetInt("Custom2", customCount2);
    }

    public void firstLocalization()
    {
        if (langCount == 0) langCount = languages.Length - 1;
        else langCount--;
        localizationManager.CurrentLanguage = languages[langCount];
    }
    public void secondLocalization()
    {
        if ( langCount == languages.Length - 1) langCount =0;
        else langCount++;
        localizationManager.CurrentLanguage = languages[langCount];
    }

    public void Exit(){
        Application.Quit();
    }
}
