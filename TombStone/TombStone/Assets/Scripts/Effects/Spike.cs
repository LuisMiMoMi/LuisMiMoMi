using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float speed = 2;
    public int damage;
    [SerializeField] float empuje;
    public float stun;
    public Vector3 direction;
    readonly int wallLayer = 9;

    protected virtual void FixedUpdate()
    {
        transform.up = direction;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<MainCharacter>() && !collision.gameObject.GetComponent<Character>().dead) || collision.gameObject.layer == wallLayer)
        {
            if (collision.gameObject.GetComponent<MainCharacter>())
            {
                collision.GetComponent<MainCharacter>().RecibirDaño(damage, stun);
                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * empuje);
            }
            Destroy(gameObject);
        }
    }

}
