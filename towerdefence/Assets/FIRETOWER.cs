using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretFire : Turret // Classe especializada de torre que aplica dano de queimadura aos inimigos
{
    [SerializeField] private float burnDuration = 3f; // Dura��o do efeito de queimadura em segundos
    [SerializeField] private float burnDamagePerSecond = 1f; // Dano causado por segundo enquanto o inimigo est� queimando

    // M�todo que ataca o inimigo aplicando o efeito de queimadura
    public override void Atacar()
    {
        if (target != null) // Verifica se h� um alvo designado
        {
            Health enemyHealth = target.GetComponent<Health>(); // Obt�m o componente de sa�de do inimigo

            if (enemyHealth != null) // Verifica se o inimigo possui o componente de sa�de
            {
                StartCoroutine(ApplyBurnDamage(enemyHealth)); // Inicia a aplica��o do dano de queimadura
            }
        }
    }

    // Coroutine que aplica o dano de queimadura ao longo do tempo
    private IEnumerator ApplyBurnDamage(Health enemyHealth)
    {
        float elapsedTime = 0f; // Tempo decorrido desde o in�cio do efeito de queimadura

        while (elapsedTime < burnDuration) // Continua aplicando dano at� o fim da dura��o da queimadura
        {
            enemyHealth.TakeDamage(burnDamagePerSecond * Time.deltaTime); // Aplica dano ao inimigo a cada frame
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido
            yield return null; // Espera at� o pr�ximo frame para continuar
        }
    }

    // M�todo de disparo que cria e dispara o proj�til e aplica o efeito de queimadura ao alvo
    protected override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Instancia um proj�til na posi��o de disparo
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Obt�m o script do proj�til
        bulletScript.SetTarget(target); // Define o alvo do proj�til
        Atacar(); // Aplica o ataque (efeito de queimadura) ao inimigo
    }
}
