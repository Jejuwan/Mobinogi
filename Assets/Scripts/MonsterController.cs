using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : MonoBehaviour
{
    [SerializeField]public Transform player;
    private Animator animator;

    public float chaseRange = 10f;    
    public float stopDistance = 1.5f;

    private NavMeshAgent agent;
    private bool isChasing = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange && distance > stopDistance)
        {
            agent.SetDestination(player.position);
            isChasing = true;
            animator.SetBool("isWalking", true);

            Vector3 direction = (player.position - transform.position).normalized;
            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    10f * Time.deltaTime
                );
            }
        }
        else if (distance <= stopDistance)
        {
            agent.ResetPath();
            isChasing = false;
            animator.SetBool("isWalking", false);
        }
        else if (isChasing)
        {
            agent.ResetPath();
            isChasing = false;
        }
    }
}
