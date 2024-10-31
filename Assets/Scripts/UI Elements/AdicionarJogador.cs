using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdicionarJogador : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputNick;

    public void createPlayer()
    {
        Jogo jogo = FindObjectOfType<Jogo>();
        jogo.adicionarJogador(inputNick.text);
        inputNick.text = "";
    }
}
