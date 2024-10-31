using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField]
    private string nick;
    [SerializeField, Range(0, 10)]
    private int level;
    [SerializeField, Range(0, 1000)]
    private int moedas;
    [SerializeField]
    private CartaDeClasse classe;
    [SerializeField]
    private CartaDeRaca raca;
    [SerializeField]
    private Dictionary<ParteDoCorpoEnum, CartaDeEquipamento> equipamentos = new Dictionary<ParteDoCorpoEnum, CartaDeEquipamento>();
    [SerializeField]
    private List<ParteDoCorpoEnum> partesDoCorpo;
    [SerializeField]
    private Baralho mao;
    [SerializeField]
    private CartaDeMonstroAmigavel pet;

    public void setNick(string nick)
    {
        this.nick = nick;
    }
    public string getNick()
    {
        return this.nick;
    }

    public void resetPlayer()
    {
        level = 0;
        moedas = 0;
        classe = null;
        raca = null;
        equipamentos.Clear();
        if (mao != null) mao.esvaziar();
        pet = null;
    }

}
