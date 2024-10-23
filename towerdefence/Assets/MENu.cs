using TMPro;  // Certifique-se de incluir essa linha para usar TextMeshPro
using UnityEngine;

public class YourClass : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("menuOpen", isMenuOpen);  // Corrigido "SetBool" e variável correta
    }
}
