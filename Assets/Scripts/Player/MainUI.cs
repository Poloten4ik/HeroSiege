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
        public Image Filler;
       
        PlayerManager player;

        void Start()
        {
            player = PlayerManager.Instance;
            player.OnHealthChange += UpdateHealth;

            player.OnGoldChange += UpdateGold;
          
            playerHealth.maxValue = player.maxHealth;
            playerHealth.value = player.currentHealth;

            maxHpText.text = player.maxHealth.ToString();
            currentHpText.text = player.maxHealth.ToString();

            currentGoldText.text = player.currentGold.ToString();

            Filler.fillAmount = 1;
        }

        public void UpdateHealth()
        { 
            currentHpText.text = player.currentHealth.ToString();
            maxHpText.text = player.maxHealth.ToString();
            Filler.fillAmount = player.currentHealth / player.maxHealth;
        }

        public void UpdateGold()
        {
            currentGoldText.text = player.currentGold.ToString();
        }
    }
}
