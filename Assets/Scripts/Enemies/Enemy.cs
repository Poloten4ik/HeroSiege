using Assets.Scripts.Enums;
using Assets.Scripts.Player;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(Damageable))]

    public class Enemy : MonoBehaviour
    {
      
        [Header("Enemy Options")]
        [SerializeField] private float followRadius = 8f;
        [SerializeField] private float attackRadius = 2f;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float attackSpeed = 1f;
        public int health = 100;
        public int exp = 20;
        public int reward = 3;

        [Header("References")]
        [SerializeField] private Transform castlePosition;
        public GameObject enemyUI;

        private float distanceToPlayer;
        private float distanceToCaslte;
        private float nextAttack;
        Animator anim;
    
        PlayerManager player;
        NavMeshAgent agent;
        LevelSystem levelSystem;
        Collider col;
        Rigidbody rb;


        public int allEnemiesDead;

        public EnemyState activeState;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            col = GetComponent<Collider>();

        }
        private void Start()
        {
            
            player = PlayerManager.Instance;   
            agent.speed = moveSpeed;
           
            ChangeState(EnemyState.MOVE_TO_CASTLE);
        }


        private void Update()
        {
            //UpdateTarget();

            if (!player.isDead)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            }

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
            if (!player.isDead && distanceToPlayer <= followRadius)
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
            if (!player.isDead)
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
            else if (player.isDead)
            {
                ChangeState(EnemyState.MOVE_TO_CASTLE);
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
                this.gameObject.transform.LookAt(castlePosition);
            }

            if (distanceToPlayer <= followRadius)
            {
                ChangeState(EnemyState.MOVE_TO_PLAYER);
            }

        }

        private void AttackPlayer()
        {
            if (!player.isDead)
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

            else
            {
                ChangeState(EnemyState.MOVE_TO_CASTLE);
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

