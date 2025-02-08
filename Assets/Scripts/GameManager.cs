using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     public GameObject enemyPrefab; // Prefab quái
    public Transform spawnPoint;   // Vị trí spawn
    public float spawnRate = 1f;   // Mỗi giây spawn 1 quái
    public float waveInterval = 5f; // Khoảng cách giữa các đợt quái
    public int enemiesPerWave = 5; // Số lượng quái mỗi đợt

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnRate); // Chờ mỗi giây spawn 1 quái
            }
            yield return new WaitForSeconds(waveInterval); // Chờ 5 giây rồi tiếp tục đợt quái mới
            enemiesPerWave++;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
    public void RestartGame()
    {
        Time.timeScale = 1; // Đặt lại tốc độ game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại scene
    }
}
