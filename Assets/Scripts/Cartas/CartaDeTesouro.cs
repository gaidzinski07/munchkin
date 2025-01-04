using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CartaDeTesouro : Carta
{
    [SerializeField]
    protected int preco;

    public int GetPreco()
    {
        return preco;
    }

    public virtual void Vender()
    {
        Jogo.Instance.VenderCarta(this);
    }
}
