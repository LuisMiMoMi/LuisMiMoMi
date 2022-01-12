using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealing : MonoBehaviour
{
    int playerMask = (1 << 6);
    [SerializeField] int healPower;

    void Update()
    {
        RaycastHit2D player = Physics2D.BoxCast(transform.position, transform.GetComponent<SpriteRenderer>().size, 0, Vector2.zero, 0, playerMask);
        if (player)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Healing(player.transform.gameObject, healPower);
                Destroy(gameObject);
            }
        }
    }

    void Healing(GameObject player, int healPower)
    {
        MainCharacter mainCharacter = player.GetComponent<MainCharacter>();
        if (mainCharacter.actualHealth + healPower > mainCharacter.health)
        {
            mainCharacter.actualHealth = mainCharacter.health;
        }
        else
        {
            mainCharacter.actualHealth += healPower;
        }
        mainCharacter.playerHealth.RefreshHearts(mainCharacter.actualHealth, mainCharacter.health);
    }
}
