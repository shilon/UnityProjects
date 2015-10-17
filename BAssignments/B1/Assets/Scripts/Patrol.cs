// Patrol.cs
using UnityEngine;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private int maxDest;
    private NavMeshAgent agent;
    public float minWaypointDistance = 0.1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        maxDest = points.Length - 1;
        
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        Vector3 tempLocalPosition;
        Vector3 tempWaypointPosition;

        // Agents position (x, set y to 0, z)
        tempLocalPosition = transform.position;
        tempLocalPosition.y = 0f;

        // Current waypoints position (x, set y to 0, z)
        tempWaypointPosition = points[destPoint].position;
        tempWaypointPosition.y = 0f;

        // Is the distance between the agent and the current waypoint within the minWaypointDistance?
        if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance)
        {
            // Have we reached the last waypoint?
            if (destPoint == maxDest)
            {
                // If so, go back to the first waypoint and start over again
                destPoint = 0;
            }
            else
            {   // If we haven't reached the Last waypoint, just move on to the next one
                destPoint++;
            }
        }

        // Set the destination for the agent
        // The navmesh agent is going to do the rest of the work
        agent.SetDestination(points[destPoint].position);
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
      
        GotoNextPoint();
    }
}
