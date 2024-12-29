using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dado
{
    [SerializeField]
    private int numFaces;

    public Dado(int numFaces)
    {
        this.numFaces = numFaces;
    }
    
    public int Rolar()
    {
        return Random.Range(1, numFaces);
    }
}
