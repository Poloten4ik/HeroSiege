using Assets.Scripts.Enemies;
using Assets.Scripts.Sound;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Boss
{
    public class ReciveDamageBoss : MonoBehaviour
    {
        public Action OnHealthChange = delegate { };

        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private BossUI bossUI;

        public AudioClip[] getHit;
        public Enemy enemy;

        AudioManager audioSource;
        Animator animator;
        Damageable damageable;
        NavMeshAgent agent; 
        CharacterController controller; 
        BossSpawner bossSpawner;
        WinningScene winningScene;

        private void Awake()
        {

            animator = GetComponent<Animator>();
            damageable = GetComponent<Damageable>();
            agent = GetComponent<NavMeshAgent>();
            controller = GetComponent<CharacterController>();

        }

        private void Start()
        {
            audioSource = AudioManager.Instance;
            bossSpawner = BossSpawner.Instance;
            damageable.OnReceiveDamage += ReciveDamage;
            winningScene = WinningScene.Instance;
        }
        private void ReciveDamage(int damage)
        {

            enemy.health = enemy.health - damage < 0 ? 0 : enemy.health - damage;
            animator.SetTrigger("GetHit");
            ShowFloatingText(damage);

            int random = UnityEngine.Random.Range(0, getHit.Length);
            audioSource.PlaySound(getHit[random]);
            OnHealthChange();

            if (enemy.health <= 0)
            {
                agent.enabled = false;
                controller.enabled = false;
                enemy.enabled = false;
                bossSpawner.bossUI.gameObject.SetActive(false);
                animator.SetTrigger("Dead");
                Time.timeScale = 0.5f;
                winningScene.WinningCinemashine();
            }
        }

        private void ShowFloatingText(int damage)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            go.GetComponentInChildren<TextMesh>().text = damage.ToString();
        }
    }
}
