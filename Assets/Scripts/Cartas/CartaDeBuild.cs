using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CartaDeBuild : CartaDeTesouro
{
    public abstract void EquiparNaBuild(Jogador jogador);
    public abstract bool PodeEquiparNaBuild(Jogador jogador);
}
