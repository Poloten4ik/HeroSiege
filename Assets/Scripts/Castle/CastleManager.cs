using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Castle
{
    public class CastleManager : MonoBehaviour
    {
        public Action OnHealthChange = delegate { };

        [SerializeField] private Damageable damageable;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private bool announcerOn = false;
        [SerializeField] private AudioClip underAttack;
        [SerializeField] private GameObject cinemachine;

        public int health = 5000;


        PlayerManager playerManager;
        private void Start()
        {
            damageable.OnReceiveDamage += ReceiveDamage;
            playerManager = PlayerManager.Instance;
        }
        private void ReceiveDamage(int damage)
        {
            health -= damage;
            OnHealthChange();
          

            if (!announcerOn)
            {
                announcerOn = true;
                StartCoroutine(AnnouncerOn());
            }

            if (health <= 0)
            {
                cinemachine.SetActive(true);
                StartCoroutine(LoseScreen());
            }
        }

        public IEnumerator AnnouncerOn()
        {
            yield return new WaitForSeconds(2);
            playerManager.audioSource.PlayOneShot(underAttack);
            StartCoroutine(AnnouncerOff());
            
        }

        public IEnumerator AnnouncerOff()
        {
            yield return new WaitForSeconds(5);
            StopCoroutine(AnnouncerOn());
            announcerOn = false; 
        }

        public IEnumerator LoseScreen()
        {
            yield return new WaitForSeconds(3);
            loseScreen.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject);
        }

    }

}
