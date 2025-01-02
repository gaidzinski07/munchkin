using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject screenPreGame;
    [SerializeField]
    private GameObject screenInGame;

    [SerializeField]
    private List<PerfilJogador> perfilJogadores;
    [SerializeField]
    private GameObject btnAcaoCartaRetirada;
    [SerializeField]
    private Transform centerTransform;
    [SerializeField]
    private Transform baralhoDePortaTransform;
    [SerializeField]
    private Transform baralhoDeTesouroTransform;

    private void Start()
    {
        screenPreGame.SetActive(true);
        screenInGame.SetActive(false);
    }

    public void IniciarJogo(Component sender, object data)
    {
        screenPreGame.SetActive(false);
        screenInGame.SetActive(true);
        
        List<Jogador> list = (List<Jogador>)data;
        int i = 0;
        foreach(Jogador jogador in list)
        {
            perfilJogadores[i++].SetJogador(jogador);
        }

    }

    public void AtualizarJogador(Component sender, object data)
    {
        if(data is Jogador)
        {
            var jogador = (Jogador)data;

            foreach(PerfilJogador p in perfilJogadores)
            {
                if (p.EhMeuJogador(jogador))
                {
                    p.SetJogador(jogador);
                    break;
                }
            }
        }
    }

    public void IniciarTurno(Component sender, object data)
    {
        btnAcaoCartaRetirada.SetActive(true);
        if(data is Carta)
        {
            Carta carta = (Carta)data;
            carta.transform.DOMove(centerTransform.position, 0.3f);
        }
    }

    public void FinalizarTurno(Component sender, object data)
    {
        btnAcaoCartaRetirada.SetActive(false);
        if (data is Carta)
        {
            Carta carta = (Carta)data;
            carta.transform.DOMove(baralhoDePortaTransform.position, 0.3f);
        }
    }

    public void LevarCartaParaBaralhoDeTesouro(Component sender, object data)
    {
        if (data is Carta)
        {
            Carta carta = (Carta)data;
            carta.transform.DOMove(baralhoDeTesouroTransform.position, 0.3f);
        }
    }

}
