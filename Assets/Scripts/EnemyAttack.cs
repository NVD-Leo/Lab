using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1.5f; // Mỗi 1.5s tấn công 1 lần
    private float nextAttackTime = 0f;

    void Start()
    {
        nextAttackTime += attackRate;
    }

    public void TryAttack(TargetHealth targetHealth)
    {
        if (Time.time >= nextAttackTime)
        {
            targetHealth.TakeDamage(damage);
            nextAttackTime = Time.time + attackRate;
        }
    }
}
