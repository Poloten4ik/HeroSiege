using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemies;
using UnityEngine.UI;

namespace Assets.Scripts.Boss
{
    public class BossUI : MonoBehaviour
    {
        public Slider enemyHealth;
        public Text maxHp;
        public Text currentHp;

        public void Init(Enemy enemy)
        {
            enemy.OnHealthChange += () => UpdateHealth(enemy);

            enemyHealth.maxValue = enemy.health;
            enemyHealth.value = enemy.health;

            maxHp.text = enemy.health.ToString();
            currentHp.text = enemy.health.ToString();

        }

        public void UpdateHealth(Enemy enemy)
        {
            enemyHealth.value = enemy.health;
            currentHp.text = enemy.health.ToString();
        }
    }
}

