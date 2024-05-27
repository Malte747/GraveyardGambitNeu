using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

LevelGold levelGold;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;
void Start()
{
    levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
}


    void Update()
    {
        if (waypoints.Length == 0)
            return;

        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        // Move towards the waypoint at a constant speed
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Smoothly rotate towards the waypoint
        Vector3 targetDirection = (targetWaypoint.position - transform.position).normalized;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation *= Quaternion.Euler(0, -90, 90); // Maintain 90 degrees on the Z axis
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if we have reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.5f);
            if (i < waypoints.Length - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            levelGold.EndTimer();
        }
    }
}
