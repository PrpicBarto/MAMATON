using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Menus (Panels)")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    [Header("Audio")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource[] sfxSources;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameReset;
    public UnityEvent OnPaused;
    public UnityEvent OnResumed;
    public UnityEvent OnGameOver;

    private bool openFromPause = false;
    private bool isGameOver = false;
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1.0f;

        if (mainMenu) mainMenu.SetActive(true);
        if (optionsMenu) optionsMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (gameOverMenu) gameOverMenu.SetActive(false);

        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        AudioListener.volume = savedVolume;
        if (volumeSlider)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        float savedSfx = PlayerPrefs.GetFloat("sfxVolume", 1f);
        if (sfxSlider)
        {
            sfxSlider.value = savedSfx;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        SetSFXVolume(savedSfx);
    }

    private void OnDestroy()
    {
        if (volumeSlider) volumeSlider.onValueChanged.RemoveListener(SetVolume);
        if (sfxSlider) sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    private void Update()
    {
        if (isGameOver) return;

        if (optionsMenu && optionsMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                BackFromOptions();
            return;
        }

        if (mainMenu && mainMenu.activeSelf)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PlayGame()
    {
        if (mainMenu) mainMenu.SetActive(false);
        if (optionsMenu) optionsMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (gameOverMenu) gameOverMenu.SetActive(false);

        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1f;

        OnGameStart?.Invoke();
    }

    public void OpenOptions()
    {
        openFromPause = pauseMenu && pauseMenu.activeSelf; 

        if (mainMenu) mainMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (optionsMenu) optionsMenu.SetActive(true);
    }

    public void BackFromOptions()
    {
        if (optionsMenu) optionsMenu.SetActive(false);

        if (openFromPause)
        {
            if (pauseMenu) pauseMenu.SetActive(true);
        }
        else
        {
            if (mainMenu) mainMenu.SetActive(true);
        }
    }

    public void GoToMainMenuPanel()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;

        if (mainMenu) mainMenu.SetActive(true);
        if (optionsMenu) optionsMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (gameOverMenu) gameOverMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if (isGameOver) return;

        if (pauseMenu) pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        OnPaused?.Invoke();
    }

    public void ResumeGame()
    {
        if (pauseMenu) pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        OnResumed?.Invoke();
    }
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (mainMenu) mainMenu.SetActive(false);
        if (optionsMenu) optionsMenu.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (gameOverMenu) gameOverMenu.SetActive(true);

        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void Retry()
    {
        isGameOver = false;
        isPaused = false;
        Time.timeScale = 1f;

        if (mainMenu) mainMenu.SetActive(false);
        if (optionsMenu) optionsMenu?.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (gameOverMenu) gameOverMenu.SetActive(false);

        OnGameReset?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSources != null)
        {
            for (int i = 0; i < sfxSources.Length; i++)
            {
                if (sfxSources[i]) sfxSources[i].volume = value;
            }
        }
        PlayerPrefs.SetFloat("sfxVolume", value);
        PlayerPrefs.Save();
    }
}
