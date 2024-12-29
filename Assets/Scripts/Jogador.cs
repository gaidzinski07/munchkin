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
    private Baralho mao;

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
        if (mao != null) mao.esvaziar();
    }

    public void AlterarNivel(int niveis)
    {
        level += niveis;
        if(level < 1)
        {

        }
    }

    public void VenderCarta()
    {

    }

}
