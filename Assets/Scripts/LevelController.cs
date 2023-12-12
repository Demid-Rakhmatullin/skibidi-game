using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    [SerializeField] PlayerController player;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform spawnPoints;

    private int _enemiesCounter;

    void Awake()
    {
        Instance = this;

        startCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void StartGame()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        SpawnEnemy();
    }

    public void RestartGame()
    {
        player.Ressurect();
        _enemiesCounter = 0;

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
            Destroy(enemy);

        startCanvas.GetComponent<MenuController>().ShowStart();
    }

    public void PlayerDead()
    {
        startCanvas.SetActive(true);
        startCanvas.GetComponent<MenuController>().ShowRestart();
        gameCanvas.SetActive(false);
    }

    public void EnemyDead()
    {
        _enemiesCounter++;

        if (_enemiesCounter == spawnPoints.childCount)
        {
            startCanvas.SetActive(true);
            startCanvas.GetComponent<MenuController>().ShowWin();
            gameCanvas.SetActive(false);
        }
        else
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var go = Instantiate(enemyPrefab, spawnPoints.GetChild(_enemiesCounter).position, Quaternion.identity);
        go.transform.LookAt(player.transform);

        var enemy = go.GetComponent<BasicToiletEnemyController>();
        player.SetChaseTarget(enemy.ChaseTarget);
    }
}
