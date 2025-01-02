using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDeMonstro : Carta
{
    [Header("Informações para a Batalha")]
    [Range(1, 9), SerializeField]
    private int level;
    [SerializeField]
    private string recompensa;
    [SerializeField]
    private GameObject batalha;
    [SerializeField]
    private List<EfeitoDeBuild> efeitosVitoriaJogador;
    [SerializeField]
    private List<EfeitoDeBuild> efeitosDerrotaJogador;
    public override void ExecutarAcao()
    {
        Instantiate(batalha, transform.parent.parent);
    }

    public int GetLevel()
    {
        return level;
    }

    public List<EfeitoDeBuild> GetEfeitosVitoriaJogador()
    {
        return efeitosVitoriaJogador;
    }

    public List<EfeitoDeBuild> GetEfeitosDerrotaJogador()
    {
        return efeitosDerrotaJogador;
    }

}
