using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdicionarJogador : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputNick;
    [SerializeField]
    private TMP_Dropdown dropDownGenero;
    [SerializeField]
    private GameObject btnStart;
    [SerializeField]
    private TextMeshProUGUI txtListaJogadores;

    public void createPlayer()
    {
        Jogo.Instance.AdicionarJogador(inputNick.text, (GeneroEnum) dropDownGenero.value);
        inputNick.text = "";
        btnStart.SetActive(Jogo.Instance.GetPodeIniciar());
        AtualizarListaJogadores();
    }

    public void CreateBot()
    {
        Jogo.Instance.AdicionarBot(inputNick.text, (GeneroEnum)dropDownGenero.value);
        inputNick.text = "";
        btnStart.SetActive(Jogo.Instance.GetPodeIniciar());
        AtualizarListaJogadores();
    }

    private void AtualizarListaJogadores()
    {
        string lista = "";
        List<Jogador> jogs = Jogo.Instance.GetJogadores();
        for(int i = 0; i < jogs.Count; i++)
        {
            lista += jogs[i].GetNick() + "\n";
        }
        txtListaJogadores.text = lista;
    }
}
