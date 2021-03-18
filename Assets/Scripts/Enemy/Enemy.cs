using Assets.Scripts.Enums;
using Assets.Scripts.Player;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Damageable))]

    public class Enemy : MonoBehaviour
    {
        public Action OnHealthChange = delegate { };

        [Header("Enemy Options")]
        [SerializeField] private int exp = 20;
        [SerializeField] private int reward = 3;
        [SerializeField] private float followRadius = 8f;
        [SerializeField] private float attackRadius = 2f;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float attackSpeed = 1f;
        public int health = 100;

        [Header("References")]
        [SerializeField] private Transform castlePosition;

        private float distanceToPlayer;
        private float distanceToCaslte;
        private float nextAttack;
        Animator anim;
        Damageable damageable;
        PlayerManager player;
        NavMeshAgent agent;
        LevelSystem levelSystem;
        Collider col;

        public EnemyState activeState;

        private void Start()
        {
            anim = GetComponent<Animator>();
            levelSystem = LevelSystem.Instance;
            player = PlayerManager.Instance;
            damageable = GetComponent<Damageable>();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            damageable.OnReceiveDamage += ReciveDamage;
            col = GetComponent<Collider>();

            ChangeState(EnemyState.MOVE_TO_CASTLE);
        }


        private void Update()
        {
            //UpdateTarget();

            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            distanceToCaslte = Vector3.Distance(transform.position, castlePosition.transform.position);

            switch (activeState)
            {
                case EnemyState.MOVE_TO_CASTLE:
                    MoveToCastle();
                    break;
                case EnemyState.ATTACK_CASTLE:
                    AttackCastle();
                    break;
                case EnemyState.MOVE_TO_PLAYER:
                    MoveToPlayer();
                    break;
                case EnemyState.ATTACK_PLAYER:
                    AttackPlayer();
                    break; 

                case EnemyState.SPELL_CAST:

                    break;
                case EnemyState.DEATH:

                    break;
            }
        }

        public void ChangeState(EnemyState newState)
        {
            switch (newState)
            {
                case EnemyState.MOVE_TO_CASTLE:
                    agent.SetDestination(castlePosition.transform.position);
                    break;

                case EnemyState.MOVE_TO_PLAYER:

                    break;

                case EnemyState.ATTACK_PLAYER:
                  
                    break;

                case EnemyState.SPELL_CAST:
                    break;

                case EnemyState.DEATH:
                    break;
            }
            activeState = newState;
        }

        private void MoveToCastle()
        {
            anim.SetBool("Attacking", false);
            agent.speed = moveSpeed;
            if (distanceToPlayer <= followRadius)
            {
                ChangeState(EnemyState.MOVE_TO_PLAYER);
            }

            else if (distanceToCaslte < attackRadius)
            {
                ChangeState(EnemyState.ATTACK_CASTLE);
            }
        }


        private void MoveToPlayer()
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Attacking", false);
            agent.speed = moveSpeed;

            if (distanceToPlayer >= followRadius)
            {
                ChangeState(EnemyState.MOVE_TO_CASTLE);
            }
            else if (distanceToPlayer < attackRadius)
            {
                ChangeState(EnemyState.ATTACK_PLAYER);
            }
           
        }

        private void AttackCastle()
        {
            anim.SetBool("Attacking", true);
            agent.speed = default;

            nextAttack -= Time.deltaTime;
            if (nextAttack <= 0)
            {
                nextAttack = attackSpeed;
                anim.SetTrigger("Attack");
            }

            if (distanceToPlayer <= followRadius)
            {
                ChangeState(EnemyState.MOVE_TO_PLAYER);
            }

        }

        private void AttackPlayer()
        {
            anim.SetBool("Attacking", true);          
            agent.speed = default;

            nextAttack -= Time.deltaTime;
            if (nextAttack <= 0)
            {
                nextAttack = attackSpeed;
                anim.SetTrigger("Attack");
            }
            if (distanceToPlayer > attackRadius)
            {
                ChangeState(EnemyState.MOVE_TO_PLAYER);
            }
        }

        private void ReciveDamage(int damage)
        {
            health -= damage;
            anim.SetTrigger("GetHit");
            OnHealthChange();
            if (health <= 0)
            {
                col.enabled = false;
                levelSystem.AddExperience(exp);
                player.AddGold(reward);
                anim.SetTrigger("Dead");
                Destroy(gameObject, 1f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, followRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }

    }
}

