using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilbhonHammer : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float empuje;
    [SerializeField] float stun;

    //Al entrar en contacto con el player, si no esta muerto ni esta golpeado, le hace daño y empuja
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && !collision.GetComponent<MainCharacter>().dead && !collision.GetComponent<MainCharacter>().hitted)
        {
            collision.GetComponent<MainCharacter>().RecibirDaño(damage, stun);
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * empuje, ForceMode2D.Impulse);
        }
    }
}
