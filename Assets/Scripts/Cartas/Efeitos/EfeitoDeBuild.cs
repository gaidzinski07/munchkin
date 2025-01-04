using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AtributoBuildEnum
{
    NONE = 0,
    RACA = 1,
    CLASSE = 2,
    EQUIPAMENTO = 3,
    GENERO = 4,
    NIVEL = 5
}

[CreateAssetMenu(menuName = "Efeito/Modificador de Build")]
public class EfeitoDeBuild : Efeito
{
    [SerializeField]
    private AtributoBuildEnum atributoAfetado;

    public override void AplicarEfeitoAoJogador(Jogador jogador)
    {
        if (jogador == null) return;
        BuildJogador build = jogador.GetBuild();
        switch (atributoAfetado)
        {
            case AtributoBuildEnum.RACA:
                DevolverCarta(build.GetRaca());
                build.SetRaca(null);
                break;
            case AtributoBuildEnum.CLASSE:
                DevolverCarta(build.GetClasse());
                build.SetClasse(null);
                break;
            case AtributoBuildEnum.EQUIPAMENTO:
                ZerarEquipamento(build);
                break;
            case AtributoBuildEnum.GENERO:
                build.SetGenero(build.GetGenero() == GeneroEnum.FEMININO ? GeneroEnum.MASCULINO : GeneroEnum.FEMININO);
                break;
            case AtributoBuildEnum.NIVEL:
                jogador.AlterarNivel(alteracaoNivel);
                break;
            default:
                break;
        }
    }

    private void DevolverCarta(Carta carta)
    {
        if (carta == null) return;
        Jogo jogo = Jogo.Instance;
        jogo.DevolverCartaParaBaralhoDeTesouro(carta);
    }

    private void ZerarEquipamento(BuildJogador build)
    {
        if(build.GetEquipamento() != null && build.GetEquipamento().Count > 0)
        {
            foreach(CartaDeEquipamento c in build.GetEquipamento())
            {
                DevolverCarta(build.RemoverEquipamento(c));
            }
        }
    }

}
