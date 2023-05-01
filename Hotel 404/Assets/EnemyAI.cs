using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 3.0f;
    public float chaseRange = 10.0f;
    public float attackRange = 2.0f;
    public float roamDistance = 5.0f;
    public float minRoamDistance = 1.0f;
    public float roamAngle = 180.0f;
    public float roamUpdateFrequencyOffset = 0.5f;
    public float roamUpdateFrequency;

    private Transform target;
    private Vector3 roamTarget;
    private NavMeshAgent navMeshAgent;
    private float nextRoamUpdateTime;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        roamTarget = GetNewRoamTarget();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        // Set the initial value of nextRoamUpdateTime
        nextRoamUpdateTime = Time.time + roamUpdateFrequency;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < chaseRange)
        {
            navMeshAgent.destination = target.position;
            navMeshAgent.stoppingDistance = attackRange;

            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Roam around freely
            if (Time.time > nextRoamUpdateTime)
            {
                if (navMeshAgent.remainingDistance < minRoamDistance)
                {
                    roamTarget = GetNewRoamTarget();
                    navMeshAgent.destination = roamTarget;
                }
                nextRoamUpdateTime = Time.time + Random.Range(roamUpdateFrequency - roamUpdateFrequencyOffset, roamUpdateFrequency + roamUpdateFrequencyOffset);
            }
        }
    }

    private Vector3 GetNewRoamTarget()
    {
        Vector3 randomDirection =
            Quaternion.Euler(0, Random.Range(-roamAngle / 2, roamAngle / 2), 0) * transform.forward;
        float distance = Random.Range(0, roamDistance);
        Vector3 targetPosition = transform.position + randomDirection * distance;

        NavMeshHit hit;
        while (NavMesh.SamplePosition(targetPosition, out hit, roamDistance, NavMesh.AllAreas))
        {
            if (Vector3.Distance(transform.position, hit.position) >= minRoamDistance)
            {
                return hit.position;
            }
            else
            {
                randomDirection =
                    Quaternion.Euler(0, Random.Range(-roamAngle / 2, roamAngle / 2), 0) * transform.forward;
                distance = Random.Range(0, roamDistance);
                targetPosition = transform.position + randomDirection * distance;
            }
        }

        return transform.position;
    }
}

