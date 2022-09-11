using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Numerics;
using Photon.Realtime;

namespace Photon.Pun.Demo.Asteroids
{
    public class GhostTankStateManager : MonoBehaviour
    {
        GhostTankBaseState currentState;
        public GhostTankChaseState chaseState = new GhostTankChaseState();
        public GhostTankRoamState roamState = new GhostTankRoamState();

        //vars to be used in both states
        NavMeshAgent nMA;
        GameObject gameManager;

        //vars to be used in roam state
        Transform targetLocation;

        private void Awake()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                gameManager = GameObject.Find("GameManager");
                targetLocation = gameManager.GetComponent<TanksGameManager>().getRandomTankWayPoint();
            }
                
        }
        // Start is called before the first frame update
        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                currentState = roamState;
                currentState.EnterState(this);
            }
                
        }
        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
                currentState.UpdateState(this);
        }

        public void switchState(GhostTankBaseState state)
        {
            currentState = state;
            state.EnterState(this);
        }
        #region getters and setters
        public NavMeshAgent getNavMeshAgent()
        {
            return nMA;
        }
        public GameObject getGameManager()
        {
            return gameManager;
        }
        public Transform getTargetLocation()
        {
            return targetLocation;
        }


        public void setNavMeshAgent(NavMeshAgent _nMA)
        {
            nMA = _nMA;
        }
        public void setGameManager(GameObject _gameManager)
        {
            gameManager = _gameManager;
        }
        public void setTargetLocation(Transform _targetLocation)
        {
            targetLocation = _targetLocation;
        }
        #endregion
    }
}
