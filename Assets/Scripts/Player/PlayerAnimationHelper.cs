using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationHelper : MonoBehaviour
    {

        [SerializeField] private Weapon weapon;

        private Animator anim;
        private bool checkCombo;
        private bool isAttacking;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isAttacking)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (checkCombo)
                {
                    anim.SetTrigger("Attack2");
                }
                else
                {
                    anim.SetTrigger("Attack");
                }
            }
        }

        public void MeleeAttackStart()
        {
            isAttacking = true;
            weapon.AttackStart();
            print("start melee");
        }

        public void MeleeAttackEnd()
        {
            isAttacking = false;
            weapon.AttackEnd();
            anim.ResetTrigger("Attack");
            print("end melee");
        }

        public void ComboStart()
        {
            checkCombo = true;
            print("start combo");   
        }

        public void ComboEnd()
        {
            checkCombo = false;
            print("end combo");
        }
    }

}