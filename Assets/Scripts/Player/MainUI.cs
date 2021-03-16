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
        PlayerManager player;

        void Start()
        {
            player = PlayerManager.Instance;
            player.OnHealthChange += UpdateHealth;

            playerHealth.maxValue = player.health;
            playerHealth.value = player.health;
        }

        private void UpdateHealth()
        {
            playerHealth.value = player.health;
        }
    }
}
