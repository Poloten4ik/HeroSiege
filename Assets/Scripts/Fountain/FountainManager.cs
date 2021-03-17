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
                print("heal on");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.healthRegeneration -= healingRate;
                print("heal off");
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, healRadius);
        }
    }
}
