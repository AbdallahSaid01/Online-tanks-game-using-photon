using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Photon.Pun.Demo.Asteroids
{
    public class GhostTankMovement : MonoBehaviour
    {
        NavMeshAgent nMA;
        GameObject gameManager;
        Transform targetLocation;
        private void Awake()
        {
            nMA = GetComponent<NavMeshAgent>();
            gameManager = GameObject.Find("GameManager");
            targetLocation = gameManager.GetComponent<TanksGameManager>().getRandomTankWayPoint();
        }
        private void Start()
        {
            nMA.destination = targetLocation.position;
        }
        private void Update()
        {
            if ((targetLocation.position - transform.position).magnitude < 0.5)
            {
                targetLocation = gameManager.GetComponent<TanksGameManager>().getRandomTankWayPoint();
                nMA.destination = targetLocation.position;
            }
        }
    }
}