using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Damageable : MonoBehaviour
    {
        public Action<int> OnRecieveDamage = delegate { };
        public void DoDamage(int damage)
        {
            OnRecieveDamage(damage);
        }
    }
}