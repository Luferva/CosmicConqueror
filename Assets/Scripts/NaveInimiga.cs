using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveInimiga : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigidbody;
    public float velocidadeMinima;
    public float velocidadeMaxima;
    public int vidas;
    public ParticleSystem particulaExplosaoPrefab;

    [SerializeField]
    [Range(0, 100)]
    private float chanceSoltarItemVida;

    [SerializeField]
    private ItemVida itemVidaPrefab;

    [SerializeField]
    [Range(0, 100)]
    private float chanceSoltarPowerUp;

    [SerializeField]
    private PowerUpColetavel[] powerUpPrefabs;


    private float velocidadeY;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 posicaoAtual = this.transform.position;
        float metadeLargura = Largura / 2f;

        float pontoReferenciaEsquerda = posicaoAtual.x - metadeLargura;
        float pontoReferenciaDireita = posicaoAtual.x + metadeLargura;

        Camera camera = Camera.main;
        Vector2 limiteInferiorEsquerdo = camera.ViewportToWorldPoint(Vector2.zero);
        Vector2 limiteSuperiorDireito = camera.ViewportToWorldPoint(Vector2.one);

        if (pontoReferenciaEsquerda < limiteInferiorEsquerdo.x) {
            //Inimigo saiu pela esquerda
            float posicaoX = limiteInferiorEsquerdo.x + metadeLargura;
            this.transform.position = new Vector2(posicaoX, posicaoAtual.y);
        } else if (pontoReferenciaDireita > limiteSuperiorDireito.x) {
            //Inimigo saiu pela direita
            float posicaoX = limiteSuperiorDireito.x - metadeLargura;
            this.transform.position = new Vector2(posicaoX, posicaoAtual.y);
        }

        this.velocidadeY = Random.Range(this.velocidadeMinima, this.velocidadeMaxima);
    }

    // Update is called once per frame
    void Update()
    {
        this.rigidbody.velocity = new Vector2(0, -this.velocidadeY);

        Camera camera = Camera.main;
        Vector3 posicaoNaCamera = camera.WorldToViewportPoint(this.transform.position);
        if (posicaoNaCamera.y < 0) {
            //Inimigo saiu da área da camera
            NaveJogador jogador = GameObject.FindGameObjectWithTag("Player").GetComponent<NaveJogador>();
            jogador.Vida--;
            Destruir(false);
        }
    }

    public void ReceberDano() {
        this.vidas--;
        if (this.vidas <= 0) {
            Destruir(true);
        }
    }

    private float Largura {
        get {
            Bounds bounds = this.spriteRenderer.bounds;
            Vector3 tamanho = bounds.size;
            return tamanho.x;
        }
    }

    public void Destruir(bool derrotado) {
        if (derrotado) {
            ControladorPontuacao.Pontuacao += 10;
            SoltarItemVida();
            SoltarPowerUp();
        }
        ParticleSystem particulaExplosao = Instantiate(this.particulaExplosaoPrefab, this.transform.position, Quaternion.identity);
        Destroy(particulaExplosao.gameObject, 1f);
        Destroy(this.gameObject);
    }

    private void SoltarItemVida() {
        float chanceAleatoria = Random.Range(0f, 100f);
        if (chanceAleatoria <= this.chanceSoltarItemVida) {
            Instantiate(this.itemVidaPrefab, this.transform.position, Quaternion.identity);
        }
    }

    private void SoltarPowerUp() {
        float chanceAleatoria = Random.Range(0f, 100f);
        if (chanceAleatoria <= this.chanceSoltarPowerUp) {
            int indiceAleatorioPowerUp = Random.Range(0, this.powerUpPrefabs.Length);
            PowerUpColetavel powerUpPrefab = this.powerUpPrefabs[indiceAleatorioPowerUp];
            Instantiate(powerUpPrefab, this.transform.position, Quaternion.identity);
        } 
    }


}
