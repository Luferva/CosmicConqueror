using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoPowerUpDisparoDuplo : EfeitoPowerUp
{
    public override void Aplicar(NaveJogador jogador)
    {
        jogador.EquiparArmaDisparoDuplo();
    }
}
