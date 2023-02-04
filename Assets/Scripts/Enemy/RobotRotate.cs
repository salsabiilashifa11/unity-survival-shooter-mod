using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotate : MonoBehaviour
{
    public float meleeRange = 3f;
    public float rotationSpeed = 10f;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Update () {           
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent>();

        RotateTowards(player);

//        if (IsInMeleeRangeOf(player)) {
//            RotateTowards(player);
//        }
    }

    private bool IsInMeleeRangeOf (Transform player) {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < meleeRange;
    }

    private void MoveTowards (Transform player) {
        nav.SetDestination(player.position);
    }

    private void RotateTowards (Transform player) {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
