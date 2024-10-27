using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoBaralhoEnum
{
    ABERTO,
    FECHADO
}

public class Baralho : MonoBehaviour
{
    [SerializeField]
    private List<Carta> cartas;
    [SerializeField]
    private TipoBaralhoEnum tipoBaralho = TipoBaralhoEnum.FECHADO;

    private void Start()
    {
        embaralhar();
    }

    public void embaralhar()
    {
        int tamanhoBaralho = cartas.Count;
        List<Carta> embaralhado = new List<Carta>();
        
        for(int i=0; i<tamanhoBaralho; i++)
        {
            embaralhado.Add(null);
        }

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

    //mudar Retorno para Carta
    public void retirarCarta()
    {
        Carta carta = cartas[0];
        cartas.Remove(carta);
        onRetirarCarta(carta);
        //return carta;
    }

    private void onRetirarCarta(Carta carta)
    {
        Transform cartaTransform = carta.transform;
        cartaTransform.position = transform.position + new Vector3(4f, 0, 0);
    }

}
