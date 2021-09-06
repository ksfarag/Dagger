using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity; //setting it to infinity makes AttackBehaviour() start the first attack faster
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            moveTo();
        }
        public void moveTo()
        {
            if (target == null) { return; }
            if (target.IsDead()) { return; }

            if (!InRange()) // if not in range, move to target
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else //else if in range of target, cancel mover then attack
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack"); //This will trigger the Hit() event within the animation
                timeSinceLastAttack = 0;
            }

        }

        // Animation event - called within the animator(Attack animation)
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        private bool InRange()
        {
            // Vector3.Distance gets the distance between transform.pos (player pos) and the target pos
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // check if player/enemies can attack (if target != null && target is alive)
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }


    }
}
