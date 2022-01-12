using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    //En el start iniciamos y seteamos toda la informacion de los menus
    void Start()
    {
        if (GetComponent<Dropdown>())
        {
            SetResolutions();
        }
        if (PlayerPrefs.HasKey("isFullScreen"))
        {
            if (PlayerPrefs.GetInt("isFullScreen") == 1)
            {
                GameManager.fullscreen = true;
            }
            else
            {
                GameManager.fullscreen = false;
            }
        }
        else
        {
            GameManager.fullscreen = true;
            PlayerPrefs.SetInt("isFullScreen", 1);
        }
        SetFullScreen(GameManager.fullscreen);
        if (FindObjectOfType<Toggle>(true))
        {
            if (PlayerPrefs.GetInt("isFullScreen") == 1)
            {
                FindObjectOfType<Toggle>(true).isOn = true;
                
            }
            else
            {
                FindObjectOfType<Toggle>(true).isOn = false;
            }
        }
        if (!PlayerPrefs.HasKey("generalVolume"))
        {
            PlayerPrefs.SetFloat("generalVolume", GameManager.generalVolume);
        }
        if (GameManager.GetAudioSources() != null)
        {
            GameManager.VolumenManager(PlayerPrefs.GetFloat("generalVolume"));
        }
        if (FindObjectOfType<Slider>(true))
        {
            FindObjectOfType<Slider>(true).value = PlayerPrefs.GetFloat("generalVolume");
        }
    }

    //Generamos la lista que se incluirá en el dropdown de las resoluciones
    void SetResolutions()
    {
        Resolution[] resolutions = GameManager.GetResolutions();
        foreach (Resolution resolution in resolutions)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(resolution.ToString(),null);
            GetComponent<Dropdown>().options.Add(optionData);
        }
        Resolution currentResolution = GameManager.GetCurrentResolution();
        GameManager.generalResolution = currentResolution;
        GetComponent<Dropdown>().transform.GetChild(0).GetComponent<Text>().text = GameManager.generalResolution.ToString();
    }

    //Establece la pantalla completa o la quita
    public void SetFullScreen(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt("isFullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullScreen", 0);
        }
        if (GetComponent<Toggle>())
        {
            GetComponent<Toggle>().isOn = value;
        }
        GameManager.SetFullScreen(value);
    }

    //Establece el isFemale, que sirve para cambiar el personaje entre chico o chica
    public void SetPlayer(bool isFemale)
    {
        GameManager.isFemale = isFemale;
    }

    public void VolumeManager(float value)
    {
        GameManager.VolumenManager(value);
    }

    public void ResolutionManager(int value)
    {
        GameManager.ResolutionManager(value);
    }

    public void StartGame()
    {
        GameManager.StartGame("Controls");
    }

    public void PauseGame()
    {
        GameManager.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.ResumeGame();
    }

    public void ChangeScene(string scene)
    {
        GameManager.ChangeScene(scene);
    }

    public void Close()
    {
        GameManager.QuitGame();
    }
}
