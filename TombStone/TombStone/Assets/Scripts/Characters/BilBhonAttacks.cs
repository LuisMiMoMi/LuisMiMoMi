using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Estructura de los ataques de bilbhon
[System.Serializable]
public struct BilBhonAttacks
{
    public int attack;
    public float prob;
    public float probTotal;
    public float probIncrement;
}