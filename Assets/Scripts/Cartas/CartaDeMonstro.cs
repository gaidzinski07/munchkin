using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeMonstro : Carta
{
    [Range(1, 9), SerializeField]
    private int level;
    [SerializeField]
    private string recompensa;
    [SerializeField]
    private int niveisRetiradosDoJogador = 1;
    public override void ExecutarAcao()
    {
        throw new System.NotImplementedException();
    }

    public int GetLevel()
    {
        return level;
    }
}
