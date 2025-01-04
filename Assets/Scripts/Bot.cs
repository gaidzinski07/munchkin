using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Jogador
{
    public void OnIniciarTurno(Component sender, object data)
    {
        if(Jogo.Instance.GetJogadorDaVez() == this)
        {
            Jogo.Instance.RemoverCartaDoBaralhoDePorta();
        }
    }

    public void OnRemoverCartaDoBaralhoDePorta(Component sender, object data)
    {
        if(Jogo.Instance.GetJogadorDaVez() == this && data is Carta)
        {
            Jogo.Instance.ExecutarAcaoDaCartaRetirada();
            Carta carta = (Carta) data;
            StartCoroutine(OnExecutarAcaoDaCartaRetirada(carta));
        }
    }
    private IEnumerator OnExecutarAcaoDaCartaRetirada(Carta carta)
    {
        if(carta is CartaDeMonstro c)
        {
            yield return new WaitForSeconds(1);
            Batalha b = FindObjectOfType<Batalha>();
            b.RealizarFuga();
            yield return new WaitForSeconds(2f);
            b.ExecutarResultado();
        }
        else
        {
            Jogo.Instance.FinalizarTurno();
        }
    }
}
