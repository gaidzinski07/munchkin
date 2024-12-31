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

    public void CreatePlayer(string nick, GeneroEnum genero)
    {
        this.nick = nick;
        this.build.SetGenero(genero);
    }
    public string GetNick()
    {
        return this.nick;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public BuildJogador GetBuild()
    {
        return build;
    }

    public void ResetPlayer()
    {
        level = 0;
        moedas = 0;
        this.build = new BuildJogador();
        if (mao != null) mao.Esvaziar();
    }

    public void AlterarNivel(int niveis)
    {
        level += niveis;
        if(level < 1)
        {
            Debug.Log("Você morreu " + nick);
        }
        else if(level >= 10)
        {
            Debug.Log("Você ganhou " + nick);
        }
    }

    public void ReceberMoedas(int moedas)
    {
        if (moedas <= 0) return;
        this.moedas += moedas;
        if(moedas >= 1000)
        {
            AlterarNivel(1);
            moedas = 0;
            Debug.Log("Subi de nível " + nick);
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
