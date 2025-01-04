using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GeneroEnum
{
    FEMININO = 0, 
    MASCULINO = 1
}
public class Jogador : MonoBehaviour
{
    [SerializeField]
    private string nick;
    [SerializeField, Range(1, 10)]
    private int level;
    [SerializeField, Range(0, 1000)]
    private int moedas;
    [SerializeField]
    private BuildJogador build = new BuildJogador();
    [SerializeField]
    private MaoJogador mao;

    [Header("Eventos")]
    [SerializeField]
    private GameEvent evntGameOver, evntVitoria;

    public void CreatePlayer(string nick, GeneroEnum genero)
    {
        this.nick = nick;
        build.SetGenero(genero);
    }
    public string GetNick()
    {
        return this.nick;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public int GetMoedas()
    {
        return moedas;
    }

    public BuildJogador GetBuild()
    {
        return build;
    }

    public void ResetPlayer()
    {
        level = 1;
        moedas = 0;
        this.build = new BuildJogador();
        if (mao != null) mao.Esvaziar();
    }

    public void AlterarNivel(int niveis)
    {
        level += niveis;
        moedas = 0;
        if(level < 1)
        {
            evntGameOver.Raise(this, level);
        }
        else if(level >= 10)
        {
            evntVitoria.Raise(this, level);
        }
    }

    public void ReceberMoedas(int moedas)
    {
        if (moedas <= 0) return;
        this.moedas += moedas;
        if(this.moedas >= 1000)
        {
            AlterarNivel(1);
            this.moedas = 0;
        }
    }

    public void ReceberCarta(Carta carta)
    {
        mao.AdicionarCarta(carta);
    }

    public int GetTamanhoMao()
    {
        return mao.GetTamanho();
    }

    public MaoJogador GetMao()
    {
        return mao;
    }

}
