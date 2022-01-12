using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicExitChange : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    bool entered;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && !entered)
        {
            entered = true;
            collision.GetComponent<AudioSource>().clip = clip;
            collision.GetComponent<AudioSource>().Play();
        }
    }
}
