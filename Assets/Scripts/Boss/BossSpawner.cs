using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Boss
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private BossUI bossUI;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private PlayerManager player;


        private void Start()
        {
            player.EnemyKilled += SpawnBoss;
        }

        private void SpawnBoss()
        {
            if (player.killedEnemies == enemySpawner.allNumberOfEnemies)
            {
                var obj = Instantiate(bossPrefab, transform.position, transform.rotation);
                var enemy = obj.GetComponent<Enemy>();

                bossUI.gameObject.SetActive(true);
                bossUI.Init(enemy);
            }
        }
    }
}
