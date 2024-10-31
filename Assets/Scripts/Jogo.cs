using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField, Range(0, 6)]
    private int maxPlayers = 6;
    [SerializeField, Range(0, 6)]
    private int minPlayers = 3;
    [SerializeField]
    private List<Jogador> jogadores;
    [SerializeField]
    private Baralho baralhoDePortas;
    [SerializeField]
    private Baralho baralhoDeTesouros;
    [SerializeField]
    private Baralho baralhoDescartePortas;
    [SerializeField]
    private Baralho baralhoDescarteTesouros;

    public void adicionarJogador(string nick)
    {
        if(jogadores.Count >= 6)
        {
            Debug.LogWarning("Impossível adicionar novos jogadores. Limite atingido...");
            return;
        }
        if(nick == "" || nick == null)
        {
            Debug.LogWarning("Preencha o nick do jogador.");
            return;
        }
        Jogador jogador = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Jogador>();
        jogador.setNick(nick);
        jogador.resetPlayer();
        jogadores.Add(jogador);
    }

}
