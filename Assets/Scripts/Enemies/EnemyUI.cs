using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Enemies
{
    public class EnemyUI : MonoBehaviour
    {

        public Slider enemyHealth;
        Enemy enemy;
        ReciveDamageEnemy reciveDamageEnemy;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            reciveDamageEnemy = GetComponent<ReciveDamageEnemy>();
        }

        private void Start()
        {
            reciveDamageEnemy.OnHealthChange += UpdateHealth;
            enemyHealth.maxValue = enemy.health;
            enemyHealth.value = enemy.health;

        }

        private void Update()
        {
            enemyHealth.transform.LookAt(Camera.main.transform);
        }

        public void UpdateHealth()
        {
            enemyHealth.value = enemy.health;
        }
    }
}
