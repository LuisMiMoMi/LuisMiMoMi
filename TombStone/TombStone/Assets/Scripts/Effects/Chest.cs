using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] GameObject sword;
    [SerializeField] GameObject potion;
    Collider2D col;
    bool playerInside;
    int playerMask = (1 << 6);

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D player = Physics2D.BoxCast(transform.position, new Vector2(col.bounds.size.x * 2f, col.bounds.size.y * 2f), 0, Vector2.down, 0.1f, playerMask);
                if (player)
                {
                    if (GetComponent<Animator>())
                    {
                        GetComponent<Animator>().Play("Chest");
                    }
                    else
                    {
                        RevealObjects();
                    }
                }
            }
        }
        
    }

    public void RevealObjects()
    {
        sword.SetActive(true);
        potion.SetActive(true);
        Destroy(GetComponent<Chest>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>())
        {
            transform.GetChild(0).GetComponent<Animation>().Play("OpenEVisual");
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>())
        {
            transform.GetChild(0).GetComponent<Animation>().Play("CloseEVisual");
            playerInside = false;
        }
    }

    void OnDestroy()
    {
        transform.GetChild(0).GetComponent<Animation>().Play("CloseEVisual");
    }
}
