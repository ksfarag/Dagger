using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        private void Update()
        {

            if (distanceToPlayer() < chaseDistance)
            {
                print(gameObject.name + " is chasing " + GameObject.FindWithTag("Player").name);
            }
        }

        private float distanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}
