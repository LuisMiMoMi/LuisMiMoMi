using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image heart;
    [SerializeField] Sprite[] heartSprites;

    //Funcion para restablecer los corazones
    public void RefreshHearts(int actualHealth, int maxHealth)
    {
        //Destruye los corazones
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //Obtiene el numero de corazones enteros que se van a dibujar
        int cantCorazon = Mathf.FloorToInt(actualHealth / 2);
        //Dibuja los corazones enteros en caso de haberlos
        for (int i = 0; i < cantCorazon; i++)
        {
            Image newHeart = Instantiate(heart, transform.position, Quaternion.identity);
            newHeart.transform.SetParent(transform);
        }
        //Dibuja los corazones vacios en caso de haberlos
        if (actualHealth % 2 != 0)
        {
            Image newHeart = Instantiate(heart, transform.position, Quaternion.identity);
            newHeart.transform.SetParent(transform);
            newHeart.sprite = heartSprites[0];
        }
        //Calcula la cantidad de corazones vacios
        int index = Mathf.FloorToInt((maxHealth - actualHealth) / 2);
        //Dibuja los corazones vacios en caso de haberlos
        for (int i = 0; i < index; i++)
        {
            Image newHeart = Instantiate(heart, transform.position, Quaternion.identity);
            newHeart.transform.SetParent(transform);
            newHeart.sprite = heartSprites[1];
        }
    }
}
