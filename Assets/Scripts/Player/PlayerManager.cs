using Assets.Scripts.Enemies;
using Assets.Scripts.Fountain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{

    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Action OnHealthChange = delegate { };
        public Action OnGoldChange = delegate { };
        public Action EnemyKilled = delegate { };

        [Header("Movement config")]
        public float moveSpeed = 10f;

        [Header("Rotation config")]
        public float rotationSpeed = 1f;

        [Header("Gravity")]
        [SerializeField] private float gravityScale = 1;

        [Header("References")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Animator anim;
        [SerializeField] private Damageable damageable;
        [SerializeField] private LevelSystem levelSystem;
        [SerializeField] private ParticleSystem levelUpEffect;
        [SerializeField] private FountainManager fountainManager;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private Text currentLvlText;
        [SerializeField] private Text damageText;
        [SerializeField] private Weapon weapon;
        

        [Header("Sounds")]
        public AudioSource audioSource;
        [SerializeField] private AudioClip lvlUp;
     

        [Header("Player Options")]
        [SerializeField] private int currentLvl = 1;
        public int currentGold = 0;
        public bool isDead = false;
        public int killedEnemies = 0;

        [Header("Player Health")]
        public int maxHealth = 100;
        public float currentHealth;
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
            currentLvlText.text = currentLvl.ToString();
            damageText.text = weapon.damage.ToString();

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
            if (!isDead)
            {
                Move();
            }

            isPlayerDead();
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
            currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
            OnHealthChange();
        }


        private void isPlayerDead()
        {
            if (currentHealth <= 0)
            {
                isDead = true;
                anim.SetTrigger("Dead");
                controller.enabled = false;
                Destroy(gameObject, 4f);
                StartCoroutine(LoseScreen());
            }
        }

        public IEnumerator LoseScreen()
        {
            yield return new WaitForSeconds(3);
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void LevelUp(int lvl)
        {
            if (currentLvl < lvl)
            {
                currentLvl = lvl;
                currentLvlText.text = currentLvl.ToString();
                maxHealth += 200;
                levelUpEffect.Play();
                audioSource.PlayOneShot(lvlUp);
            }
        }

        public void AddGold(int reward)
        {
            currentGold += reward;
            OnGoldChange();
        }

        public void KilledEnemies(int count)
        {
            killedEnemies += count;
            EnemyKilled();
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
