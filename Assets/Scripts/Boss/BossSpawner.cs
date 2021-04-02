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
        [SerializeField] private GameObject cinemachine;

        [Header("Sounds")]
        public AudioSource audioSource;
        public AudioSource backgroundMusic;

        [SerializeField] private AudioClip bossSpawn;

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
                cinemachine.SetActive(true);

                bossUI.gameObject.SetActive(true);
                bossUI.Init(reciveDamageBoss);
                audioSource.PlayOneShot(bossSpawn);
                backgroundMusic.mute = true;
                StartCoroutine(CinemachineOff());
            }
        }

        private IEnumerator CinemachineOff()
        {
            yield return new WaitForSeconds(3);
            backgroundMusic.mute = false;
            cinemachine.SetActive(false);
        }

    }
}
