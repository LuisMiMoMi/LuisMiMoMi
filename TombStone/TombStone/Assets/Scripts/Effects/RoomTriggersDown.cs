using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggersDown : RoomTriggers
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && (collision.transform.position.y - transform.position.y) < 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Collider2D>().isTrigger = false;
            onRoom = true;
        }
    }
}
