using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Timer());
        StartCoroutine(Skip());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        GameManager.ChangeScene(GameManager.GetCurrentScene().buildIndex+1);
    }

    //La corutina espera hasta que se aprete el click izquierdo del raton para pasar a la siguiente escena
    IEnumerator Skip()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        GameManager.ChangeScene(GameManager.GetCurrentScene().buildIndex + 1);
    }
}
