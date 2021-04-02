using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        AudioSource audioSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
