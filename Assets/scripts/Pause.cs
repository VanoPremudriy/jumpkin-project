using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    [SerializeField] private LocalizationManager localizationManager;
    public static bool isGamePause = false;
    public GameObject pauseMenuUI;
    public GameObject gameInterface;
    public GameObject[] pump;
    public GameObject[] Custom1;
    public GameObject[] Custom2;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private AudioSource[] EffectsAudioSource;
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
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

    void resume()
    {
        pauseMenuUI.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
        isGamePause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        gameInterface.SetActive(false);
        Time.timeScale = 0f;
        isGamePause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void loadMenu()
    {
        pauseMenuUI.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void quitGame()
    {
        Application.LoadLevel(0);
    }
}
