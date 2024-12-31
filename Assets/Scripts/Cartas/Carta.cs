using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EstadoSelecaoCarta
{
    NONE = 0,
    JOGADOR = 1,
    MONSTRO = 2
}

public abstract class Carta : MonoBehaviour
{
    [SerializeField]
    protected string nome;
    [SerializeField]
    protected string descricao;
    [SerializeField]
    protected List<Efeito> efeitos;
    protected EstadoSelecaoCarta estadoSelecao = EstadoSelecaoCarta.NONE;
    public abstract void ExecutarAcao();

    public string GetNome()
    {
        return nome;
    }
    public void SetNome(string nome)
    {
        this.nome = nome;
    }
    public string GetDescricao()
    {
        return descricao;
    }
    public void SetDescricao(string descricao)
    {
        this.descricao = descricao;
    }

    public List<Efeito> GetEfeitos()
    {
        return efeitos;
    }

    public EstadoSelecaoCarta GetEstadoSelecao()
    {
        return estadoSelecao;
    }

    public override bool Equals(object obj)
    {
        return obj is Carta carta &&
               base.Equals(obj) &&
               nome == carta.nome &&
               descricao == carta.descricao;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), nome, descricao);
    }
}
