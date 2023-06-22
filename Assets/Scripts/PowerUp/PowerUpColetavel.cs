using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpColetavel : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float intervaloTempoAntesAutodestruir;

    [SerializeField]
    private int quantidadeTotalPiscadas;

    [SerializeField]
    private float intervaloTempoEntrePiscadas;

    [SerializeField]
    private float reducaoTempoPiscadas;

    private float contagemTempoAntesAutodestruir;
    private bool autodestruindo;

    public abstract EfeitoPowerUp EfeitoPowerUp { get;}

    public void Start()
    {
        this.autodestruindo = false;
        this.contagemTempoAntesAutodestruir = 0;
    }

    public void Update()
    {
        if (!this.autodestruindo)
        {
            this.contagemTempoAntesAutodestruir += Time.deltaTime;
            if (this.contagemTempoAntesAutodestruir >= this.intervaloTempoAntesAutodestruir){
                IniciarAutodestruicao();
            }
        }
    }

    public void Coletar()
    {
        Destroy(this.gameObject);
    }

    private void IniciarAutodestruicao()
    {
        this.autodestruindo = true;

        StartCoroutine(Autodestruir());
    }

    private IEnumerator Autodestruir()
    {
        int contadorPiscadas = 0;
        do
        {
            this.spriteRenderer.enabled = !this.spriteRenderer.enabled;

            yield return new WaitForSeconds(this.intervaloTempoEntrePiscadas);
            contadorPiscadas++;
            this.intervaloTempoEntrePiscadas -= contadorPiscadas * this.reducaoTempoPiscadas;
        } while (contadorPiscadas < this.quantidadeTotalPiscadas);
        Destroy(this.gameObject);
    }
}
