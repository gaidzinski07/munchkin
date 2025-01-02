using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Efeito/Modificador de Batalha")]
public class EfeitoDeBatalha : Efeito
{
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

    private bool IsSameCard(Carta internal_carta, Carta carta)
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
        bool isSameClasse = IsSameCard(classe, build.GetClasse());
        bool isSameRaca = IsSameCard(raca, build.GetRaca());
        bool isSameEquipamento = equipamento == null || build.GetEquipamento().Contains(equipamento);

        return (isSameClasse && isSameRaca && isSameEquipamento);
    }

}
