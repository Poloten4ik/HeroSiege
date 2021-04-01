using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Boss
{
    public class BossSpawner : MonoBehaviour
    {
        public static BossSpawner Instance { get; private set; }

        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private PlayerManager player;

        public BossUI bossUI;

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
            player.EnemyKilled += SpawnBoss;
        }

        private void SpawnBoss()
        {
            if (player.killedEnemies == enemySpawner.allNumberOfEnemies)
            {
                var obj = Instantiate(bossPrefab, transform.position, transform.rotation);
                var reciveDamageBoss = obj.GetComponent<ReciveDamageBoss>();

                bossUI.gameObject.SetActive(true);
                bossUI.Init(reciveDamageBoss);
            }
        }
    }
}
