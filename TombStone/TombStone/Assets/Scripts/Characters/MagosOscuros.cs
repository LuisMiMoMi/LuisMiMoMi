using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagosOscuros : Enemy
{
    [SerializeField] GameObject spell;

    //Si no esta muerto o ha sido golpeado, se movera cuando este el objetivo muy cerca y atacara dentro del radio si el objetivo esta lejos
    protected void FixedUpdate()
    {
        if (!dead && !hitted)
        {
            if (CheckSound(moveRadius))
            {
                if (CheckVision())
                {
                    Movement(speed, -direction.x, -direction.y);
                }
            }
            else if (CheckSound(hitRadius))
            {
                StopMovement();
                if (canHit && VisionToHit())
                {
                    StartCoroutine(Attack(player));
                }
            }
        }
    }

    //Lanza el ataque
    IEnumerator Attack(GameObject player)
    {
        canHit = false;
        Vector3 diretion = (player.transform.position - transform.position).normalized;
        Vector3 pos = transform.position + (diretion * 0.4f);
        GameObject bullt = Instantiate(spell, pos, Quaternion.identity);
        bullt.GetComponent<MageSpell>().damage = damage;
        bullt.GetComponent<MageSpell>().stun = stun;
        yield return new WaitForSeconds(hitCD);
        canHit = true;
    }
    
    //Comprueba si ve al objetivo
    bool VisionToHit()
    {
        RaycastHit2D hit = Physics2D.Linecast(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, enemyLayerInv + bulletLayerInv);
        //Debug.DrawLine(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, Color.black, .5f);
        if (hit && hit.transform.GetComponent<MainCharacter>() && !hit.transform.GetComponent<MainCharacter>().dead)
        {
            if (hit.transform.position.x - transform.position.x > 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (hit.transform.position.x - transform.position.x < 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            return true;
        }
        return false;
    }

    //Para el movimiento
    void StopMovement()
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    //Comprueba si ve al objetivo para moverse la direcion opuesta
    protected override bool CheckVision()
    {
        RaycastHit2D hit = Physics2D.Linecast(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, enemyLayerInv + bulletLayerInv);
        //Debug.DrawLine(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, Color.black, .5f);
        if (hit && hit.transform.GetComponent<MainCharacter>() && !hit.transform.GetComponent<MainCharacter>().dead)
        {
            direction = (hit.transform.position - transform.position).normalized;
            if (hit.transform.position.x - transform.position.x > 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (hit.transform.position.x - transform.position.x < 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            return true;
        }
        return false;
    }
}
