using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public delegate void OnTargetFound(RaycastHit hit);
    public Transform target;
    private NavMeshAgent agent;
    private EnemySense[] senses;

    public float jumpScareTimer;
    public float jumpScareRate;

    public float dashSpeed;
    public float moveSpeed;
    public float jumpSacreSpeed;

    public Transform jumpScarePoint;
    public int jumpScareProbability;

    public float searchDistance;
    public float chaseDistance;
    public float reachDistance;

    public float deathTime;
    public float deathTimer;
    public Image deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        senses = gameObject.GetComponentsInChildren<EnemySense>();
        target = GameObject.Find("Character").transform;
        jumpScareTimer = jumpScareRate;
        Stop();
        agent.stoppingDistance = reachDistance;

        foreach(EnemySense sense in senses)
        {
            sense.EnemyAnimator = gameObject.GetComponent<Animator>();
        }
    }

    public void LookAtPoint(Vector3 point)
    {
        Vector3 pointPosition = new Vector3(point.x, transform.position.y, point.z);
        Quaternion toRotation = Quaternion.LookRotation(pointPosition - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, agent.angularSpeed * Time.deltaTime);
    }

    public void SearchWithRaycast(float searchDistance, OnTargetFound onTargetFound)
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * searchDistance, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit, searchDistance))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                onTargetFound(hit);
            }
        }
    }

    public void MoveTowards()
    {
        if (agent.stoppingDistance < reachDistance)
            agent.stoppingDistance = reachDistance;
        if (agent.isStopped)
            agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        agent.SetDestination(target.position);
    }

    public void ChaseTowards()
    {
        if (agent.stoppingDistance < reachDistance)
            agent.stoppingDistance = reachDistance;
        if (agent.isStopped)
            agent.isStopped = false;
        agent.speed = dashSpeed;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        agent.SetDestination(target.position);
    }

    public void Stop()
    {
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        if(!agent.isStopped)
            agent.isStopped = true;
    }

    public bool AttemptJumpscare()
    {
        if(jumpScareTimer >= 0)
        {
            jumpScareTimer = jumpScareTimer - (1 * Time.deltaTime);
            return false;
        } else
        {
            int rnd = Random.Range(0, 100);
            if (rnd <= jumpScareProbability)
                return true;
            else
            { 
                jumpScareTimer = jumpScareRate;
                return false;
            }
        }
    }
    public void JumpScare()
    {
        if(jumpScareTimer < jumpScareRate)
            jumpScareTimer = jumpScareRate;
        if (agent.isStopped)
            agent.isStopped = false;
        agent.speed = jumpSacreSpeed;
        agent.stoppingDistance = 0f;
        agent.angularSpeed = 1;
        agent.acceleration = 500;
        agent.SetDestination(jumpScarePoint.position);
    }

    public float DistanceFromTarget()
    {
        if (target != null)
            return Vector3.Distance(target.transform.position, transform.position);
        else
            return 0;
    }

    public float DistanceFromJumpScare()
    {
        if (target != null)
            return Vector3.Distance(jumpScarePoint.transform.position, transform.position);
        else
            return 0;
    }
}
