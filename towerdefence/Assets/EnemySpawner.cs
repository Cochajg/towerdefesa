using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour // Classe respons�vel por gerar inimigos no jogo
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>(); // Array de prefabs de inimigos que podem ser gerados
    [SerializeField] private int baseEnemies = 8; // N�mero base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f; // Taxa de inimigos a serem gerados por segundo
    [SerializeField] private float timeBetweenWaves = 5f; // Tempo entre ondas de inimigos
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escalonamento de dificuldade
    [SerializeField] private float enemiesPerSecondCap = 15f; // Limite m�ximo de inimigos por segundo

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento que � chamado quando um inimigo � destru�do

    private int currentWave = 1; // Onda atual de inimigos
    private float timeSinceLastSpawn; // Tempo desde o �ltimo inimigo gerado
    private int enemiesAlive; // Contador de inimigos ainda vivos
    private int enemiesLeftToSpawn; // Contador de inimigos restantes a serem gerados
    private float eps; // N�mero de inimigos por segundo para a onda atual
    private bool isSpawning = false; // Indica se os inimigos est�o sendo gerados

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); // Adiciona o m�todo EnemyDestroyed ao evento onEnemyDestroy
    }

    private void Start()
    {
        StartCoroutine(StartWave()); // Inicia a gera��o da primeira onda de inimigos
    }

    private void Update()
    {
        if (!isSpawning) return; // Se n�o estiver gerando inimigos, retorna

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o �ltimo spawn

        // Verifica se � hora de gerar um novo inimigo
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(); // Gera um novo inimigo
            enemiesLeftToSpawn--; // Diminui o contador de inimigos restantes
            enemiesAlive++; // Aumenta o contador de inimigos vivos
            timeSinceLastSpawn = 0f; // Reseta o timer de spawn
        }

        // Verifica se todos os inimigos da onda foram destru�dos
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave(); // Encerra a onda atual
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--; // Diminui o contador de inimigos vivos quando um inimigo � destru�do
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Espera o tempo definido entre ondas
        isSpawning = true; // Come�a a gera��o de inimigos
        enemiesLeftToSpawn = EnemiesPerWave(); // Define quantos inimigos ser�o gerados na nova onda
        eps = EnemiesPerSecond(); // Calcula quantos inimigos devem ser gerados por segundo
    }

    private void EndWave()
    {
        isSpawning = false; // Para a gera��o de inimigos
        timeSinceLastSpawn = 0f; // Reseta o tempo desde o �ltimo spawn
        currentWave++; // Aumenta a contagem da onda atual
        StartCoroutine(StartWave()); // Inicia a pr�xima onda
    }

    private void SpawnEnemy()
    {
        // Verifica se a lista de prefabs n�o est� vazia
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("A lista de prefabs de inimigos est� vazia!"); // Log de aviso
            return; // Sai do m�todo se n�o houver prefabs
        }

        // Seleciona um prefab aleat�rio da lista
        GameObject prefabParaSpawnar = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        // Instancia o inimigo na posi��o de in�cio definida pelo LevelManager
        _ = Instantiate(prefabParaSpawnar, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    // Calcula o n�mero de inimigos a serem gerados na onda atual com base na dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); // Retorna um n�mero inteiro baseado na dificuldade
    }

    // Calcula quantos inimigos podem ser gerados por segundo com base na dificuldade   
    private float EnemiesPerSecond()
    {
        // Retorna a quantidade de inimigos por segundo limitada pelo cap
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
