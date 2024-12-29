using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Efeito/Modificador de Batalha")]
public class Efeito : ScriptableObject
{
    [Header("Efeito válido contra:")]
    [SerializeField]
    private CartaDeClasse classe;
    [SerializeField]
    private CartaDeRaca raca;
    [SerializeField]
    private CartaDeEquipamento equipamento;

    [Header("Alteração de nível")]
    [SerializeField]
    private int alteracaoNivel;
    [SerializeField]
    private int alteracaoFuga;

    public int CalculaBonusBatalha(BuildJogador build)
    {
        if (CanApplyBonus(build))
        {
            return alteracaoNivel;
        }
        return 0;
    }

    public int CalculaBonusFuga(BuildJogador build)
    {
        if (CanApplyBonus(build))
        {
            return alteracaoFuga;
        }
        return 0;
    }

    private bool isSameCard(Carta internal_carta, Carta carta)
    {
        bool isSame = (internal_carta == null);

        if(!isSame && internal_carta.Equals(carta))
        {
            isSame = true;
        }
        return isSame;
    }

    private bool CanApplyBonus(BuildJogador build)
    {
        bool isSameClasse = isSameCard(classe, build.GetClasse());
        bool isSameRaca = isSameCard(raca, build.GetRaca());
        bool isSameEquipamento = equipamento == null || build.GetEquipamento().Contains(equipamento);

        return (isSameClasse && isSameRaca && isSameEquipamento);
    }

}
