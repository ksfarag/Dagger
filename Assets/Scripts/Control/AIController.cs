using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointToleranceDistance = 1f;
        [SerializeField] float waypointCooldownTime;


        Fighter fighter;
        Mover mover;
        GameObject player;

        Vector3 guardPos;
        float timePlayerWasLastSeen = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = 0;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            guardPos = transform.position;
            mover = GetComponent<Mover>();
        }
        private void Update()
        {

            if (inAttackRange() && fighter.CanAttack(player))
            {
                Attack();
            }
            else if (timePlayerWasLastSeen <= suspicionTime)
            {
                SusBehavior();
            }
            else
            {
                Patrol();
            }
            timePlayerWasLastSeen += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void Patrol()
        {
            Vector3 nextPos = guardPos;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPos = CurrentWaypoint();
            }
            if (timeSinceArrivedAtWaypoint > waypointCooldownTime)
            {
                mover.startMoveToAction(nextPos);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, CurrentWaypoint());
            return distanceToWaypoint < waypointToleranceDistance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 CurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SusBehavior()
        {
            fighter.Cancel();
        }

        private void Attack()
        {
            timePlayerWasLastSeen = 0;
            fighter.Attack(player);
        }

        //checks if player is in the enemy AI attack range
        private bool inAttackRange()
        {

            float distanceToPlayer= Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
