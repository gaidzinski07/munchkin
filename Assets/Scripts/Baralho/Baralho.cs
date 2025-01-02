using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Baralho : ConjuntoDeCartas
{
    public void Embaralhar()
    {
        int tamanhoBaralho = cartas.Count;
        List<Carta> embaralhado = new List<Carta>();

        for (int i = 0; i < tamanhoBaralho; i++)
        {
            embaralhado.Add(null);
        }

        for (int i = 0; i < tamanhoBaralho; i++)
        {
            int newIndex = Random.Range(0, tamanhoBaralho);
            while (embaralhado[newIndex] != null)
            {
                newIndex = Random.Range(0, tamanhoBaralho);
            }
            embaralhado[newIndex] = cartas[i];
        }
        cartas = embaralhado;
    }

    public override void OnAdicionarCarta(Carta carta)
    {
        base.OnAdicionarCarta(carta);
        carta.SetSelecionavel(false);
    }
}
