using Assets.Scripts.Items;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance { get; private set; }


        [SerializeField] private float shopRadius;
        [SerializeField] private int salvePrice;
        [SerializeField] private int bookPrice;
        [SerializeField] private int bootsPrice;
        [SerializeField] private Flasck flasck;
        [SerializeField] private MainUI mainUI;
        [SerializeField] private GameObject shopUI;
        [SerializeField] private PlayerAnimationHelper playerAnimationHelper;
        [SerializeField] private Weapon weapon;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip noGold;

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

        void Start()
        {
            player = PlayerManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                shopUI.SetActive(true);
                playerAnimationHelper.enabled = false;
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                shopUI.SetActive(false);
                playerAnimationHelper.enabled = true;
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, shopRadius);
        }


        public void BuySalce()
        {
            if (player.currentGold >= salvePrice)
            {
                flasck.currentAmount++;
                flasck.currentAmountText.text = flasck.currentAmount.ToString();
                player.currentGold -= salvePrice;
                mainUI.UpdateGold(); 
            }

            else
            {
                audioSource.PlayOneShot(noGold);
            }
        }

        public void BuyBook()
        {
            if (player.currentGold >= bookPrice)
            {
                weapon.damage += 50;
                player.damageText.text = weapon.damage.ToString();
                player.currentGold -= bookPrice;
                mainUI.UpdateGold();
            }

            else
            {
                audioSource.PlayOneShot(noGold);
            }
        }
    }

}

