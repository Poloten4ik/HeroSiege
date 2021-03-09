using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        private Animator anim;
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void MeleeAttackStart()
        {
            weapon.AttackStart();
            print("attack start");
        }

        public void MeleeAttackEnd()
        {
            weapon.AttackEnd();
            anim.ResetTrigger("Attack");
            print("attack end");
        }
    }

}
