using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStairs : MonoBehaviour
{
    [SerializeField] float decrement;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            collision.GetComponent<Character>().speed -= decrement;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            collision.GetComponent<Character>().speed += decrement;
        }
    }
}
