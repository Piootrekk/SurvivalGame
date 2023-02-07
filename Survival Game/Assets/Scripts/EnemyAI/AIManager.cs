using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AIManager : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] TypeOfAI typeOfAI;
    [Space]
    [Header("Basic settings")]
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerMask, groundMask, buildMask;
    [SerializeField] float speed;
    [Space]
    [Header("Range")]
    [SerializeField] float walkPointRange;
    [SerializeField] float attackRange, sightRange;
    
    [Header("Attack")]
    [SerializeField] float timeBetweenAttack;

    private float rotationSpeed = 2f;
    private bool isPlayerInAttackRange;
    private bool isPlayerInSightRange;
    private bool isBuildInAttackRange;
    private bool getDamage;
    private bool isOnAttack;
    private Vector3 walkPoint;
    private bool isPointSet;
    private Animator animator;
    private bool invokeMethod = true;
    private NavMeshAgent agent;
    private List<Collider> collidersBuild;

    float timer;
    float delay = 3f;

    public bool GetDamage { get => getDamage; set => getDamage = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (typeOfAI == TypeOfAI.Agresive)
        {
            CheckAgresivesRanges();
        }
        else if (typeOfAI == TypeOfAI.Passive)
        {
            CheckPasiveRanges();
        }
        
    }

    private void Walk()
    {
        if (!isPointSet && !invokeMethod)
        {
            GenerateWalkPoint();
            invokeMethod = true;

        }
        if (isPointSet)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(walkPoint - transform.position), Time.deltaTime * rotationSpeed);
            agent.speed = 1f;
            agent.SetDestination(walkPoint);
            animator.SetBool("IsWalk", true);
            timer = Time.time;
        } 
        var distance = transform.position - walkPoint;
        if (distance.magnitude < 2f)
        {
            isPointSet = false;
            animator.SetBool("IsWalk", false);
            timer = 0f;
        }
        if (Time.time - timer > delay)
        {
            invokeMethod = false;
        }
    }

    private void Chase()
    {
        agent.speed = speed;
        agent.SetDestination(player.position);
        animator.SetBool("IsChase", true);
    }

    private void Run()
    {
        if (!isPointSet && isPlayerInAttackRange && isPlayerInSightRange) GenerateWalkPoint();
        if(isPointSet) animator.SetBool("IsRun", true);
        if (isPointSet)
        {
            agent.speed = 1f;
            agent.SetDestination(walkPoint);
        }
        var distance = transform.position - walkPoint;
        if (distance.magnitude < 1f)
        {
            animator.SetBool("IsRun", false);
            isPointSet = false;
        }
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!isOnAttack)
        {
            isOnAttack = true;
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttack);
            
        }
    }

    private void ResetAttack()
    {
        isOnAttack = false;
    }

    private void CheckAgresivesRanges()
    {
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        isBuildInAttackRange = Physics.CheckSphere(transform.position, attackRange, buildMask);
        if (isBuildInAttackRange)
        {
            collidersBuild = Physics.OverlapSphere(transform.position, attackRange, buildMask).ToList();
            
            Debug.Log("Object in range: " + collidersBuild.FirstOrDefault().gameObject.name);
            AttackBuild();
            return;
        }
        if (!isPlayerInAttackRange && !isPlayerInSightRange) { Walk(); animator.SetBool("IsChase", false); }
        else if (!isPlayerInAttackRange && isPlayerInSightRange) Chase();
        else if (isPlayerInAttackRange && isPlayerInSightRange) { Attack(); animator.SetBool("IsChase", false); }
    }

    private void AttackBuild()
    {
        agent.SetDestination(collidersBuild.First().transform.position);
        transform.LookAt(collidersBuild.First().transform.position);
        if (!isOnAttack)
        {
            isOnAttack = true;
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttack);

        }
    }


    private void CheckPasiveRanges()
    {
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        if (!isPlayerInSightRange)  Walk(); 
        else if (isPlayerInSightRange) Walk();
        else if (getDamage) Run();
    }

    private void GenerateWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, Mathf.Infinity, groundMask))
        {
            isPointSet = true;

        }
        else GenerateWalkPoint();   
    }

}


[System.Serializable]
public enum TypeOfAI
{
    Agresive,
    Passive
}