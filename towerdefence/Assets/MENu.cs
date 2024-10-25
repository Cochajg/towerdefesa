using TMPro;  // Biblioteca necess�ria para usar o TextMeshPro, que permite textos mais personalizados e de alta qualidade
using UnityEngine;

public class YourClass : MonoBehaviour // Classe que gerencia a interface de moeda e o menu do jogo
{
    [SerializeField] private TextMeshProUGUI currencyUI; // Refer�ncia ao elemento UI de texto para exibir a moeda, configurado no editor
    [SerializeField] private Animator anim; // Refer�ncia ao componente Animator para controlar anima��es de interface

    private bool isMenuOpen = true; // Vari�vel para controlar o estado de abertura do menu

    // M�todo que alterna o estado do menu entre aberto e fechado
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen; // Inverte o valor atual de isMenuOpen
        anim.SetBool("menuOpen", isMenuOpen); // Atualiza o par�metro "menuOpen" no Animator com o novo estado do menu
    }
}
