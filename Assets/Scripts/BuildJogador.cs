using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildJogador
{
    [SerializeField]
    private int numClasses = 1, numRacas = 1, numEquipamentoGrande = 1;
    [SerializeField]
    private CartaDeClasse classe;
    [SerializeField]
    private CartaDeRaca raca;
    [SerializeField]
    private List<CartaDeEquipamento> equipamento;
    [SerializeField]
    private GeneroEnum genero;
    [SerializeField]
    private GameEvent evntAtualizarJogador;

    public BuildJogador() { }

    public BuildJogador(CartaDeClasse classe, CartaDeRaca raca, List<CartaDeEquipamento> equipamento, GeneroEnum genero)
    {
        this.classe = classe;
        this.raca = raca;
        this.equipamento = equipamento;
        this.genero = genero;
    }

    public void SetClasse(CartaDeClasse classe)
    {
        this.classe = classe;
        evntAtualizarJogador.Raise(null, null);
    }

    public void SetRaca(CartaDeRaca raca)
    {
        this.raca = raca;
        evntAtualizarJogador.Raise(null, null);
    }

    public void AddEquipamento(CartaDeEquipamento equipamento)
    {
        this.equipamento.Add(equipamento);
        evntAtualizarJogador.Raise(null, null);
    }

    public CartaDeEquipamento RemoverEquipamento(CartaDeEquipamento equipamento)
    {
        CartaDeEquipamento c = this.equipamento.Remove(equipamento) ? equipamento : null;
        evntAtualizarJogador.Raise(null, null);
        return c;
    }

    public CartaDeClasse GetClasse()
    {
        return classe;
    }

    internal void SetGenero(GeneroEnum genero)
    {
        this.genero = genero;
        evntAtualizarJogador.Raise(null, null);
    }

    public CartaDeRaca GetRaca()
    {
        return raca;
    }

    public List<CartaDeEquipamento> GetEquipamento()
    {
        return equipamento;
    }

    public GeneroEnum GetGenero()
    {
        return genero;
    }

}
