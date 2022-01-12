using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MenuBehaviour
{
    GameObject optionsCanvas;
    GameObject playerHealthCanvas;
    MainCharacter mainCharacter;

    //Obtenemos los datos que necesitamos y establecemos el delegado
    void Awake()
    {
        playerHealthCanvas = GameObject.Find("PlayerHealthCanvas");
        mainCharacter = FindObjectOfType<MainCharacter>();
        mainCharacter.OnPlayerDeath += GameOver;
    }

    //Comprobamos si apreta esc para poner pausa
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerHealthCanvas.activeSelf)
        {
            ActivatePanel(0);
        }
    }

    //Activa el playerHealth
    public void ActivatePlayerCanvas()
    {
        for (int i = 0; i < playerHealthCanvas.transform.childCount; i++)
        {
            playerHealthCanvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    //Activa el panel que le pidas, primero desactivando la vida del personaje
    void ActivatePanel(int panel)
    {
        for (int i = 0; i < playerHealthCanvas.transform.childCount; i++)
        {
            playerHealthCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (optionsCanvas == null)
        {
            optionsCanvas = GameObject.Find("OptionsCanvas");
        }
        PauseGame();
        optionsCanvas.transform.GetChild(panel).gameObject.SetActive(true);
    }

    //Destruye todo el contenido que puede haber en el dontdestroyonload
    public void DestroyDontDestroyOnLoad()
    {
        Destroy(MainCharacter.mainCharacterInstance);
        Sword[] swords = FindObjectsOfType<Sword>();
        foreach (Sword sword in swords)
        {
            Destroy(sword.gameObject);
        }
    }

    //Reinicia el juego
    public void RestartGame()
    {
        ActivatePlayerCanvas();
        mainCharacter.GetComponent<AudioSource>().Stop();
        ResumeGame();
        ChangeScene(GameManager.GetCurrentScene().name);
    }

    //Saca la pantalla de gameOver, parando la musica y haciendo desaparecer los posibles canvas de la pantalla
    public void GameOver()
    {
        if (GameObject.FindGameObjectWithTag("BilbhonLife"))
        {
            GameObject.FindGameObjectWithTag("BilbhonLife").gameObject.SetActive(false);
        }
        if (GameManager.GetCurrentScene().buildIndex == 4)
        {
            mainCharacter.GetComponent<AudioSource>().Stop();
            mainCharacter.GetComponent<AudioSource>().clip = null;
        }
        ActivatePanel(2);
    }
}
