using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDePorta : Carta
{
    [SerializeField]
    private bool podeIrParaMaoDoJogador = false;
    public override void executarAcao()
    {
        Debug.Log(this.getNome());
    }
}
