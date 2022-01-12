using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float speed;
    public bool hitted;
    protected Animator characterAnim;
    public bool dead;
    [SerializeField] public int health = 3;
    public int actualHealth;
    protected SpriteRenderer sprite;
    protected Collider2D characterCollider;
    [SerializeField] protected Image healthImg;
    protected Rigidbody2D rb2d;

    //Obtengo parametros que hacen falta posteriormente
    void Awake()
    {
        characterCollider = transform.GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (GetComponent<Animator>())
        {
            characterAnim = GetComponent<Animator>();
        }
        if (GetComponent<Rigidbody2D>())
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        actualHealth = health;
    }

    //Refresca las barras de vida
    protected virtual void Update()
    {
        if (healthImg)
        {
            healthImg.fillAmount = (float)actualHealth / health;
        }
    }

    //Moviemiento general de todos los characters del juego
    protected void Movement(float spd, float directionX, float directionY)
    {
        if (!dead && !hitted)
        {
            if (GetComponent<Rigidbody2D>())
            {
                rb2d.velocity = new Vector2(directionX, directionY) * spd;
                if (characterAnim)
                {
                    characterAnim.SetFloat("speed", rb2d.velocity.x + rb2d.velocity.y);
                }
            }
            else
            {
                transform.Translate(new Vector3(directionX, directionY) * spd * Time.deltaTime);
                if (characterAnim)
                {
                    characterAnim.SetFloat("speed", spd);
                }
            }
        }
    }

    //Si estas vivo y no te acaban de golpear, recibes daño, y si estás muerto, ejecuta la de muerte
    public virtual void RecibirDaño(int damage, float stun)
    {
        if (!dead && !hitted)
        {
            
            actualHealth -= damage;
            if (actualHealth <= 0)
            {
                dead = true;
                Muerte();
                return;
            }
            StartCoroutine(Golpe(stun));
        }
    }

    //Corutina para ver el golpe y dejarte golpeado
    IEnumerator Golpe(float stun)
    {
        hitted = true;
        if (characterAnim)
        {
            characterAnim.SetTrigger("hit");
        }
        yield return new WaitForSeconds(stun);
        hitted = false;
    }

    //Funcion general de muerte
    protected virtual void Muerte()
    {
        if (characterAnim)
        {
            characterAnim.SetBool("dead", true);
        }
        if (this.gameObject.GetComponent<Rigidbody2D>())
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (this.gameObject.GetComponent<Collider2D>())
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

    }

}
