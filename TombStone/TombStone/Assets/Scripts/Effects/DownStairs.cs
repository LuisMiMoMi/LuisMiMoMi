using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairs : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>())
        { 
            Sword[] swords = FindObjectsOfType<Sword>();
            foreach (Sword sword in swords)
            {
                if (MainCharacter.mainCharacterInstance.transform.GetChild(0).childCount != 0 && MainCharacter.mainCharacterInstance.transform.GetChild(0).GetChild(0).GetComponent<Sword>() != sword)
                {
                    Destroy(sword.gameObject);
                }
            }
            GameManager.ChangeScene(GameManager.GetCurrentScene().buildIndex + 1);
        }
    }
}
