using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveJogador : MonoBehaviour
{
    private const int QuantidadeMaximaVidas = 5;

    [SerializeField]
    public Rigidbody2D rigidbody;

    [SerializeField]
    public float velocidadeMovimento;

    [SerializeField]
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private ControladorArma controladorArma;

    private int vidas;
    private FimJogo telaFimJogo;


    void Start()
    {
        ControladorPontuacao.Pontuacao = 0;
        this.vidas = QuantidadeMaximaVidas;

        GameObject fimJogoGameObject = GameObject.FindGameObjectWithTag("TelaFimJogo");
        this.telaFimJogo = fimJogoGameObject.GetComponent<FimJogo>();
        this.telaFimJogo.Esconder();

       EquiparArmaDisparoAlternado();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentação por teclado
        /* float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float velocidadeX = (horizontal * this.velocidadeMovimento);
        float velocidadeY = (vertical * this.velocidadeMovimento);

        this.rigidbody.velocity = new Vector2(velocidadeX, velocidadeY); */

        VerificarLimiteTela();
    }

    public void EquiparArmaDisparoAlternado() {
        this.controladorArma.EquiparArmaDisparoAlternado();
    }

    public void EquiparArmaDisparoDuplo() {
        this.controladorArma.EquiparArmaDisparoDuplo();
    }

    private void VerificarLimiteTela() {
        Vector2 posicaoAtual = this.transform.position;

        float metadeLargura = Largura / 2f;
        float metadeAltura = Altura / 2f;

        Camera camera = Camera.main;
        Vector2 limiteInferiorEsquerdo = camera.ViewportToWorldPoint(Vector2.zero);
        Vector2 limiteSuperiorDireito = camera.ViewportToWorldPoint(Vector2.one);

        float pontoReferenciaEsquerdo = posicaoAtual.x - metadeLargura;
        float pontoReferenciaDireito = posicaoAtual.x + metadeLargura;

        if (pontoReferenciaEsquerdo < limiteInferiorEsquerdo.x) { //Saindo pela esquerda
            this.transform.position = new Vector2(limiteInferiorEsquerdo.x + metadeLargura, posicaoAtual.y);
        } else if (pontoReferenciaDireito > limiteSuperiorDireito.x) { //Saindo pela direita
            this.transform.position = new Vector2(limiteSuperiorDireito.x - metadeLargura, posicaoAtual.y);
        }

        posicaoAtual = this.transform.position;
        
        float pontoReferenciaSuperior = posicaoAtual.y + metadeAltura;
        float pontoReferenciaInferior = posicaoAtual.y - metadeAltura;

        if (pontoReferenciaSuperior > limiteSuperiorDireito.y) { //Saindo por cima
            this.transform.position = new Vector2(posicaoAtual.x, limiteSuperiorDireito.y - metadeAltura);
        } else if (pontoReferenciaInferior < limiteInferiorEsquerdo.y) { //Saindo por baixo
            this.transform.position = new Vector2(posicaoAtual.x, limiteInferiorEsquerdo.y + metadeAltura);
        }


    }

    private float Largura {
        get {
            Bounds bounds = this.spriteRenderer.bounds;
            Vector3 tamanho = bounds.size;
            return tamanho.x;
        }
    }

    private float Altura {
        get {
            Bounds bounds = this.spriteRenderer.bounds;
            Vector3 tamanho = bounds.size;
            return tamanho.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Inimigo")) {
            NaveInimiga inimigo = collider.GetComponent<NaveInimiga>();
            ColidirInimigo(inimigo);
        } else if (collider.CompareTag("ItemVida")) {
            ItemVida itemVida = collider.GetComponent<ItemVida>();
            ColetarItemVida(itemVida);
        } else if (collider.CompareTag("PowerUp")) {
            PowerUpColetavel powerUp = collider.GetComponent<PowerUpColetavel>();
            ColetarPowerUp(powerUp);
        }
    }

    private void ColidirInimigo(NaveInimiga inimigo)
    {
        Vida--;
        inimigo.ReceberDano();
    }

    private void ColetarItemVida(ItemVida itemVida)
    {
        Vida += itemVida.QuantidadeVidas;
        itemVida.Coletar();
    }

    private void ColetarPowerUp(PowerUpColetavel powerUp)
    {
        EfeitoPowerUp efeitoPowerUp = powerUp.EfeitoPowerUp;
        efeitoPowerUp.Aplicar(this);
        powerUp.Coletar();
    }

    public int Vida {
        get {
            return this.vidas;
        }
        set {
            this.vidas = value;
            if (this.vidas > QuantidadeMaximaVidas) {
                this.vidas = QuantidadeMaximaVidas;
            } else if (this.vidas <= 0) {
                this.vidas = 0;
                this.gameObject.SetActive(false);
                telaFimJogo.Exibir();
            }
        }
    }

}
