using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private NavMeshAgent agent;
    private bool speedBoosted = false; // Kiểm tra đã tăng tốc chưa

    public float speedMultiplier = 2f; // Hệ số nhân tốc độ khi máu dưới 50%

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!speedBoosted && currentHealth <= maxHealth * 0.5f)
        {
            BoostSpeed();
        }
    }

    void BoostSpeed()
    {
        speedBoosted = true;
        agent.speed *= speedMultiplier;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
