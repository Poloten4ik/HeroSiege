using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.UI
{
    public class WinningScene : MonoBehaviour
    {
        public static WinningScene Instance { get; private set; }

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject cinemachine;


        [Header("Sounds")]
        public AudioSource audioSource;
        public AudioSource backgroundMusic;
        [SerializeField] private AudioClip winningSound;


        private void Awake()
        {

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
         public void WinningCinemashine()
        {
            cinemachine.SetActive(true);
            audioSource.PlayOneShot(winningSound);
            backgroundMusic.mute = true;
            StartCoroutine(WinningUI());
        }

        private IEnumerator WinningUI()
        {
            yield return new WaitForSeconds(3);
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
