using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Prefabs")]
        [SerializeField] private List<GameObject> enemyPrefabs;

        [Header("Wave parameters")]
        [SerializeField] private float spawnDuration;
        [SerializeField] private float nextWaveDelay;
        [SerializeField] int numberOfEnemies;

        private void Start()
        {
            StartCoroutine(EnemySpawn());
        }

        private IEnumerator EnemySpawn()
        {
            int countOfEnemies = 0;
            int wave = 0;
            while (numberOfEnemies > countOfEnemies && wave != enemyPrefabs.Count)
            {
                yield return new WaitForSeconds(spawnDuration);
                Instantiate(enemyPrefabs[wave], transform.position, transform.rotation);
                countOfEnemies += 1;

                if (numberOfEnemies == countOfEnemies)
                {
                    countOfEnemies = 0;
                    wave++;
                    yield return new WaitForSeconds(nextWaveDelay);
                }
            }
        }
    }
}
