using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour // Classe respons�vel pelo gerenciamento do n�vel do jogo
{
    public static LevelManager instance; // Inst�ncia �nica da classe LevelManager (singleton)
    public Transform startpoint; // Ponto inicial onde os inimigos aparecem
    public Transform[] path; // Array que cont�m os pontos do caminho que os inimigos seguem

    public int currency; // Quantidade de moeda dispon�vel para o jogador

    private void Awake()
    {
        instance = this; // Atribui a inst�ncia atual � vari�vel est�tica
    }

    private void Start()
    {
        currency = 100; // Inicializa a quantidade de moeda com 100 no in�cio do jogo
    }

    // M�todo para aumentar a quantidade de moeda
    public void IncreaseCurrency(int amount)
    {
        currency += amount; // Adiciona a quantidade especificada � moeda atual
    }

    // M�todo para gastar uma quantidade de moeda
    public bool SpendCurrency(int amount)
    {
        // Verifica se h� moeda suficiente para gastar
        if (amount <= currency)
        {
            currency -= amount; // Deduz a quantidade especificada da moeda atual
            return true; // Retorna verdadeiro se a transa��o foi bem-sucedida
        }
        else
        {
            return false; // Retorna falso se n�o houver moeda suficiente
        }
    }
}
