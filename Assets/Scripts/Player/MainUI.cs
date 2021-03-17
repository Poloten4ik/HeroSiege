using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
   public class MainUI : MonoBehaviour
    {
        public Slider playerHealth;
        public Text currentHpText;
        public Text maxHpText;
        public Text currentGoldText;
        
        PlayerManager player;



        void Start()
        {
            player = PlayerManager.Instance;
            player.OnHealthChange += UpdateHealth;
            player.OnGoldChange += UpdateGold;

            playerHealth.maxValue = player.maxHealth;
            playerHealth.value = player.currentHealth;

            maxHpText.text = player.maxHealth.ToString();
            currentHpText.text = player.currentHealth.ToString();
            currentGoldText.text = player.currentGold.ToString();
        }

        public void UpdateHealth()
        {
            playerHealth.value = player.currentHealth;
            currentHpText.text = player.currentHealth.ToString();
        }

        public void UpdateGold()
        {
            currentGoldText.text = player.currentGold.ToString();
        }
    }
}
