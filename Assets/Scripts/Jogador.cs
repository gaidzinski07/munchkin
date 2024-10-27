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
    private Dictionary<ParteDoCorpoEnum, CartaDeEquipamento> equipamentos;
    [SerializeField]
    private List<ParteDoCorpoEnum> partesDoCorpo;
    [SerializeField]
    private Baralho mao;
    [SerializeField]
    private CartaDeMonstroAmigavel pet;
}
