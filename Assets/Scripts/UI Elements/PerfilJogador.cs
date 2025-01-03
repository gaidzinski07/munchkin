using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PerfilJogador : MonoBehaviour
{
    [SerializeField]
    private GameObject maleIcon;
    [SerializeField]
    private GameObject femaleIcon;
    [SerializeField]
    private TextMeshProUGUI nickText;
    [SerializeField]
    private TextMeshProUGUI nivelText;
    [SerializeField]
    private TextMeshProUGUI moedasText;
    [SerializeField]
    private List<Transform> posMaoJogador;
    private Jogador jogador;
    List<Carta> ultimaMao;
    [SerializeField]
    private GameObject objPonteiro;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(jogador != null)
        {
            AtualizarJogador();
        }
    }

    public void SetJogador(Jogador jogador)
    {
        if (jogador == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        this.jogador = jogador;
        AtualizarJogador();
    }

    public void AtualizarJogador()
    {
        maleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.MASCULINO);
        femaleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.FEMININO);
        nickText.text = this.jogador.GetNick();
        nivelText.text = this.jogador.GetLevel() + "";
        moedasText.text = this.jogador.GetMoedas() + "";

        if (ultimaMao != jogador.GetMao().GetCartas())
        {
            ultimaMao = jogador.GetMao().GetCartas();

            if (ultimaMao != null && ultimaMao.Count > 0)
            {
                int i = 0;
                foreach (Carta carta in ultimaMao)
                {
                    carta.transform.DOMove(posMaoJogador[i++].position, .5f);
                }
            }
        }
    }

    public void OnIniciarTurno(bool ehMinhaVez)
    {
        objPonteiro.SetActive(ehMinhaVez);
    }

    public bool EhMeuJogador(Jogador jogador)
    {
        return ((jogador != null && this.jogador != null) && jogador.GetInstanceID() == this.jogador.GetInstanceID());
    }

}
