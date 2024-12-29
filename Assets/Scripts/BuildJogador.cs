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

    public BuildJogador() { }

    public BuildJogador(CartaDeClasse classe, CartaDeRaca raca, List<CartaDeEquipamento> equipamento, GeneroEnum genero)
    {
        this.classe = classe;
        this.raca = raca;
        this.equipamento = equipamento;
        this.genero = genero;
    }

    public CartaDeClasse GetClasse()
    {
        return classe;
    }

    internal void SetGenero(GeneroEnum genero)
    {
        this.genero = genero;
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
