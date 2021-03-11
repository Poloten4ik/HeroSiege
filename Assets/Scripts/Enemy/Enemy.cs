using Assets.Scripts.Player;
using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Damageable))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [SerializeField] private float followRadius = 8f;
        [SerializeField] private float attackRadius = 2f;
        [SerializeField] private Transform castlePosition;

        private float distanceToPlayer;
        private float distanceToCaslte;
        Animator anim;
        Damageable damageable;
        PlayerMovement player;
        NavMeshAgent agent;


        private void Start()
        {
            anim = GetComponent<Animator>();
            player = PlayerMovement.Instance;
            damageable = GetComponent<Damageable>();
            agent = GetComponent<NavMeshAgent>();
            damageable.OnRecieveDamage += ReciveDamage;
            
        }

        private void Update()
        {
            UpdateTarget();
            
        }

        private void UpdateTarget()
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            distanceToCaslte = Vector3.Distance(transform.position, castlePosition.transform.position);
           
            if (distanceToPlayer > followRadius)
            {
                agent.SetDestination(castlePosition.transform.position);
            }
            else if (distanceToPlayer <= followRadius)
            {
                agent.SetDestination(player.transform.position);
                Attack();
            }
        }

        private void Attack()
        {
            if (distanceToPlayer < attackRadius)
            {
                anim.SetTrigger("Attack");
            }
        }

        private void ReciveDamage(int damage)
        {
            health -= damage;
            anim.SetTrigger("GetHit");

            if (health <= 0)
            {
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

