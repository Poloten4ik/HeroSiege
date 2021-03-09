using Assets.Scripts.Player;
using Pathfinding;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Damageable))]

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [SerializeField] private float followRadius = 8f;
        [SerializeField] private float attackRadius = 2f;
        [SerializeField] private Transform castlePosition;
        [SerializeField] private AIDestinationSetter aIDestinationSetter;


        private float distanceToPlayer;
        private float distanceToCaslte;
        Animator anim;
        Damageable damageable;
        PlayerMovement player;


        private void Start()
        {
            anim = GetComponent<Animator>();
            player = PlayerMovement.Instance;
            damageable = GetComponent<Damageable>();
            
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
                aIDestinationSetter.target = castlePosition;
            }
            else if (distanceToPlayer < followRadius)
            {
                aIDestinationSetter.target = player.transform;
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

