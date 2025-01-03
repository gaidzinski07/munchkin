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
    private GameObject btnFinalizerBuild;
    [SerializeField]
    private Transform centerTransform;

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

    public void IniciarTurno(Component sender, object data)
    {
        btnFinalizerBuild.SetActive(true);
        if(data is int index)
        {
            for(int i = 0; i < perfilJogadores.Count; i++)
            {
                perfilJogadores[i].OnIniciarTurno(index == i);
            }
        }
    }

    public void RemoverCartaDoBaralhoDePorta(Component sender, object data)
    {
        btnAcaoCartaRetirada.SetActive(true);
        btnFinalizerBuild.SetActive(false);
        if (data is Carta c)
        {
            c.transform.DOMove(centerTransform.position, 0.3f);
        }
    }

    public void FinalizarTurno(Component sender, object data)
    {
        btnAcaoCartaRetirada.SetActive(false);
        btnFinalizerBuild.SetActive(false);
    }

}
