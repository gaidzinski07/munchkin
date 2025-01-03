using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jogo : MonoBehaviour
{
    /**Singleton**/
    public static Jogo Instance;

    [Header("Configuração de jogadores")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField, Range(0, 6)]
    private int maxPlayers = 6;
    [SerializeField, Range(3, 6)]
    private int minPlayers = 3;
    [SerializeField]
    private List<Jogador> jogadores;
    [SerializeField]
    private Baralho baralhoDePortas;
    [SerializeField]
    private Baralho baralhoDeTesouros;
    private bool podeIniciar = false;
    private int jogadorDaVez;
    private Carta cartaDoTurno;

    [Header("Game Events")]
    [SerializeField]
    private GameEvent evntIniciarJogo;
    [SerializeField]
    private GameEvent evntIniciarTurno;
    [SerializeField]
    private GameEvent evntRemoverCartaBaralhoDePorta;
    [SerializeField]
    private GameEvent evntFinalizarTurno;
    private int turno = 0;

    private void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        baralhoDeTesouros.Embaralhar();
        baralhoDePortas.Embaralhar();
    }

    public BatalhaDTO GenerateBatalhaDTO()
    {
        if(cartaDoTurno is CartaDeMonstro)
        {
            BatalhaDTO dto = new BatalhaDTO();
            dto.cartaDeMonstro = (CartaDeMonstro) cartaDoTurno;
            dto.jogador = jogadores[jogadorDaVez];
            dto.modificadoresProJogador = BuscarModificadoresNasMaosDosJogadores(EstadoSelecaoCarta.JOGADOR);
            dto.modificadoresProMonstro = BuscarModificadoresNasMaosDosJogadores(EstadoSelecaoCarta.MONSTRO);
            return dto;
        }
        return null;
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
        jogador.ResetPlayer();
        jogador.CreatePlayer(nick, genero);
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

    private void SortearOrdemDosJogadores()
    {
        jogadores = jogadores.OrderBy(i => Guid.NewGuid()).ToList();
        jogadorDaVez = 0;
    }

    private void DistribuirCartas()
    {
        foreach(Jogador j in jogadores)
        {
            for(int i = 0; i < j.GetTamanhoMao(); i++)
            {
                baralhoDeTesouros.EnviarCarta(j.GetMao(), baralhoDeTesouros.GetCartas()[0]);
            }
        }
    }
    public void IniciarPartida()
    {
        DistribuirCartas();
        SortearOrdemDosJogadores();
        IniciarTurno();
        evntIniciarJogo.Raise(this, jogadores);
    }

    public bool EquiparCartaNaBuildDoJogadorDaVez(CartaDeBuild carta)
    {
        if (carta.PodeEquiparNaBuild(jogadores[jogadorDaVez])){
            Carta retirada = jogadores[jogadorDaVez].GetMao().RetirarCarta(carta);
            if(retirada != null)
            {
                carta.EquiparNaBuild(jogadores[jogadorDaVez]);
                return true;
            }
        }
        return false;
    }

    //Habilita Motagem da Build do Jogador da vez apenas
    private void IniciarTurno()
    {
        turno++;
        jogadorDaVez = jogadorDaVez % jogadores.Count;
        Jogador jogador = jogadores[jogadorDaVez];
        evntIniciarTurno.Raise(this, jogadorDaVez);
        foreach(Jogador j in jogadores)
        {
            foreach(Carta c in j.GetMao().GetCartas())
            {
                c.SetSelecionavel(j == jogador);
            }
        }
    }
    //Segunda parte do turno: remove uma carta do baralho de portas
    public void RemoverCartaDoBaralhoDePorta()
    {
        cartaDoTurno = baralhoDePortas.RetirarCarta();
        foreach (Jogador j in jogadores)
        {
            foreach (Carta c in j.GetMao().GetCartas())
            {
                c.SetSelecionavel(j != jogadores[jogadorDaVez]);
            }
        }
        evntRemoverCartaBaralhoDePorta.Raise(this, cartaDoTurno);
    }

    public void FinalizarTurno()
    {
        baralhoDePortas.AdicionarCarta(cartaDoTurno);
        evntFinalizarTurno.Raise(this, cartaDoTurno);
        cartaDoTurno = null;
        jogadorDaVez++;

        foreach (Jogador j in jogadores)
        {
            foreach (Carta c in j.GetMao().GetCartas().ToList())
            {
                c.SetSelecionavel(false);
                Debug.Log(c.GetEstadoSelecao());
                if (c.GetEstadoSelecao() == EstadoSelecaoCarta.JOGADOR || c.GetEstadoSelecao() == EstadoSelecaoCarta.MONSTRO)
                {
                    Debug.Log("Enviando carta " + c.GetNome());
                    c.SetSelecionavel(false);
                    j.GetMao().EnviarCarta(baralhoDeTesouros, c);
                }
                else
                {
                    c.SetEstado(0);
                }
            }
        }

        IniciarTurno();
    }

    public void ExecutarAcaoDaCartaRetirada()
    {
        if (cartaDoTurno == null) return;

        cartaDoTurno.ExecutarAcao();
    }

    public void DevolverCartaParaBaralhoDeTesouro(Carta carta)
    {
        baralhoDeTesouros.AdicionarCarta(carta);
    }

    private List<Carta> BuscarModificadoresNasMaosDosJogadores(EstadoSelecaoCarta estado)
    {
        List<Carta> list = new List<Carta>();
        foreach (Jogador j in jogadores)
        {
            foreach (Carta c in j.GetMao().GetCartas().ToList())
            {
                if(c.GetEstadoSelecao() == estado)
                {
                    list.Add(c);
                }
            }
        }
        return list;
    }
}
