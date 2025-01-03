using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeEquipamento : CartaDeBuild
{
    [SerializeField, Range(0,2)]
    private int numMaos;
    [SerializeField]
    private TamanhoEnum tamanho;

    public TamanhoEnum GetTamanho()
    {
        return tamanho;
    }

    public override void EquiparNaBuild(Jogador jogador)
    {
        jogador.GetBuild().AddEquipamento(this);
    }

    public override bool PodeEquiparNaBuild(Jogador jogador)
    {
        return jogador.GetBuild().PodeAdicionarNovoEquipamento(tamanho);
    }

    public override void ExecutarAcao()
    {
        Jogo.Instance.EquiparCartaNaBuildDoJogadorDaVez(this);
    }
}
