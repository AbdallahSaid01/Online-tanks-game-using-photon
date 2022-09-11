using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.Demo.Asteroids
{
    public class TanksGameManager : MonoBehaviourPunCallbacks
    {
        public static TanksGameManager Instance = null;
        [SerializeField] Transform[] tankSpawnLocations;
        [SerializeField] Transform[] powerUpSpawnLocations;
        [SerializeField] Transform[] waypoints;

        public void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            Transform location = getRandomTankSpawnPoint();
            PhotonNetwork.Instantiate("PlayerTigerTank", location.position, location.rotation);
            foreach (Transform t in powerUpSpawnLocations)
                PhotonNetwork.Instantiate("DamagePowerUp", t.position, t.rotation);
            if(PhotonNetwork.IsMasterClient)
                for (int i = 0; i <= (3 - PhotonNetwork.PlayerList.Length); i++)
                    PhotonNetwork.InstantiateSceneObject("GhostTigerTank", waypoints[i].position, waypoints[i].rotation);
        }
        private void OnApplicationQuit()
        {
            PhotonNetwork.LeaveRoom();
        }
        public Transform getRandomTankSpawnPoint()
        {
            return tankSpawnLocations[Mathf.RoundToInt(Random.Range(0, tankSpawnLocations.Length))];
        }
        public Transform getRandomTankWayPoint()
        {
            return waypoints[Mathf.RoundToInt(Random.Range(0, waypoints.Length))];
        }
        public void DisconnectPlayer()
        {
            StartCoroutine(DisconnectAndLoad());
        }
        IEnumerator DisconnectAndLoad()
        {
            PhotonNetwork.Disconnect();
            while (PhotonNetwork.IsConnected)
                yield return null;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Tanks-LobbyScene");
        }
        public Transform[] getWaypoints()
        {
            return waypoints;
        }
    }
}
