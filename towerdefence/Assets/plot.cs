using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour // Classe que representa um ponto de construção onde o jogador pode construir uma torre
{
    [SerializeField] private SpriteRenderer sr; // Referência ao SpriteRenderer para alterar a cor do plot ao interagir
    [SerializeField] private Color hoverColor; // Cor de destaque ao passar o mouse sobre o plot
    private GameObject tower; // Torre construída neste plot, se houver
    private Color startColor; // Cor original do plot

    void Start()
    {
        if (sr != null)
        {
            startColor = sr.color; // Armazena a cor original do plot
        }
        else
        {
            Debug.LogError("SpriteRenderer não atribuído!"); // Exibe uma mensagem de erro se o SpriteRenderer não foi configurado
        }
    }

    // Muda a cor do plot para a cor de destaque ao passar o mouse
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    // Retorna a cor do plot para a cor original ao retirar o mouse
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    // Constrói uma torre no plot ao clicar, caso ainda não haja uma torre e o jogador tenha dinheiro suficiente
    private void OnMouseDown()
    {
        if (tower != null) return; // Se já houver uma torre, não faz nada

        Tower towerToBuild = Building.instance.GetSelectedTower(); // Obtém a torre selecionada para construir

        if (towerToBuild.cost > LevelManager.instance.currency) // Verifica se o jogador tem dinheiro suficiente
        {
            Debug.Log("Moeda insuficiente!"); // Mensagem informativa no console
            return;
        }

        LevelManager.instance.SpendCurrency(towerToBuild.cost); // Deduz o custo da torre do total de moeda do jogador
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity); // Instancia a torre no plot
    }

    void Update()
    {
        // Pode ser usado para outras atualizações relacionadas ao plot, se necessário
    }
}
