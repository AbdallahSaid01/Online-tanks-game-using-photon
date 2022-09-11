using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Photon.Pun.Demo.Asteroids
{
    public class GhostTankRoamState : GhostTankBaseState
    {
        private float startTime;
        private float timeSinceStart;
        public override void EnterState(GhostTankStateManager ghostTank)
        {
            Debug.Log("tank is roaming!");
            ghostTank.setNavMeshAgent(ghostTank.GetComponent<NavMeshAgent>());
            ghostTank.setTargetLocation(ghostTank.getGameManager().GetComponent<TanksGameManager>().getRandomTankWayPoint());
            ghostTank.getNavMeshAgent().destination = ghostTank.getTargetLocation().position;
            startTime = Time.time;
        }
        public override void UpdateState(GhostTankStateManager ghostTank)
        {
            timeSinceStart = Time.time - startTime;
            if ((ghostTank.getTargetLocation().position - ghostTank.transform.position).magnitude < 0.5)
            {
                ghostTank.setTargetLocation(ghostTank.getGameManager().GetComponent<TanksGameManager>().getRandomTankWayPoint());
                ghostTank.getNavMeshAgent().destination = ghostTank.getTargetLocation().position;
            }
            if(timeSinceStart >= 5.0f)
            {
                ghostTank.switchState(ghostTank.chaseState);
            }
        }
    }
}