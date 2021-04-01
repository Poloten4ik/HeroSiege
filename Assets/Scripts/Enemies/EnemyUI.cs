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

        private void Start()
        {
            enemy = GetComponent<Enemy>();
            enemy.OnHealthChange += UpdateHealth;

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
