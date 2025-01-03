using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum EstadoSelecaoCarta
{
    NONE = 0,
    JOGADOR = 1,
    MONSTRO = 2,
    TROCA = 3,
    VENDA = 4
}
[Serializable]
public class CorEstadoSelecao
{
    public Color cor { get; private set; }
    public EstadoSelecaoCarta estado { get; private set; }

    private CorEstadoSelecao(Color cor, EstadoSelecaoCarta estado)
    {
        this.cor = cor;
        this.estado = estado;
    }

    public static CorEstadoSelecao NONE = new CorEstadoSelecao(new Color(0, 0, 0, 0), EstadoSelecaoCarta.NONE);
    public static CorEstadoSelecao JOGADOR = new CorEstadoSelecao(Color.green, EstadoSelecaoCarta.JOGADOR);
    public static CorEstadoSelecao MONSTRO = new CorEstadoSelecao(Color.red, EstadoSelecaoCarta.MONSTRO);
    public static CorEstadoSelecao TROCA = new CorEstadoSelecao(Color.cyan, EstadoSelecaoCarta.TROCA);
    public static CorEstadoSelecao VENDA = new CorEstadoSelecao(Color.blue, EstadoSelecaoCarta.VENDA);


    public readonly static List<CorEstadoSelecao> TODOS_ESTADOS = new List<CorEstadoSelecao> { NONE, JOGADOR, MONSTRO, TROCA, VENDA };

}

public abstract class Carta : MonoBehaviour
{
    [SerializeField]
    protected string nome;
    [SerializeField]
    protected string descricao;
    [SerializeField]
    protected List<Efeito> efeitos;
    [Header("Interação")]
    [SerializeField]
    private GameObject contextMenu;
    [SerializeField]
    private Image imgOutline;
    protected bool selecionavel;
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

    public void SetSelecionavel(bool selecionavel)
    {
        this.selecionavel = selecionavel;
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

    public virtual void OnClick()
    {
        if (selecionavel)
        {
            contextMenu.SetActive(!contextMenu.activeSelf);
        }
    }

    public void CloseContextMenu()
    {
        contextMenu.SetActive(false);
    }

    public void SetEstado(int estado)
    {
        estadoSelecao = (EstadoSelecaoCarta) estado;
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        foreach(CorEstadoSelecao c in CorEstadoSelecao.TODOS_ESTADOS)
        {
            if(c.estado == estadoSelecao)
            {
                imgOutline.color = c.cor;
            }
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), nome, descricao);
    }
}
