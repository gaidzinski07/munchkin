using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CenaVitoria : MonoBehaviour
{

    public TextMeshProUGUI txtVitoria;
    // Start is called before the first frame update
    void Start()
    {
        txtVitoria.text = "Parab�ns, " + JogadorVitorioso.Jogador.GetNick() + "!";
    }
}
