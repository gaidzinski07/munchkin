using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Jogo : MonoBehaviour
{
    /**Singleton**/
    public static Jogo Instance;

    [Header("Configuração de jogadores")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject botPrefab;
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

    public void OnGameOver(Component sender, object data)
    {
        if(sender is Jogador perdedor)
        {

            foreach (Carta carta in perdedor.GetMao().GetCartas())
            {
                perdedor.GetMao().EnviarCarta(baralhoDeTesouros, carta);
            }

            int cont = 0;
            Jogador supostoVencedor = jogadores[0];
            foreach(Jogador jogador in jogadores)
            {
                if(jogador.GetLevel() > 1)
                {
                    cont++;
                    supostoVencedor = jogador;
                }
            }
            if(cont == 1)
            {
                OnVitoria(this, supostoVencedor);
            }
            else if(cont > 1)
            {
                int moedasParaCadaJogadorVivo = perdedor.GetMoedas() / cont;
                foreach(Jogador j in jogadores)
                {
                    if(j.GetLevel() > 1)
                    {
                        j.ReceberMoedas(moedasParaCadaJogadorVivo);
                    }
                }
            }
        }
    }

    public void OnVitoria(Component sender, object data)
    {
        if (sender is Jogador jogador)
        {
            if(jogador.GetLevel() >= 10 || jogadores.Count == 1)
            {
                JogadorVitorioso.Jogador = jogador;
                SceneManager.LoadScene("Vitoria");
            }
        }
    }

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

    public Jogador GetJogadorDaVez()
    {
        return jogadores[jogadorDaVez];
    }

    public void AdicionarJogador(string nick, GeneroEnum genero)
    {
        AdicionarPlayer(nick, genero, playerPrefab);
    }

    public void AdicionarBot(string nick, GeneroEnum genero)
    {
        AdicionarPlayer("[BOT] " + nick, genero, botPrefab);
    }

    private void AdicionarPlayer(string nick, GeneroEnum genero, GameObject prefab)
    {
        if (jogadores.Count >= maxPlayers)
        {
            Debug.LogWarning("Impossível adicionar novos jogadores. Limite atingido...");
            return;
        }
        if (nick == "" || nick == null)
        {
            Debug.LogWarning("Preencha o nick do jogador.");
            return;
        }
        nick = nick;
        Jogador jogador = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Jogador>();
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
        CalcularProximoJogador();

        foreach (Jogador j in jogadores)
        {
            foreach (Carta c in j.GetMao().GetCartas().ToList())
            {
                c.SetSelecionavel(false);
                if (c.GetEstadoSelecao() == EstadoSelecaoCarta.JOGADOR || c.GetEstadoSelecao() == EstadoSelecaoCarta.MONSTRO)
                {
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

    public void EnviarRecompensaParaJogadorDaVez(int qtdRecompensas)
    {
        for(int i = 0; i < qtdRecompensas; i++)
        {
            baralhoDeTesouros.EnviarCarta(jogadores[jogadorDaVez].GetMao(), baralhoDeTesouros.GetCartas()[0]);
        }
    }

    private List<Carta> BuscarModificadoresNasMaosDosJogadores(EstadoSelecaoCarta estado)
    {
        List<Carta> list = new List<Carta>();
        foreach (Jogador j in jogadores)
        {
            if(j != jogadores[jogadorDaVez])
            {
                foreach (Carta c in j.GetMao().GetCartas().ToList())
                {
                    if(c.GetEstadoSelecao() == estado)
                    {
                        list.Add(c);
                    }
                }
            }
        }
        return list;
    }

    public void VenderCarta(CartaDeTesouro cartaDeTesouro)
    {
        if (jogadores[jogadorDaVez].GetMao().GetCartas().Contains(cartaDeTesouro))
        {
            if(jogadores[jogadorDaVez].GetMao().EnviarCarta(baralhoDeTesouros, cartaDeTesouro))
            {
                jogadores[jogadorDaVez].ReceberMoedas(cartaDeTesouro.GetPreco());
            }
        }
    }

    private void CalcularProximoJogador()
    {
        jogadorDaVez ++;
        jogadorDaVez = jogadorDaVez % jogadores.Count;
        while (jogadores[jogadorDaVez].GetLevel() < 1)
        {
            jogadorDaVez += 1;
            jogadorDaVez = jogadorDaVez % jogadores.Count;
        }
    }
}
