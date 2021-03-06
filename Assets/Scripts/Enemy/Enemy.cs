using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Damageable))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private int health = 100;

        Animator anim;
        Damageable damageable;

        private void Start()
        {
            anim = GetComponent<Animator>();
            damageable = GetComponent<Damageable>();

            damageable.OnRecieveDamage += ReciveDamage;
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
    }
}

