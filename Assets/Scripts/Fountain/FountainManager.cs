using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Fountain
{
    public class FountainManager : MonoBehaviour
    {
        [SerializeField] private float healRadius = 1f;
        [SerializeField] private int healingRate = 1;
        [SerializeField] private ParticleSystem healEffect;

        PlayerManager player;
        private void Start()
        {
            player = PlayerManager.Instance;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.healthRegeneration += healingRate;
                healEffect.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.healthRegeneration -= healingRate;
                healEffect.Stop();
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, healRadius);
        }
    }
}
