using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour // Classe responsável pelo gerenciamento do nível do jogo
{
    public static LevelManager instance; // Instância única da classe LevelManager (singleton)
    public Transform startpoint; // Ponto inicial onde os inimigos aparecem
    public Transform[] path; // Array que contém os pontos do caminho que os inimigos seguem

    public int currency; // Quantidade de moeda disponível para o jogador

    private void Awake()
    {
        instance = this; // Atribui a instância atual à variável estática
    }

    private void Start()
    {
        currency = 100; // Inicializa a quantidade de moeda com 100 no início do jogo
    }

    // Método para aumentar a quantidade de moeda
    public void IncreaseCurrency(int amount)
    {
        currency += amount; // Adiciona a quantidade especificada à moeda atual
    }

    // Método para gastar uma quantidade de moeda
    public bool SpendCurrency(int amount)
    {
        // Verifica se há moeda suficiente para gastar
        if (amount <= currency)
        {
            currency -= amount; // Deduz a quantidade especificada da moeda atual
            return true; // Retorna verdadeiro se a transação foi bem-sucedida
        }
        else
        {
            return false; // Retorna falso se não houver moeda suficiente
        }
    }
}
