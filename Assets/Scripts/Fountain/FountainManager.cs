using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Fountain
{
    public class FountainManager : MonoBehaviour
    {
        public static FountainManager Instance { get; private set; }

        [SerializeField] private float healRadius = 1f;
        [SerializeField] private int healingRate = 1;

        public ParticleSystem healEffect;

        PlayerManager player;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            player = PlayerManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.healthRegeneration += healingRate;
                if (player.currentHealth < player.maxHealth)
                healEffect.Play();
                print("start");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.healthRegeneration -= healingRate;
                healEffect.Stop();
                print("stop");
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, healRadius);
        }
    }
}
