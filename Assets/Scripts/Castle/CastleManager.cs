using Assets.Scripts.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Castle
{
    public class CastleManager : MonoBehaviour
    {
        public Action OnHealthChange = delegate { };

        [SerializeField] private Damageable damageable;
        public int health = 5000;

        private void Start()
        {
            damageable.OnReceiveDamage += ReceiveDamage;
        }
        private void ReceiveDamage(int damage)
        {
            health -= damage;
            OnHealthChange();

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
