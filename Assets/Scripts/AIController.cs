using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask playerMask, groundMask;

    public int health;

    public GameObject Circle;

    public Vector2 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Circle = GameObject.Find("Circle");
        agent = GetComponent<NavMeshAgent>();
        health = 100;
    }

    private void Update()
    {
        if (health < 0 || health == 0)
        {
            Destroy(gameObject);
        }

        if(Physics2D.OverlapCircle(transform.position, sightRange, playerMask) != null)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }

        if (Physics2D.OverlapCircle(transform.position, attackRange, playerMask) != null)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }

        if (!playerInSightRange && !playerInAttackRange) WalkAround();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void WalkAround()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Vector2 direction = new Vector2(
                walkPoint.x - transform.position.x,
                walkPoint.y - transform.position.y
            );

            transform.up = direction;
        }
        Vector2 distanceToWalkPoint = (Vector2)transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        if (walkPoint.x < Circle.transform.localScale.x / 2 && walkPoint.x > -Circle.transform.localScale.x / 2 && walkPoint.y < Circle.transform.localScale.y / 2 && walkPoint.y > -Circle.transform.localScale.y / 2)
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination((Vector2)player.position);
        Vector2 direction = new Vector2(
                player.position.x - transform.position.x,
                player.position.y - transform.position.y
            );

        transform.up = direction;
    }

    private void AttackPlayer()
    {

        agent.SetDestination((Vector2)transform.position);

        Vector2 direction = new Vector2(
                player.position.x - transform.position.x,
                player.position.y - transform.position.y
            );

        transform.up = direction;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
