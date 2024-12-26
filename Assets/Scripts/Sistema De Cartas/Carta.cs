using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carta : MonoBehaviour
{
    [SerializeField]
    private string nome;
    [SerializeField]
    private string descricao;

    public abstract void executarAcao();

    public string getNome()
    {
        return nome;
    }
    public void setNome(string nome)
    {
        this.nome = nome;
    }
    public string getDescricao()
    {
        return descricao;
    }
    public void setDescricao(string descricao)
    {
        this.descricao = descricao;
    }
}
