using TMPro;  // Biblioteca necessária para usar o TextMeshPro, que permite textos mais personalizados e de alta qualidade
using UnityEngine;

public class YourClass : MonoBehaviour // Classe que gerencia a interface de moeda e o menu do jogo
{
    [SerializeField] private TextMeshProUGUI currencyUI; // Referência ao elemento UI de texto para exibir a moeda, configurado no editor
    [SerializeField] private Animator anim; // Referência ao componente Animator para controlar animações de interface

    private bool isMenuOpen = true; // Variável para controlar o estado de abertura do menu

    // Método que alterna o estado do menu entre aberto e fechado
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen; // Inverte o valor atual de isMenuOpen
        anim.SetBool("menuOpen", isMenuOpen); // Atualiza o parâmetro "menuOpen" no Animator com o novo estado do menu
    }
}
