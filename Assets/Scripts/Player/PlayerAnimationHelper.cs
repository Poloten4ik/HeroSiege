using Assets.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationHelper : MonoBehaviour
    {

        [SerializeField] private Weapon weapon;
        [SerializeField] private PlayerManager playerMovement;
        [SerializeField] private float updateMoveSpeed;
        [SerializeField] private LayerMask damageLayer;
        [SerializeField] private float comboRadius;
        [SerializeField] private int slamDamage;
        [SerializeField] private ParticleSystem slamEffect;

        private Animator anim;
        private bool checkCombo;
        private bool attack2;
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

                }
                else if (attack2)
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
         //  print("start melee");
        }

        public void MeleeAttackEnd()
        {
            isAttacking = false;
            weapon.AttackEnd();
            anim.ResetTrigger("Attack");
            playerMovement.moveSpeed = updateMoveSpeed;
           // print("end melee");
        }

        public void Attack2Start()
        {
            attack2 = true;
            playerMovement.moveSpeed = 0;
            print(attack2);
        }

        public void Attack2End()
        {
            attack2 = false;
            playerMovement.moveSpeed = updateMoveSpeed;
            print(attack2);
        }

        public void ComboStart()
        {
            checkCombo = true;
            playerMovement.moveSpeed = 0;
            //print(checkCombo);
        }

        public void ComboEnd()
        {
            checkCombo = false;
            playerMovement.moveSpeed = updateMoveSpeed;
           // print(checkCombo);
        }

        public void Slam()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, comboRadius, damageLayer);

            foreach (Collider col in colliders)
            {
                Damageable damageable = col.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.DoDamage(slamDamage);
                }
            }
            slamEffect.Play();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, comboRadius);

        }
    }

}