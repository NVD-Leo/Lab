using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform target; // Mục tiêu của Enemy
    private NavMeshAgent agent;
    private EnemyAttack enemyAttack;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Base").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position); // Thiết lập mục tiêu
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void FixedUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 1)
        {
            TargetHealth TargetHealth = target.GetComponent<TargetHealth>();
            enemyAttack.TryAttack(TargetHealth);
        }
    }
}
