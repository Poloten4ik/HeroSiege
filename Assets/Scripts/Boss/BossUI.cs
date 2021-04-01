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

        ReciveDamageBoss reciveDamageBoss;
       
        private void Start()
        {
            reciveDamageBoss = GetComponent<ReciveDamageBoss>();
        }

        public void Init(ReciveDamageBoss reciveDamageBoss)
        {
            reciveDamageBoss.OnHealthChange += () => UpdateHealth(reciveDamageBoss);

            enemyHealth.maxValue = reciveDamageBoss.enemy.health;
            enemyHealth.value = reciveDamageBoss.enemy.health;

            maxHp.text = reciveDamageBoss.enemy.health.ToString();
            currentHp.text = reciveDamageBoss.enemy.health.ToString();
        }

        public void UpdateHealth(ReciveDamageBoss reciveDamageBoss)
        {
            enemyHealth.value = reciveDamageBoss.enemy.health;
            currentHp.text = reciveDamageBoss.enemy.health.ToString();
        }

        
    }
}

