using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies
{
    public class ReciveDamageEnemy : MonoBehaviour
    {
        public Action OnHealthChange = delegate { };

        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private GameObject minimap;

        Damageable damageable;
        Enemy enemy;
        Animator animator;
        NavMeshAgent agent;
        LevelSystem levelSystem;
        Collider col;
        Rigidbody rb;
        PlayerManager player;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            enemy = GetComponent<Enemy>();
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            col = GetComponent<Collider>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
     
            player = PlayerManager.Instance;
            levelSystem = LevelSystem.Instance;
            damageable.OnReceiveDamage += ReciveDamage;
        }
        private void ReciveDamage(int damage)
        {
            
            enemy.health = enemy.health - damage < 0 ? 0 : enemy.health - damage;
            animator.SetTrigger("GetHit");
            ShowFloatingText(damage);
            OnHealthChange();
            if (enemy.health <= 0)
            {
                col.enabled = false;
                agent.enabled = false;
                enemy.enemyUI.SetActive(false);
                rb.detectCollisions = false;
                enemy.enabled = false;
                minimap.SetActive(false);
                levelSystem.AddExperience(enemy.exp);
                player.AddGold(enemy.exp);
                animator.SetTrigger("Dead");
                player.KilledEnemies(1);
                Destroy(gameObject, 5f);
            }
        }

        private void ShowFloatingText(int damage)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            go.GetComponentInChildren<TextMesh>().text = damage.ToString();
        }
    }

}
