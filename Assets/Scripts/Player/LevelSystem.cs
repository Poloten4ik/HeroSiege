using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class LevelSystem : MonoBehaviour
    {
        public static LevelSystem Instance { get; private set; }

        [SerializeField] private int experience = 0;
        [SerializeField] private int experienceToNextLevel = 100;

        PlayerManager playerManager;
        public int playerLvl = 1;

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

        private void Update()
        {
            print(experience);
            print(playerLvl);
        }
        public void AddExperience(int amount)
        {
            experience += amount;
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
