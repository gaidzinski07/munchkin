using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BatalhaDTO
{
    public Jogador jogador;
    public CartaDeMonstro cartaDeMonstro;
    public List<Carta> modificadoresProJogador;
    public List<Carta> modificadoresProMonstro;
}

public class Batalha : MonoBehaviour
{
    private Jogador jogador;
    private CartaDeMonstro monstro;
    private List<Carta> modificadoresProJogador = new List<Carta>();
    private List<Carta> modificadoresProMonstro = new List<Carta>();
    private int nivelJogador, nivelMonstro;
    private Dado dado = new Dado(6);
    [SerializeField]
    private TextMeshProUGUI txtLogBatalha;
    bool isFuga = false;

    private void Start()
    {
        Jogo jogo = Jogo.Instance;

        BatalhaDTO dto = jogo.GetBatalhaDTO();

        jogador = dto.jogador;
        monstro = dto.cartaDeMonstro;
        modificadoresProJogador = dto.modificadoresProJogador;
        modificadoresProMonstro = dto.modificadoresProMonstro;
        Debug.Log(dto.modificadoresProMonstro.Count + " modificadores do monstro");
    }

    //True = jogador ganhou
    //False = jogador perdeu
    public void RealizarBatalha()
    {
        txtLogBatalha.text = "Iniciando...\n";
        nivelMonstro = CalcularNivelMonstroBatalha();
        nivelJogador = CalcularNivelJogadorBatalha();

        StartCoroutine(ReportBatalha(nivelJogador >= nivelMonstro));
    }

    //True = jogador fugiu
    //False = jogador nao fugiu
    public void RealizarFuga()
    {
        isFuga = true;
        txtLogBatalha.text = "Iniciando...\n";
        nivelJogador = dado.Rolar();
        txtLogBatalha.text += "Dado rolado: " + nivelJogador + "\n";
        nivelJogador += CalcularBonusFugaJogador();
        nivelMonstro = monstro.GetLevel();
        StartCoroutine(ReportBatalha(nivelJogador >= nivelMonstro));
    }

    //Internal Methods

    private IEnumerator ReportBatalha(bool resultado)
    {
        txtLogBatalha.text = "Calculando níveis...\n";
        yield return new WaitForSeconds(0.5f);
        txtLogBatalha.text += "Nível do Monstro: " + nivelMonstro + "\n";
        yield return new WaitForSeconds(0.5f);
        txtLogBatalha.text += "Nível do Jogador: " + nivelJogador + "\n";
        txtLogBatalha.text += resultado ? "Sucesso!" : "Fracasso...";
    }

    public void ExecutarResultado()
    {
        bool resultado = nivelJogador >= nivelMonstro;

        if(!(isFuga && resultado))
        {
            List<EfeitoDeBuild> efeitos = resultado ? monstro.GetEfeitosVitoriaJogador() : monstro.GetEfeitosDerrotaJogador();

            foreach (EfeitoDeBuild e in efeitos) 
            {
                e.AplicarEfeitoAoJogador(jogador);
            }
        }
        Jogo.Instance.FinalizarTurno();
        Destroy(gameObject);
    }

    private int CalcularBonusFugaJogador()
    {
        int bonus = 0;
        List<EfeitoDeBatalha> efeitosJogador = GetEfeitosProJogador();
        foreach(EfeitoDeBatalha e in efeitosJogador)
        {
            bonus += e.CalculaBonusFuga(jogador.GetBuild());
        }
        return bonus;
    }

    private int CalcularNivelMonstroBatalha()
    {
        List<EfeitoDeBatalha> efeitosMonstro = GetEfeitosProMonstro();
        int nivel = monstro.GetLevel();
        foreach (EfeitoDeBatalha e in efeitosMonstro)
        {
            nivel += e.CalculaBonusBatalha(jogador.GetBuild());
        }
        return nivel;
    }

    private int CalcularNivelJogadorBatalha()
    {
        List<EfeitoDeBatalha> efeitosJogador = GetEfeitosProJogador();
        int nivel = jogador.GetLevel();
        foreach (EfeitoDeBatalha e in efeitosJogador)
        {
            nivel += e.CalculaBonusBatalha(jogador.GetBuild());
        }
        return nivel;
    }

    private List<EfeitoDeBatalha> GetEfeitosProJogador()
    {
        List<EfeitoDeBatalha> efeitosJogador = new List<EfeitoDeBatalha>();

        BuildJogador buildJogador = jogador.GetBuild();

        if(buildJogador.GetClasse() != null)
        {
            List<EfeitoDeBatalha> efeitosdeBatalhaClasse = GetEfeitosDeBatalhaEmListaDeEfeitos(buildJogador.GetClasse().GetEfeitos());
            efeitosJogador.AddRange(efeitosdeBatalhaClasse);
        }
        if(buildJogador.GetRaca() != null)
        {
            List<EfeitoDeBatalha> efeitosdeBatalhaRaca = GetEfeitosDeBatalhaEmListaDeEfeitos(buildJogador.GetRaca().GetEfeitos());
            efeitosJogador.AddRange(efeitosdeBatalhaRaca);
        }

        if(buildJogador.GetEquipamento() != null)
        {
            foreach (CartaDeEquipamento c in buildJogador.GetEquipamento())
            {
                List<EfeitoDeBatalha> efeitosdeBatalhaEquipamento = GetEfeitosDeBatalhaEmListaDeEfeitos(c.GetEfeitos());
                efeitosJogador.AddRange(efeitosdeBatalhaEquipamento);
            }
        }

        if(modificadoresProJogador != null && modificadoresProJogador.Count > 0)
        {
            foreach(Carta c in modificadoresProJogador)
            {
                List<EfeitoDeBatalha> efeitosdeBatalhaModificadores = GetEfeitosDeBatalhaEmListaDeEfeitos(c.GetEfeitos());
                efeitosJogador.AddRange(efeitosdeBatalhaModificadores);
            }
        }

        return efeitosJogador;
    }


    private List<EfeitoDeBatalha> GetEfeitosProMonstro()
    {
        List<EfeitoDeBatalha> efeitosMonstro = new List<EfeitoDeBatalha>();

        List<EfeitoDeBatalha> efeitosdeBatalhaMonstro = GetEfeitosDeBatalhaEmListaDeEfeitos(monstro.GetEfeitos());
        efeitosMonstro.AddRange(efeitosdeBatalhaMonstro);

        if(modificadoresProMonstro != null && modificadoresProMonstro.Count > 0)
        {
            foreach (Carta c in modificadoresProMonstro)
            {
                List<EfeitoDeBatalha> efeitosdeBatalhaModificadores = GetEfeitosDeBatalhaEmListaDeEfeitos(c.GetEfeitos());
                efeitosMonstro.AddRange(efeitosdeBatalhaModificadores);
            }
        }

        return efeitosMonstro;
    }

    private List<EfeitoDeBatalha> GetEfeitosDeBatalhaEmListaDeEfeitos(List<Efeito> list)
    {
        List<EfeitoDeBatalha> result = new List<EfeitoDeBatalha>();
        foreach (Efeito e in list)
        {
            if (e.GetType() == typeof(EfeitoDeBatalha))
            {
                Debug.Log("Efeito encontrado");
                result.Add((EfeitoDeBatalha)e);
            }
        }
        return result;
    }


}
