using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] TypeOfAI typeOfAI;
    [Space]
    [Header("Basic settings")]
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerMask, groundMask;
    [Space]
    [Header("Range")]
    [SerializeField] float walkPointRange;
    [SerializeField] float attackRange, sightRange;
    
    [Header("Attack")]
    [SerializeField] float timeBetweenAttack;

    private float rotationSpeed = 2f;
    private bool isPlayerInAttackRange;
    private bool isPlayerInSightRange;
    private bool getDamage;
    private bool isOnAttack;
    private Vector3 walkPoint;
    private bool isPointSet;
    private Animator animator;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        if (!isPointSet) Invoke(nameof(GenerateWalkPoint), 0f);
        if (isPointSet)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(walkPoint - transform.position), Time.deltaTime * rotationSpeed);
            agent.SetDestination(walkPoint);
            animator.SetBool("IsWalk", true);
        } 
        var distance = transform.position - walkPoint;
        if (distance.magnitude < 1f)
        {
            isPointSet = false;
            animator.SetBool("IsWalk", false);
        }

    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Run()
    {
        if (!isPointSet && isPlayerInAttackRange && isPlayerInSightRange) GenerateWalkPoint();
        if(isPointSet) animator.SetBool("IsRun", true);
        if (isPointSet) agent.SetDestination(walkPoint);
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
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, groundMask);
        if (!isPlayerInAttackRange && !isPlayerInSightRange) Walk();
        else if (!isPlayerInAttackRange && isPlayerInSightRange) Chase();
        else if (isPlayerInAttackRange && isPlayerInSightRange) Attack();
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