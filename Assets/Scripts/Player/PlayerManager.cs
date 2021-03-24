using Assets.Scripts.Enemy;
using Assets.Scripts.Fountain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{

    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Action OnHealthChange = delegate { };
        public Action OnGoldChange = delegate { };

        [Header("Movement config")]
        public float moveSpeed = 10f;

        [Header("Rotation config")]
        [SerializeField] private float rotationSpeed = 1f;

        [Header("Gravity")]
        [SerializeField] private float gravityScale = 1;

        [Header("References")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Animator anim;
        [SerializeField] private Damageable damageable;
        [SerializeField] private LevelSystem levelSystem;
        [SerializeField] private ParticleSystem levelUpEffect;
        [SerializeField] private FountainManager fountainManager;

        [Header("Player Options")]
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private int currentLvl = 1;
        public int currentGold = 0;

        [Header("Player Health")]
        public int maxHealth = 100;
        public int currentHealth;
        public int healthRegeneration = 1;
        public bool isHealingOn;

        private Camera mainCamera;
        private float gravity;
        public void DoDamage()
        {
            anim.SetTrigger("Death");
        }

        private void Start()
        {
            mainCamera = Camera.main;
            damageable.OnReceiveDamage += ReceiveDamage;
            currentHealth = maxHealth;

            StartCoroutine(HealthRegeneration());
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            Move();
            anim.SetFloat("AttackSpeed", attackSpeed);
        }

        private void Move()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = mainCamera.transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 moveDirection = forward * inputV + right * inputH;

            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }

            if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
            {
                anim.SetBool("Running", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
            }
            else
            {
                anim.SetBool("Running", false);
            }

            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;
            moveDirection.y = gravity;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void ReceiveDamage(int damage)
        {
            currentHealth -= damage;
            OnHealthChange();
        }

        public void LevelUp(int lvl)
        {
            if (currentLvl < lvl)
            {
                attackSpeed += 0.1f;
                currentLvl = lvl;
                levelUpEffect.Play();
            }
        }

        public void AddGold(int reward)
        {
            currentGold += reward;
            OnGoldChange();
        }

        public IEnumerator HealthRegeneration()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1);
                if (currentHealth >= maxHealth)
                {
                    fountainManager.healEffect.Stop();
                    continue;
                }
                currentHealth = currentHealth + healthRegeneration > maxHealth ? maxHealth : currentHealth + healthRegeneration;
                OnHealthChange();
            }
        }
    }
}
