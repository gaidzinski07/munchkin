using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batalha : MonoBehaviour
{
    [SerializeField]
    private Jogador jogador;
    [SerializeField]
    private CartaDeMonstro monstro;
    [SerializeField]
    private List<Carta> modificadoresProJogador;
    [SerializeField]
    private List<Carta> modificadoresProMonstro;
    private int nivelJogador, nivelMonstro;
    private Dado dado = new Dado(6);

    //True = jogador ganhou
    //False = jogador perdeu
    public void RealizarBatalha()
    {
        //int baseFuga = dado.Rolar();
        nivelMonstro = CalcularNivelMonstroBatalha();
        nivelJogador = CalcularNivelJogadorBatalha();

        Debug.Log("Resultado da Batalha: " + (nivelJogador >= nivelMonstro));
    }

    //True = jogador fugiu
    //False = jogador nao fugiu
    public void RealizarFuga()
    {
        int nivelFuga = dado.Rolar();
        nivelFuga += CalcularBonusFugaJogador();
        nivelMonstro = monstro.GetLevel();

        Debug.Log("Resultado da Fuga: " + (nivelFuga >= nivelMonstro));
    }

    //Internal Methods

    private int CalcularBonusFugaJogador()
    {
        int bonus = 0;
        List<Efeito> efeitosJogador = GetEfeitosProJogador();
        foreach(Efeito e in efeitosJogador)
        {
            bonus += e.CalculaBonusFuga(jogador.GetBuild());
        }
        return bonus;
    }

    private int CalcularNivelMonstroBatalha()
    {
        List<Efeito> efeitosMonstro = GetEfeitosProMonstro();
        int nivel = monstro.GetLevel();
        foreach (Efeito e in efeitosMonstro)
        {
            nivel += e.CalculaBonusBatalha(jogador.GetBuild());
        }
        return nivel;
    }

    private int CalcularNivelJogadorBatalha()
    {
        List<Efeito> efeitosJogador = GetEfeitosProJogador();
        int nivel = jogador.GetLevel();
        foreach (Efeito e in efeitosJogador)
        {
            nivel += e.CalculaBonusBatalha(jogador.GetBuild());
        }
        return nivel;
    }

    private List<Efeito> GetEfeitosProJogador()
    {
        List<Efeito> efeitosJogador = new List<Efeito>();

        BuildJogador buildJogador = jogador.GetBuild();

        efeitosJogador.AddRange(buildJogador.GetClasse().GetEfeitos());
        efeitosJogador.AddRange(buildJogador.GetRaca().GetEfeitos());

        foreach (CartaDeEquipamento c in buildJogador.GetEquipamento())
        {
            efeitosJogador.AddRange(c.GetEfeitos());
        }

        foreach(Carta c in modificadoresProJogador)
        {
            efeitosJogador.AddRange(c.GetEfeitos());
        }

        return efeitosJogador;
    }


    private List<Efeito> GetEfeitosProMonstro()
    {
        List<Efeito> efeitosMonstro = new List<Efeito>();

        efeitosMonstro.AddRange(monstro.GetEfeitos());

        foreach (Carta c in modificadoresProMonstro)
        {
            efeitosMonstro.AddRange(c.GetEfeitos());
        }

        return efeitosMonstro;
    }


}
