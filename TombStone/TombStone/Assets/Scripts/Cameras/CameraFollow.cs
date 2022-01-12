using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    MainCharacter player;
    Tilemap tilemap;
    float asp, ortSize;
    float limX, limY;
    float minYBound, minXBound, maxYBound, maxXBound;

    void Awake()
    {
        player = FindObjectOfType<MainCharacter>();
        tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
        ortSize = Camera.main.orthographicSize;
        asp = Camera.main.aspect * ortSize;
        GetBounds();
    }

    void LateUpdate()
    {
        GetPosition();
        //La cámara se movera directamente al personaje.
        transform.position = new Vector3(limX, limY, transform.position.z);
    }

    void GetPosition()
    {
        if (player)
        {
            limX = Mathf.Clamp(player.transform.position.x, minXBound, maxXBound);
            limY = Mathf.Clamp(player.transform.position.y, minYBound, maxYBound);
        }
    }

    void GetBounds()
    {
        minXBound = tilemap.localBounds.min.x + asp;
        maxXBound = tilemap.localBounds.max.x - asp;
        minYBound = tilemap.localBounds.min.y + ortSize;
        maxYBound = tilemap.localBounds.max.y - ortSize;
    }
}
