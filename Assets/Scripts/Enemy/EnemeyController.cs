using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://www.youtube.com/watch?v=UjkSFoLxesw&t=150s
public class EnemeyController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    private Transform player;

    public float health = 100;

    private Vector3[] walkPoints;
    private int currWalkPoint = 0;
    private bool walkPointSet = false;
    public GameObject projectile;
    public float timeBetweenAttacks = 0.3f;
    bool alreadyAttacked;
    public float damage = 30f;
    public float projectileSpeed = 30f;
    private Transform firePoint;

    public float sightRange, attackRange;

    private Animator animator;
    public GameObject explosion;

    private void Start()
    {
        player = GameObject.Find("Player").transform;   
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        firePoint = GameObject.Find(gameObject.name + "FirePoint").GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
        
        GameObject[] walkObjects = GameObject.FindGameObjectsWithTag("walk" + gameObject.name);
        walkPoints = new Vector3[walkObjects.Length];
        int i = 0;
        foreach(GameObject walkObj in walkObjects){
            walkPoints[i] = walkObj.transform.position;
            i++;
        }
        
    }


    private void Update()
    {
        if (health <= 0){
            Vector3 spawnPos = transform.position;
            spawnPos.y += 0.25f;
            GameObject explosionObj = Instantiate(explosion, spawnPos, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosionObj, 2);
        }

        float playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance < attackRange){ 
            animator.SetBool("isInRange", false);
            animator.SetBool("isAtacking", true);
            AttackPlayer();
        }
        else if (playerDistance < sightRange) {
            animator.SetBool("isInRange", true);
            animator.SetBool("isPatroling", false);
            animator.SetBool("isAtacking", false);
            ChasePlayer();
        }
        else {
            animator.SetBool("isPatroling", true);
            animator.SetBool("isAtacking", false);
            Patroling();
        }
    }

    private void Patroling()
    {
        float currDistance = Vector3.Distance(walkPoints[currWalkPoint], transform.position);
        
        if (currDistance < 2){
            walkPointSet = false;
            currWalkPoint = (currWalkPoint + 1) % walkPoints.Length;
        }
        if(!walkPointSet)
            agent.SetDestination(walkPoints[currWalkPoint]);
    }
    

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6f); 
        

      if (!alreadyAttacked)
        {
            GameObject projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity);
            projectileObj.GetComponent<ProjectileController>().damage = damage;
            projectileObj.GetComponent<ProjectileController>().isEnemy = true;
            Rigidbody rb = projectileObj.GetComponent<Rigidbody>();
            Vector3 velocity = (player.position - firePoint.position).normalized * projectileSpeed;
            velocity.x += Random.Range(-2, 2);
            rb.velocity = velocity;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.tag == "Projectile"){
            health -= other.gameObject.GetComponent<ProjectileController>().damage;
        }
    }
    
}
