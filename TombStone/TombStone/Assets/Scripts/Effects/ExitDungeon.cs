using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDungeon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>())
        {
            Destroy(collision.gameObject);
            GameManager.ChangeScene(GameManager.GetCurrentScene().buildIndex + 1);
        }
    }
}
