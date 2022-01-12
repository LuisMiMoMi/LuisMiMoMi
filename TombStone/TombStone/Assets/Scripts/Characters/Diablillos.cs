using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diablillos : Enemy
{
    //Movimiento de los diablillos en concreto
    protected void FixedUpdate()
    {
        if (!dead && !hitted)
        {
            if (CheckSound(moveRadius))
            {
                if (CheckVision())
                {
                    Movement(speed, direction.x, direction.y);
                } else
                {
                    StopMovement();
                }
            }
            else
            {
                StopMovement();
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!dead && !hitted && canHit && CheckSound(hitRadius))
        {
            StartCoroutine(Hit(player));
        }
    }

    IEnumerator Hit(GameObject player)
    {
        canHit = false;
        if (characterAnim)
        {
            characterAnim.SetTrigger("attack");
        }
        player.GetComponent<Character>().RecibirDaño(damage, stun);
        yield return new WaitForSeconds(hitCD);
        canHit = true;
    }

    void StopMovement()
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
