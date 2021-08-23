using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat()) { return; }   //if found a target to attack, exit update
            if (InteractWithMovement()) { return; } //if found a legal spot to move to, exit update
            print("nothing to interact with");      //else, print "nothing to interact with"
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit; // gets info back from Physics.Raycast
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            //   hasHit = true if the ray casted out of Physics.Raycast hit a collider
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) 
                {
                    GetComponent<Mover>().startMoveToAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanAttack(target)) { continue; }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        //Returns a ray starting from Camera POV and ends at where the mouse last clicked
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
