using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaoJogador : ConjuntoDeCartas
{
    [SerializeField]
    private int tamanho = 5;
    [SerializeField]
    private GameEvent evntCartaRecebida;

    public override bool ReceberCarta(Carta carta)
    {
        if(cartas.Count < tamanho)
        {
            AdicionarCarta(carta);
            return true;
        }
        return false;
    }

    public override void AdicionarCarta(Carta carta)
    {
        if (cartas.Count < tamanho)
        {
            base.AdicionarCarta(carta);
        }
    }

    public override void OnAdicionarCarta(Carta carta)
    {
        base.OnAdicionarCarta(carta);
        carta.SetSelecionavel(true);
    }

    public int GetTamanho()
    {
        return tamanho;
    }
}
