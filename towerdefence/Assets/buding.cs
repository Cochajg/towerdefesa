using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour // Classe que gerencia a sele��o e configura��o das torres no jogo
{
    public static Building instance; // Inst�ncia est�tica da classe para permitir acesso global � sele��o de torres
    [SerializeField] private Tower[] towers; // Array de torres dispon�veis para sele��o, configurado no editor

    private int selectedTower = 0; // �ndice da torre selecionada atualmente

    // Retorna a torre atualmente selecionada
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    // Define a torre selecionada, recebendo o �ndice da torre
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    // Chamado ao inicializar o objeto, define a inst�ncia �nica da classe
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
