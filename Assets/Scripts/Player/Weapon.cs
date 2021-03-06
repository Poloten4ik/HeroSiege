using Assets.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private int damage = 40;

        //[SerializeField] private ParticleSystem attackEffect;

        private Collider col;
        private void Start()
        {
            col = GetComponent<Collider>();
            col.enabled = false;
        }

        public void AttackStart()
        {
            col.enabled = true;
            //attackEffect.Play();
        }

        public void AttackEnd()
        {
            col.enabled = false;
            //attackEffect.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            print("trigger" + other.name);
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.DoDamage(damage);
            }
        }
    }
}