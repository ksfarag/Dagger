using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {

            if (inAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else 
            {
                fighter.Cancel();
            }

        }

        //checks if player is in the enemy AI attack range
        private bool inAttackRange()
        {

            float distanceToPlayer= Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}
