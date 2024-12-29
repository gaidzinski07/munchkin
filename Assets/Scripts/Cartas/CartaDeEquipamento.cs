using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeEquipamento : Carta
{
    [SerializeField, Range(0,2)]
    private int numMaos;
    [SerializeField]
    private TamanhoEnum tamanho;

    public override void ExecutarAcao()
    {
        throw new System.NotImplementedException();
    }
}
