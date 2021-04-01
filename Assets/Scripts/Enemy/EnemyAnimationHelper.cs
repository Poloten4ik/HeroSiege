using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private Weapon weapon2;

        private Animator anim;
        private PlayerManager player;
        void Start()
        {
            anim = GetComponent<Animator>();
            player = PlayerManager.Instance;
        }

        public void MeleeAttackStart()
        {
            weapon.AttackStart();
            this.gameObject.transform.LookAt(player.transform.position);
            // print("attack start");
        }

        public void MeleeAttackEnd()
        {
            weapon.AttackEnd();
            anim.ResetTrigger("Attack");
            
            //  print("attack end");
        }
    }

}
