using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CartaDeEquipamento : CartaDeTesouro
{
    [SerializeField, Range(0,2)]
    private int numMaos;
}
