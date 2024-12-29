using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carta : MonoBehaviour
{
    [SerializeField]
    private string nome;
    [SerializeField]
    private string descricao;
    [SerializeField]
    private List<Efeito> efeitos;    
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
