using System.Collections;
using UnityEngine.AI;
using UnityEngine;

using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.Demo.Asteroids
{
    public class GhostTank : MonoBehaviour
    {
        public float movementSpeed = 5.0f;
        public float rotationSpeed = 100.0f;

        public GameObject theBullet;
        public Transform barrelEnd;

        public int bulletSpeed;
        public PhotonView view;
        public AudioSource audioSource;

        public bool shootAble = true;
        public float waitBeforeNextShot = 1.0f;

        private NavMeshAgent nma;
        float closestPlayerDistance;
        GameObject[] players;
        [SerializeField] LayerMask lm;
        private Transform[] wayPoints;
        [SerializeField] GameObject bulletPrefab;

        private void Start()
        {
            view = GetComponent<PhotonView>();
            audioSource = GetComponent<AudioSource>();
            nma = GetComponent<NavMeshAgent>();
            players = GameObject.FindGameObjectsWithTag("Player");
            closestPlayerDistance = Mathf.Infinity;
            wayPoints = GameObject.Find("GameManager").GetComponent<TanksGameManager>().getWaypoints();
        }
        private void Update()
        {
            //MoveBetweenWaypoints(0);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, lm))
            {
                if (shootAble)
                {
                    shootAble = false;
                    view.RPC("Shoot", RpcTarget.All);
                    StartCoroutine(ShootingYield());
                }
            }
        }
        IEnumerator ShootingYield()
        {
            yield return new WaitForSeconds(waitBeforeNextShot);
            shootAble = true;
        }
        [PunRPC]
        private void Shoot()
        {
            var bullet = PhotonNetwork.Instantiate("TankShell", barrelEnd.position, barrelEnd.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            audioSource.Play();
            Debug.Log("Played: PlayShootSound");
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
        
    }
}

