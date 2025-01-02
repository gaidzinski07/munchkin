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

    private void Awake()
    {
        gameObject.SetActive(false);
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
        maleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.MASCULINO);
        femaleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.FEMININO);
        nickText.text = this.jogador.GetNick();
        nivelText.text = this.jogador.GetLevel() + "";
        moedasText.text = this.jogador.GetMoedas() + "";

        List<Carta> mao = jogador.GetMao().GetCartas();

        if(mao != null && mao.Count > 0)
        {
            int i = 0;
            foreach(Carta carta in mao)
            {
                //carta.transform.position = posMaoJogador[i++].position;
                carta.transform.DOMove(posMaoJogador[i++].position, .5f);
            }
        }

    }

    public void AtualizarJogador(Component sender, object data)
    {
        maleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.MASCULINO);
        femaleIcon.SetActive(this.jogador.GetBuild().GetGenero() == GeneroEnum.FEMININO);
        nickText.text = this.jogador.GetNick();
        nivelText.text = this.jogador.GetLevel() + "";
        moedasText.text = this.jogador.GetMoedas() + "";

        List<Carta> mao = jogador.GetMao().GetCartas();

        if (mao != null && mao.Count > 0)
        {
            int i = 0;
            foreach (Carta carta in mao)
            {
                //carta.transform.position = posMaoJogador[i++].position;
                carta.transform.DOMove(posMaoJogador[i++].position, .5f);
            }
        }
    }

    public bool EhMeuJogador(Jogador jogador)
    {
        return (jogador != null && jogador.Equals(this.jogador));
    }

}
