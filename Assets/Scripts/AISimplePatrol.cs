using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISimplePatrol : MonoBehaviour
{
    //Dictates whether the agent waits on each node.
    [SerializeField]
    bool patrolWaiting;

    //the total time we wait at each node
    [SerializeField]
    float totalWaitTime = 3f;

    //probability of switching direction
    [SerializeField]
    float switchProbability = 0.2f;

    //list of nodes to visit
    [SerializeField]
    List<Waypoint> patrolPoints;

    //variables for base behaviour
    UnityEngine.AI.NavMeshAgent agent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    //animator variables
    public Animator anim;
    public bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("nav mesh component not attached to " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for behaviour");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if close to destination
        if (travelling && agent.remainingDistance <= 1.0f)
        {
            travelling = false;

            //if wait, then wait
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //instead if waiting
        if (waiting)
        {
            isWalking = false;

            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                isWalking = true;

                ChangePatrolPoint();
                SetDestination();
            }
        }

        //update animator for walking or not
        anim.SetBool("walking", isWalking);
    }

    private void SetDestination()
    {
        if (patrolPoints != null)
        {
            isWalking = true;

            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            agent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count; //check if exceeded list and reset to 0 if so
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
