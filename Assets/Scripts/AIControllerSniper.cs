using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControllerSniper : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask playerMask, groundMask, circleMask;

    public int health;

    public GameObject Circle;

    public Vector2 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private GameObject bulletAnchor;
    public GameObject bulletPrefab;
    private int ammo;
    private bool isFireAllowed;

    private bool doUpdate = true;

    public AudioSource shootAudioSource;
    public AudioSource reloadAudioSource;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Circle = GameObject.Find("Circle");
        bulletAnchor = transform.GetChild(0).gameObject;
        agent = GetComponent<NavMeshAgent>();
        health = 100;
        ammo = 5;
        isFireAllowed = true;
    }

    private void Update()
    {
        if (health < 0 || health == 0)
        {
            Destroy(gameObject);
        }

        if (Physics2D.OverlapCircle(transform.position, sightRange, playerMask) != null)
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

        if (doUpdate == true)
        {
            if (!playerInSightRange && !playerInAttackRange) WalkAround();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
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

        if (Physics2D.OverlapCircle(walkPoint, 1f, circleMask) != null)
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

        if (isFireAllowed == true)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Reload()
    {
        isFireAllowed = false;
        reloadAudioSource.Play();
        yield return new WaitForSeconds(2f);
        ammo = 5;
        isFireAllowed = true;
    }

    IEnumerator Fire()
    {
        isFireAllowed = false;
        shootAudioSource.Play();
        Vector2 bulletPosition = bulletAnchor.transform.position;
        GameObject proj = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
        proj.GetComponent<bullet>().damage = 50;
        ammo--;
        if (ammo == 0)
        {
            StartCoroutine(Reload());
            yield break;
        }
        yield return new WaitForSeconds(1f);
        isFireAllowed = true;
    }

    private void OnDisable()
    {
        doUpdate = false;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            CancelInvoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            InvokeRepeating("StormDamage", 0, 0.2f);
        }
    }
    private void StormDamage()
    {
        health -= 1;
    }
}
