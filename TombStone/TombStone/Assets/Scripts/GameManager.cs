using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager
{
    public static float generalVolume = 0.4f;
    public static Resolution generalResolution;
    public static bool isFemale;
    public static bool fullscreen;

    public static void StartGame(string scene)
    {
        ChangeScene(scene);
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        PauseAudioSources();
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        ResumeAudioSources();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    //Los dos tipos de llamadas a las escenas, de forma en que segun necesite llamarla con int o con string, tengo metodo
    public static void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    //Obtiene la escena actual, esto se usa para poder cambiar a la siguiente escena, la cual es el siguiente nivel de la dungeon,
    //o para repetir escena al morir
    public static Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static void ResolutionManager(int value)
    {
        Resolution resolution = GetResolutions()[value];
        generalResolution = resolution;
        SetResolution(generalResolution.width, generalResolution.height, generalResolution.refreshRate);
    }

    //Nos sirve para poder en fullScreen o no el juego
    public static void SetFullScreen(bool value)
    {
        fullscreen = value;
        Screen.fullScreen = value;
    }

    public static void SetResolution(int width, int height, int refreshRate)
    {
        Screen.SetResolution(width, height, fullscreen, refreshRate);
    }

    public static void VolumenManager(float volume)
    {
        AudioSource[] audios = GetAudioSources();
        foreach (AudioSource audioSource in audios)
        {
            audioSource.volume = volume;
        }
        generalVolume = volume;
        PlayerPrefs.SetFloat("generalVolume", generalVolume);
    }

    public static void PauseAudioSources()
    {
        AudioSource[] audios = GetAudioSources();
        foreach (AudioSource audioSource in audios)
        {
            audioSource.Pause();
        }
    }

    public static void ResumeAudioSources()
    {
        AudioSource[] audios = GetAudioSources();
        foreach (AudioSource audioSource in audios)
        {
            audioSource.Play();
        }
    }

    public static AudioSource[] GetAudioSources()
    {
        return Object.FindObjectsOfType<AudioSource>();
        
    }

    public static Resolution[] GetResolutions()
    {
        return Screen.resolutions;
    }

    public static Resolution GetCurrentResolution()
    {
        return Screen.currentResolution;
    }
}
