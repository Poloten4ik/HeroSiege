using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationHelper : MonoBehaviour
    {

        [SerializeField] private Weapon weapon;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private float updateMoveSpeed;

        private Animator anim;
        private bool checkCombo;
        private bool isAttacking;
        private void Start()
        {
            anim = GetComponent<Animator>();
            updateMoveSpeed = playerMovement.moveSpeed;
        }

        private void Update()
        {
            if (isAttacking)
            {
                return;
            }
        

            if (Input.GetMouseButtonDown(0))
            {
                //playerMovement.moveSpeed = 0;
                if (checkCombo)
                {
                    anim.SetTrigger("Combo");
                    print("combo");
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
            playerMovement.moveSpeed = updateMoveSpeed;
            print("end melee");
        }

        public void ComboStart()
        {
            checkCombo = true;
            print(checkCombo);
        }

        public void ComboEnd()
        {
            checkCombo = false;
            print(checkCombo);
        }
    }

}