using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResistance : MonoBehaviour
{
    [SerializeField] MainCharacter player;
    Image resistImage;

    //Obtiene la imagen que representara la resistencia
    void Start()
    {
        resistImage = GetComponent<Image>();
    }

    //Actualiza la imagen según la resistencia actual del jugador en referencia a su resistencia maxima
    void Update()
    {
        resistImage.fillAmount = player.actualResistance / player.maxResistance;
    }
}
