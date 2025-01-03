using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildJogador
{
    [SerializeField]
    private int numClasses = 1, numRacas = 1, numEquipamentoGrande = 1, numEquipamentoPequeno = 3;
    [SerializeField]
    private CartaDeClasse classe;
    [SerializeField]
    private CartaDeRaca raca;
    [SerializeField]
    private List<CartaDeEquipamento> equipamento = new List<CartaDeEquipamento>();
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

    public void SetGenero(GeneroEnum genero)
    {
        this.genero = genero;
    }

    public void SetClasse(CartaDeClasse classe)
    {
        if (PodeEquiparNovaClasse())
        {
            this.classe = classe;
            classe.gameObject.SetActive(false);
        }
    }

    public void SetRaca(CartaDeRaca raca)
    {
        if (PodeEquiparNovaRaca())
        {
            this.raca = raca;
            raca.gameObject.SetActive(false);
        }
    }

    public void AddEquipamento(CartaDeEquipamento equipamento)
    {
        if (PodeAdicionarNovoEquipamento(equipamento.GetTamanho()))
        {
            this.equipamento.Add(equipamento);
            equipamento.gameObject.SetActive(false);
        }
    }

    public CartaDeEquipamento RemoverEquipamento(CartaDeEquipamento equipamento)
    {
        CartaDeEquipamento c = this.equipamento.Remove(equipamento) ? equipamento : null;
        return c;
    }

    public CartaDeClasse GetClasse()
    {
        return classe;
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

    public bool PodeEquiparNovaClasse()
    {
        return classe == null;
    }

    public bool PodeEquiparNovaRaca()
    {
        return raca == null;
    }

    public bool PodeAdicionarNovoEquipamento(TamanhoEnum tamanho)
    {
        int cont = 0;
        if (equipamento != null && equipamento.Count > 0)
        {
            foreach (CartaDeEquipamento c in equipamento)
            {
                if (c.GetTamanho() == tamanho)
                {
                    cont++;
                }
            }
        }
        return cont < (tamanho == TamanhoEnum.PEQUENO ? numEquipamentoPequeno : numEquipamentoGrande);
    }

}
