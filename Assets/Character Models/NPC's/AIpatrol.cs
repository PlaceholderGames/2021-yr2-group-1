using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;


public class AIpatrol : MonoBehaviour
{

    //public Transform[] points; // create an array of points which I can assign for the NPC to move between
    //private int destPoint = 0; // initialising the destination point
    private NavMeshAgent agent;

    [SerializeField]
    Transform _destination;

    //animator variable
    public Animator anim;
    public bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("the nav mesh is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        //turning this off as i want it to happen for now
        // agent.autoBraking = false;

    }

    private void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            agent.SetDestination(targetVector);
        }
    }

    //void GotoNextPoint()
    //{
    //    // Returns if no points have been set up
    //    if (points.Length == 0)
    //        return;

    //    //something to sleep the ai in here 
    //    isWalking = false;

    //    // Set the agent to go to the currently selected destination.
    //    agent.destination = points[destPoint].position;

    //    //sets state of is walking to true
    //    isWalking = true;

    //    // Choose the next point in the array as the destination,
    //    // cycling to the start if necessary.
    //    destPoint = (destPoint + 1) % points.Length;
    //}


    // Update is called once per frame
    void Update()
    {
        //update animator for walking or not
        anim.SetBool("walking", isWalking);

        // Choose the next destination point when the agent gets
        // close to the current one.
        //if (!agent.pathPending && agent.remainingDistance < 0.0f)
        //    GotoNextPoint();
    }
}
