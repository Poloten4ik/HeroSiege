using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Damageable : MonoBehaviour
    {
        public Action<int> OnReceiveDamage = delegate { }; 
        public void DoDamage(int damage)
        {
            OnReceiveDamage(damage);
        }
    }
}