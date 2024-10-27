using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{
    private List<Carta> cartas;

    public void embaralhar()
    {
        int tamanhoBaralho = cartas.Count;
        List<Carta> embaralhado = new List<Carta>(tamanhoBaralho);
        for(int i = 0; i < tamanhoBaralho; i++)
        {
            int newIndex = Random.Range(0, tamanhoBaralho);
            while(embaralhado[newIndex] != null)
            {
                newIndex = Random.Range(0, tamanhoBaralho);
            }
            embaralhado[newIndex] = cartas[i];
        }
        cartas = embaralhado;
    }

    public void adicionarCarta(Carta carta)
    {
        cartas.Add(carta);
    }

    public Carta retirarCarta()
    {
        Carta carta = cartas[0];
        cartas.Remove(carta);
        return carta;
    }
}
