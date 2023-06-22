using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimJogo : MonoBehaviour
{
    public Text textoPontuacao;
    public Text textoMelhorPontuacao;

    public void Exibir() {
        this.gameObject.SetActive(true);
        this.textoPontuacao.text = (ControladorPontuacao.Pontuacao + "p");
        this.textoMelhorPontuacao.text = (ControladorPontuacao.MelhorPontuacao + "p");
        // Pausar o jogo
        Time.timeScale = 0;
    }

    public void Esconder() {
        this.gameObject.SetActive(false);
    }
    public void TentarNovamente() {
        //Continuar jogo
        Time.timeScale = 1;
        SceneManager.LoadScene("Fase01");
    }

}
