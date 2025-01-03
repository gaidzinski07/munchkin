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

    public virtual void OnAdicionarCarta(Carta carta) {
        carta.gameObject.SetActive(true);
    }

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

    public virtual void OnRetirarCarta(Carta carta){
        carta.CloseContextMenu();
        carta.SetSelecionavel(false);
        carta.SetEstado(0);
    }

    public void Esvaziar()
    {
        cartas.Clear();
    }

    public void EnviarCarta(ConjuntoDeCartas destino, Carta carta)
    {
        Debug.Log(carta.GetNome());
        if (!cartas.Contains(carta)) return;
        if(destino.ReceberCarta(carta))
        {
            RetirarCarta(carta);
        }
    }

}
