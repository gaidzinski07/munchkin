using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeMaldicao : Carta
{
    public override void ExecutarAcao()
    {
        Jogador alvo = Jogo.Instance.GetJogadorDaVez();
        if (alvo == null) return;
        foreach(Efeito e in efeitos)
        {
            if(e.GetType() == typeof(EfeitoDeBuild))
            {
                EfeitoDeBuild efeito = (EfeitoDeBuild)e;
                efeito.AplicarEfeitoAoJogador(alvo);
            }
        }
    }
}
