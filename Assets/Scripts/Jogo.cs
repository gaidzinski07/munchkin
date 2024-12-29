using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    /**Singleton**/
    public static Jogo Instance;

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
    private bool podeIniciar = false;

    [Header("Game Events")]
    [SerializeField]
    private GameEvent evntIniciarJogo;

    private void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void AdicionarJogador(string nick, GeneroEnum genero)
    {
        if(jogadores.Count >= maxPlayers)
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
        jogador.CreatePlayer(nick, genero);
        jogador.ResetPlayer();
        jogadores.Add(jogador);
        podeIniciar = jogadores.Count >= minPlayers;
    }

    public List<Jogador> GetJogadores()
    {
        return jogadores;
    }

    public bool GetPodeIniciar()
    {
        return podeIniciar;
    }

    public void IniciarPartida()
    {
        evntIniciarJogo.Raise(this, null);
    }

}
