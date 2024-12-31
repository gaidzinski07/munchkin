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
    [SerializeField]
    private GameObject batalha;
    public override void ExecutarAcao()
    {
        Instantiate(batalha, transform.parent.parent);
    }

    public int GetLevel()
    {
        return level;
    }
}
