using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeMonstroHostil : CartaDePorta
{
    [Range(1, 9), SerializeField]
    private int level;
    [SerializeField]
    private string recompensa;

}
