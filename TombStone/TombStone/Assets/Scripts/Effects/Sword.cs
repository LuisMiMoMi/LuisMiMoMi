using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float empuje;
    [SerializeField] float stun;
    [HideInInspector] public bool onFloor;
    [SerializeField] float atSpeed;
    bool atCd, changed;
    Animation swordAnim;
    GameObject player;


    void Awake()
    {
        if (GetComponent<Animation>())
        {
            swordAnim = GetComponent<Animation>();
        }
        onFloor = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !atCd && !onFloor)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.E) && onFloor && player != null && !changed)
        {
            if (player.transform.GetChild(0).childCount != 0)
            {
                Transform lastItem = player.transform.GetChild(0).GetChild(0);
                lastItem.SetParent(null);
                changed = true;
                lastItem.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                if (lastItem.GetComponent<Sword>())
                {
                    lastItem.GetComponent<Sword>().onFloor = true;
                    lastItem.GetComponent<Collider2D>().enabled = true;
                }
                else if (lastItem.GetComponent<Key>())
                {
                    lastItem.GetComponent<Key>().onFloor = true;
                }
            }
            onFloor = false;
            transform.SetParent(player.transform.GetChild(0));
            transform.localPosition = new Vector3(0, player.GetComponent<Collider2D>().bounds.extents.y + GetComponent<Collider2D>().bounds.extents.y);
            transform.rotation = new Quaternion();
            GetComponent<SpriteRenderer>().sortingLayerName = "Walls";
            GetComponent<Collider2D>().enabled = false; 
        }
    }

    IEnumerator Attack()
    {
        atCd = true;
        swordAnim.Play();
        yield return new WaitForSeconds(atSpeed);
        atCd = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() && !collision.GetComponent<Enemy>().dead && !collision.GetComponent<Enemy>().hitted)
        {
            if (stun - collision.GetComponent<Enemy>().stunResistance < 0)
            {
                stun = 0;
            }
            collision.GetComponent<Enemy>().RecibirDaño(damage, stun);
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * empuje, ForceMode2D.Impulse);
        }

        if (collision.GetComponent<MainCharacter>() && onFloor)
        {
            player = collision.gameObject;
            changed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && onFloor)
        {
            player = null;
        }
    }
}
