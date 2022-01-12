using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    [SerializeField] public AudioClip clip;
    bool entered;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && !entered)
        {
            entered = true;
            collision.GetComponent<AudioSource>().clip = clip;
            collision.GetComponent<AudioSource>().Play();
        }
    }
}
