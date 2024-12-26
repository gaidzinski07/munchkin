using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject screenPreGame;
    [SerializeField]
    private GameObject screenInGame;

    private void Start()
    {
        screenPreGame.SetActive(true);
        screenInGame.SetActive(false);
    }

    public void IniciarJogo()
    {
        screenPreGame.SetActive(false);
        screenInGame.SetActive(true);
    }

}
