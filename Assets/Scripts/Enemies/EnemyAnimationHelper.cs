using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Enemies
{
    public class EnemyAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private Weapon weapon2;
        Enemy enemy;

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
        }

        public void MeleeAttackEnd()
        {
            weapon.AttackEnd();
            anim.ResetTrigger("Attack");
        }

        public void MoveUnderground()
        {
            Vector3 under = new Vector3(transform.position.x, transform.position.y - 1.3f, transform.position.z);
            transform.DOMove(under, 3f);
        }
    }

}
