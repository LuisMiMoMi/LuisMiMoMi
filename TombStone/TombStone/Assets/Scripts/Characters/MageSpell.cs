using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float time = 6;
    int wallLayer = 9;
    Vector3 direction;
    [HideInInspector] public int damage;
    [HideInInspector] public float stun;
    bool hit;
    GameObject player;

    //Se establece la direccion a la que ira el hechizo
    void Awake()
    {
        player = FindObjectOfType<MainCharacter>().gameObject;
        direction = (player.transform.position - transform.position).normalized;
        transform.right = direction;
        Destroy(gameObject, time);
    }

    //Mueve el objecto hasta la posicion objetivo
    void FixedUpdate()
    {
        if (!hit)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    //Cuando choca con el objetivo, se ejecuta la animacion de la explosion
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<MainCharacter>() && !collision.gameObject.GetComponent<MainCharacter>().dead) || collision.gameObject.layer == wallLayer)
        {
            hit = true;
            if (collision.gameObject.GetComponent<MainCharacter>())
            {
                collision.GetComponent<MainCharacter>().RecibirDaño(damage, stun);
            }
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().Play("SpellAnimation");
            }
            else
            {
                Destroy(gameObject, 0.40f);
            }
        }
    }

    //Destruir el hechizo desde la animacion
    public void DestroySpell()
    {
        Destroy(gameObject);
    }
}
