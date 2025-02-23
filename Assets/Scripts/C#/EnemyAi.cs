using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player1; // Assign in the Inspector
    public Transform player2; // Assign in the Inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Transform target = GetClosestPlayer();
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    Transform GetClosestPlayer()
    {
        if (player1 == null && player2 == null) return null;
        if (player1 == null) return player2;
        if (player2 == null) return player1;

        float distanceToP1 = Vector3.Distance(transform.position, player1.position);
        float distanceToP2 = Vector3.Distance(transform.position, player2.position);

        return distanceToP1 < distanceToP2 ? player1 : player2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            Vector3 bounceDirection = (transform.position - other.transform.position).normalized;
            agent.velocity = bounceDirection * 3f; // Adjust bounce force as needed
        }
    }
}
