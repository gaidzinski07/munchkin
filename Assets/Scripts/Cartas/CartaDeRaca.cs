using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeRaca : CartaDeBuild
{
    public override void EquiparNaBuild(Jogador jogador)
    {
        jogador.GetBuild().SetRaca(this);
    }

    public override bool PodeEquiparNaBuild(Jogador jogador)
    {
        return jogador.GetBuild().PodeEquiparNovaRaca();
    }

    public override void ExecutarAcao()
    {
        throw new System.NotImplementedException();
    }
}
