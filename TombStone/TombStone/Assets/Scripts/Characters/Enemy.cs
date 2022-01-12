using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected int playerLayer = (1 << 6), enemyLayerInv = ~(1 << 7), bulletLayerInv = ~(1 << 8);
    protected Collider2D c2d;
    protected Vector3 direction;
    protected bool canHit;
    protected GameObject player;
    [SerializeField] protected float moveRadius;
    [SerializeField] protected float hitRadius;
    [SerializeField] protected float hitCD;
    [SerializeField] protected int damage;
    [SerializeField] public float stunResistance;
    [SerializeField] protected float stun;
    [SerializeField] protected bool vision, visionHit;

    protected virtual void Start()
    {
        c2d = GetComponent<Collider2D>();
        canHit = true;
    }

    //Activar la vision si está en el radio
    protected virtual bool CheckSound(float radius)
    {
        RaycastHit2D hitPlayer = Physics2D.CircleCast(c2d.bounds.center, radius, Vector2.zero, 0, playerLayer);
        if (hitPlayer)
        {
            player = hitPlayer.transform.gameObject;
            return true;
        }
        player = null;
        return false;
    }

    //Moverse si ve al objectivo
    protected virtual bool CheckVision()
    {
        RaycastHit2D hit = Physics2D.Linecast(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, enemyLayerInv + bulletLayerInv);
        //Debug.DrawLine(c2d.bounds.center, player.GetComponent<Collider2D>().bounds.center, Color.black, .5f);
        if (hit && hit.transform.GetComponent<MainCharacter>() && !hit.transform.GetComponent<MainCharacter>().dead)
        {
            direction = (hit.transform.position - transform.position).normalized;
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

    //Para destruir el GameObject desde la animacion de muerte
    void DestroyObject()
    {
        Destroy(gameObject);
    }

    protected void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (vision)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(collider.bounds.center, moveRadius);
        }

        if (visionHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(collider.bounds.center, hitRadius);
        }
        
    }
}
