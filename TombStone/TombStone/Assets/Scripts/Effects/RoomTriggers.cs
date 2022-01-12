using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggers : MonoBehaviour
{
    protected GameObject room;
    protected bool onRoom;
    protected int enemyLayer = (1 << 7);
    protected float timer;

    void Start()
    {
        room = transform.GetChild(0).gameObject;
    }
    
    protected virtual void Update()
    {
        if (onRoom)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                if (Physics2D.BoxCastAll(room.GetComponent<SpriteRenderer>().bounds.center, room.GetComponent<SpriteRenderer>().bounds.size, 0, Vector2.zero, 0, enemyLayer).Length == 0)
                {
                    this.gameObject.SetActive(false);
                }
                timer = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && (collision.transform.position.x - transform.position.x) > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Collider2D>().isTrigger = false;
            onRoom = true;
        }
    }
}
