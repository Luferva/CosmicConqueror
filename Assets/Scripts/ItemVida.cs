using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVida : MonoBehaviour
{
    [SerializeField]
    private int quantidadeVidas;
    [SerializeField]
    private ParticleSystem particulePrefab;

    public int QuantidadeVidas {
        get {
            return this.quantidadeVidas;
        }
    }

    public void Coletar() {
        ParticleSystem particula = Instantiate(this.particulePrefab, this.transform.position, Quaternion.identity);
        Destroy(particula.gameObject, 1f);
        Destroy(this.gameObject);
    }
}
