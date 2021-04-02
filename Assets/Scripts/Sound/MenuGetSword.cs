using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class MenuGetSword : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] attackSound;
        public AudioClip slam;
        public AudioClip getWeapon;

        public void PlayerSoundEffect()
        {
            int random = Random.Range(0, attackSound.Length);

            audioSource.PlayOneShot(attackSound[random]);

        }

         public void SlamSound()
        {
            audioSource.PlayOneShot(slam);
        }

        public void GetWeapon()
        {
            audioSource.PlayOneShot(getWeapon);
        }
    }
}
