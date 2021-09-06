using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public bool IsDead() 
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {            
            if (isDead) { return; }
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<NavMeshAgent>().enabled = false; //stops the character from moving after it dies
            GetComponent<Fighter>().enabled = false; //stops the character attack/movement behaviour after it dies
        }
    }

}