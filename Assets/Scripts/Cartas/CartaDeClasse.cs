using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeClasse : CartaDeBuild
{
    public override void EquiparNaBuild(Jogador jogador)
    {
        jogador.GetBuild().SetClasse(this);
    }

    public override bool PodeEquiparNaBuild(Jogador jogador)
    {
        return jogador.GetBuild().PodeEquiparNovaClasse();
    }

    public override void ExecutarAcao()
    {
        throw new System.NotImplementedException();
    }
}
