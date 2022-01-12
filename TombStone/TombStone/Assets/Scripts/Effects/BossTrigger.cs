using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : RoomTriggers
{
    [SerializeField] GameObject boss;

    protected override void Update()
    {
        if (onRoom)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                if (Physics2D.BoxCastAll(room.GetComponent<SpriteRenderer>().bounds.center, room.GetComponent<SpriteRenderer>().bounds.size, 0, Vector2.zero, 0, enemyLayer).Length == 0)
                {
                    transform.GetChild(1).GetComponent<Animator>().Play("BossDoorAnim");
                    this.enabled = false;
                }
                timer = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && (collision.transform.position.y - transform.position.y) < 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Collider2D>().isTrigger = false;
            onRoom = true;
            boss.SetActive(true);
        }
    }
}
