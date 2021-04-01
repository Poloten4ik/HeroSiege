using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class LevelSystem : MonoBehaviour
    {
        public static LevelSystem Instance { get; private set; }
        public Action OnExpChange = delegate { };
        private PlayerManager playerManager;

        public int playerLvl = 1;
        public float experience = 0;
        public float experienceToNextLevel = 100;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            playerManager = PlayerManager.Instance;
        }

        public void AddExperience(int amount)
        {
            experience += amount;
            OnExpChange();
            if (experience >= experienceToNextLevel)
            {
                playerLvl++;
                experience -= experienceToNextLevel;
                experienceToNextLevel *= 2;
                playerManager.LevelUp(playerLvl);
            }
        }
    }
}
