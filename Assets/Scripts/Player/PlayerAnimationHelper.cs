using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationHelper : MonoBehaviour
    {

        [SerializeField] private Weapon weapon;
        [SerializeField] private PlayerManager playerMovement;
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
                playerMovement.moveSpeed = 0.5f;
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
            playerMovement.moveSpeed = 0;
            print(checkCombo);
        }

        public void ComboEnd()
        {
            checkCombo = false;
            playerMovement.moveSpeed = updateMoveSpeed;
            print(checkCombo);
        }
    }

}