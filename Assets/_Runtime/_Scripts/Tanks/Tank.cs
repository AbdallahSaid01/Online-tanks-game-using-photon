using System.Collections;

using UnityEngine;

using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.Demo.Asteroids
{
    public class Tank : MonoBehaviour
    {
        public float movementSpeed = 5.0f;
        public float rotationSpeed = 100.0f;

        private float initialTime;
        private float currentTime;

        public GameObject theBullet;
        public Transform barrelEnd;

        public int bulletSpeed;
        public PhotonView view;
        public AudioSource audioSource;

        public bool shootAble = true;
        public float waitBeforeNextShot = 1.0f;
        private float currentHealth;
        private float maxHealth;
        [SerializeField] GameObject bulletPrefab;

        private void Start()
        {
            view = GetComponent<PhotonView>();
            audioSource = GetComponent<AudioSource>();
            maxHealth = 5;
            currentHealth = maxHealth;
        }
        private void Update()
        {
            currentTime = Time.time;
            if (view.IsMine)
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
                {
                    if (shootAble)
                    {
                        shootAble = false;
                        view.RPC("Shoot", RpcTarget.All);
                        StartCoroutine(ShootingYield());
                    }
                }
            }
            if(waitBeforeNextShot != 1.0f && currentTime - initialTime >= 3.0f)
            {
                waitBeforeNextShot = 1.0f;
            }
            if(currentHealth <= 0)
            {
                
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
            if (collision.gameObject.tag == "PowerUp")
            {
                PhotonNetwork.Destroy(collision.gameObject);
                initialTime = Time.time;
                waitBeforeNextShot = 0.2f;
            }
            if (collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(collision.collider, GetComponent <Collider>());
            }
        }
        public void ReduceHealth()
        {
            currentHealth -= 1;
        }
    }
}

