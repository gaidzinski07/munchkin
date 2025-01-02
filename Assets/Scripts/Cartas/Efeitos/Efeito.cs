using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Efeito : ScriptableObject
{
    [Header("Validade do Efeito na build:")]
    [SerializeField]
    protected CartaDeClasse classe;
    [SerializeField]
    protected CartaDeRaca raca;
    [SerializeField]
    protected CartaDeEquipamento equipamento;
    [Header("Altera��o de n�vel:")]
    [SerializeField]
    protected int alteracaoNivel;
}
