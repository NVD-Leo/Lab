using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    public GameObject gameOverPanel; // Gán Panel Game Over trong Inspector

    void Start()
    {
        currentHealth = maxHealth;
        gameOverPanel.SetActive(false); // Ẩn panel khi bắt đầu game
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameOverPanel.SetActive(true); // Hiển thị Game Over Panel
        Time.timeScale = 0; // Dừng game
    }
}
