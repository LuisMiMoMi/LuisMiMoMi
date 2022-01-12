using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBehaviour : MonoBehaviour
{
    string historiaFinal = "Tras un largo combate final, y muchas vidas arrebatadas, consigues salir de la mazmorra. " +
        "No obstante, no sabes d�nde est�s, pero caminas varios kil�metros hasta llegar a un peque�o poblado, donde los pueblerinos te socorren las heridas. " +
        "Por fin todo ha acabado, y podr�s seguir con tu vida, aunque en un mundo totalmente distinto.";
    [SerializeField] Text textoHistoria;
    void Start()
    {
        //Se inicia la escritura del texto final
        if (textoHistoria)
        {
            StartCoroutine(Reloj());
        }
    }

    IEnumerator Reloj()
    {
        //Escribe cada caracter del texto cada 0.1 segundos, y el audio se para al acabar el texto de escribirse
        foreach (char caracter in historiaFinal)
        {
            textoHistoria.text += caracter;
            yield return new WaitForSeconds(0.1f);
        }
        if (GetComponent<AudioSource>())
        {
            StopAudioScribir();
        }
    }

    void StopAudioScribir() 
    {
        GetComponent<AudioSource>().Stop();
    }
}
