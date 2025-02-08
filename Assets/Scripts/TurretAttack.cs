using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TowerAttack : MonoBehaviour
{
    public Transform rotatingPart; // Phần xoay của tháp
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackRate = 1f;
    public float attackRange = 5f; // Bán kính tấn công của tháp
    public bool prioritizeLowestHealth = true; // Ưu tiên kẻ địch ít máu hoặc gần Target nhất

    private float nextAttackTime = 0f;

    void FixedUpdate()
    {
        GameObject target = GetTarget();
        if (target != null && Time.time >= nextAttackTime)
        {
            Debug.Log("ta");
            Attack(target);
            nextAttackTime = Time.time + attackRate;
        }
    }

    GameObject GetTarget()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(firePoint.position, attackRange, LayerMask.GetMask("Enemy"));
        List<GameObject> enemies = hitEnemies.Select(hit => hit.gameObject).ToList();

        if (enemies.Count == 0) return null;

        if (prioritizeLowestHealth)
        {
            return enemies.OrderBy(e => e.GetComponent<EnemyHealth>().maxHealth).FirstOrDefault();
        }
        else
        {
            return enemies.OrderBy(e => Vector3.Distance(e.transform.position, GameObject.FindWithTag("Target").transform.position)).FirstOrDefault();
        }
    }

    void Attack(GameObject target)
    {
        if (rotatingPart)
        {
            rotatingPart.LookAt(target.transform);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }

    // Hiển thị phạm vi tấn công bằng Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, attackRange);
    }
}
