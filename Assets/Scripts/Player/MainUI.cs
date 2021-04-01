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
        public Image hpFiller;
        public Image xpFilter;

        private PlayerManager player;
        private LevelSystem levelSystem;

        void Start()
        {
            player = PlayerManager.Instance;
            levelSystem = LevelSystem.Instance;

            player.OnHealthChange += UpdateHealth;

            player.OnGoldChange += UpdateGold;
            levelSystem.OnExpChange += UpdateExp;

            playerHealth.maxValue = player.maxHealth;
            playerHealth.value = player.currentHealth;

            maxHpText.text = player.maxHealth.ToString();
            currentHpText.text = player.maxHealth.ToString();

            currentGoldText.text = player.currentGold.ToString();

            hpFiller.fillAmount = 1;
            xpFilter.fillAmount = 0;
        }

        public void UpdateHealth()
        {
            currentHpText.text = player.currentHealth.ToString();
            maxHpText.text = player.maxHealth.ToString();
            hpFiller.fillAmount = player.currentHealth / player.maxHealth;
        }

        public void UpdateGold()
        {
            currentGoldText.text = player.currentGold.ToString();
        }

        public void UpdateExp()
        {
            xpFilter.fillAmount = levelSystem.experience / levelSystem.experienceToNextLevel;
        }
    }
}
