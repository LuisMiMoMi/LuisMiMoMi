using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [HideInInspector] public bool onFloor;
    bool changed;
    GameObject player;
    [SerializeField] GameObject door;

    void Awake()
    {
        onFloor = true;
    }

    void Update()
    {
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
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacter>() && onFloor)
        {
            player = collision.gameObject;
            changed = false;
        }

        if (collision.CompareTag(gameObject.tag))
        {
            door.GetComponent<Animator>().Play("Door");
            Destroy(gameObject);
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
