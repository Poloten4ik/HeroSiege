using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Items
{
    public class Flasck : MonoBehaviour
    {
        [SerializeField] private int healAmount = 100;
        [SerializeField] private int currentAmount = 3;
        [SerializeField] private Text currentAmountText;
        [SerializeField] private AudioClip flaskUse;

        PlayerManager player;

        private void Start()
        {
            player = PlayerManager.Instance;
            currentAmountText.text = currentAmount.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && currentAmount != 0)
            {
                FlasckUse();
            }


        }
        public void FlasckUse()
        {

            player.currentHealth = player.currentHealth + healAmount > player.maxHealth ? player.maxHealth : player.currentHealth + healAmount;
            player.OnHealthChange();
            currentAmount--;
            currentAmountText.text = currentAmount.ToString();
            player.audioSource.PlayOneShot(flaskUse);
        }
    }

}
