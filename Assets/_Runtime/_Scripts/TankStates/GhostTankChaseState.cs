using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Photon.Pun.Demo.Asteroids
{
    public class GhostTankChaseState : GhostTankBaseState
    {
        GameObject[] playerTanks;
        float closestTank;
        int tankNumber;

        private float startTime;
        private float timeSinceStart;

        public override void EnterState(GhostTankStateManager ghostTank)
        {
            Debug.Log("tank is chasing!");
            playerTanks = GameObject.FindGameObjectsWithTag("Player");
            closestTank = 0.0f;
            tankNumber = 0;
            if (playerTanks != null)
            {
                for (int i = 0; i < playerTanks.Length; i++)
                {
                    if ((ghostTank.transform.position - playerTanks[i].transform.position).magnitude > closestTank)
                    {
                        closestTank = (ghostTank.transform.position - playerTanks[i].transform.position).magnitude;
                        tankNumber = i;
                    }
                }
                ghostTank.getNavMeshAgent().destination = playerTanks[tankNumber].transform.position;
            }
            startTime = Time.time;
        }
        public override void UpdateState(GhostTankStateManager ghostTank)
        {
            timeSinceStart = Time.time - startTime;
            if (playerTanks != null && (ghostTank.getTargetLocation().position - playerTanks[tankNumber].transform.position).magnitude < 0.5)
            {
                for (int i = 0; i < playerTanks.Length; i++)
                {
                    if ((ghostTank.transform.position - playerTanks[i].transform.position).magnitude > closestTank)
                    {
                        closestTank = (ghostTank.transform.position - playerTanks[i].transform.position).magnitude;
                        tankNumber = i;
                    }
                }
                ghostTank.getNavMeshAgent().destination = playerTanks[tankNumber].transform.position;
            }
            if(timeSinceStart >= 2.0f)
            {
                ghostTank.switchState(ghostTank.roamState);
            }
        }
    }
}