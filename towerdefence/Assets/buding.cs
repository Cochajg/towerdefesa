using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour // Classe que gerencia a seleção e configuração das torres no jogo
{
    public static Building instance; // Instância estática da classe para permitir acesso global à seleção de torres
    [SerializeField] private Tower[] towers; // Array de torres disponíveis para seleção, configurado no editor

    private int selectedTower = 0; // Índice da torre selecionada atualmente

    // Retorna a torre atualmente selecionada
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    // Define a torre selecionada, recebendo o índice da torre
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    // Chamado ao inicializar o objeto, define a instância única da classe
    private void Awake()
    {
        instance = this;
    }

   
    void Start()
    {
        
    }

   
    void Update()
    {
       
    }
}
