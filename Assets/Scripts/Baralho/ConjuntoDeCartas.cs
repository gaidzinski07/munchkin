using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ConjuntoDeCartas
{
    [SerializeField]
    protected List<Carta> cartas = new List<Carta>();

    public List<Carta> GetCartas()
    {
        return cartas;
    }

    public virtual void AdicionarCarta(Carta carta)
    {
        cartas.Add(carta);
        OnAdicionarCarta(carta);
    }

    public virtual bool ReceberCarta(Carta carta)
    {
        AdicionarCarta(carta);
        return true;
    }

    public virtual void OnAdicionarCarta(Carta carta) { }

    public Carta RetirarCarta()
    {
        Carta carta = cartas[0];
        cartas.Remove(carta);
        OnRetirarCarta(carta);
        return carta;
    }

    public Carta RetirarCarta(Carta carta)
    {
        cartas.Remove(carta);
        OnRetirarCarta(carta);
        return carta;
    }

    public virtual void OnRetirarCarta(Carta carta){ }

    public void Esvaziar()
    {
        cartas.Clear();
    }

    public void EnviarCarta(ConjuntoDeCartas destino, int indexCarta)
    {
        if (cartas.Count <= indexCarta) return;
        Carta carta = cartas[indexCarta];
        if(destino.ReceberCarta(carta))
        {
            RetirarCarta(carta);
        }
    }

}
