using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class TankShell : MonoBehaviour
    {
        private float initialTime;
        private float currentTime;

        public float despawnTime = 3.0f;
        public Player Owner { get; private set; }
        private void Start()
        {
            initialTime = Time.time;
            currentTime = Time.time;
        }
        private void Update()
        {
            currentTime = Time.time;
            if(currentTime - initialTime >= despawnTime)
                PhotonNetwork.Destroy(gameObject);
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Destructable")
            {
                gameObject.GetComponent<PhotonView>().RPC("destroyCube", RpcTarget.All, collision.gameObject.GetComponent<PhotonView>().ViewID);
                PhotonNetwork.Destroy(collision.gameObject);
            }
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        public void destroyCube(int cubeID)
        {
            Destroy(PhotonView.Find(cubeID).gameObject);
        }
    }
}

