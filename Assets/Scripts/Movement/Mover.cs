using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }
        public void startMoveToAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().startAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
        public void Cancel() 
        {
            navMeshAgent.isStopped = true;
        }
        

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity; //global velocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); //local velocity for the agent
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
            //SetFloat() sends Velocity values to the animator
        }
    }
}