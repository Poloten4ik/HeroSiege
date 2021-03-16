using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Castle
{
    public class CastleManager : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;
        public int health = 5000;

        private void Start()
        {
            damageable.OnRecieveDamage += ReciveDamage;
        }
        private void ReciveDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
