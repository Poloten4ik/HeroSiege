using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimationHelper : MonoBehaviour
    {

        [SerializeField] private Weapon weapon;
        [SerializeField] private PlayerManager playerMovement;
        [SerializeField] private float updateMoveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private LayerMask damageLayer;
        [SerializeField] private float comboRadius;
        [SerializeField] public int slamDamage;
        [SerializeField] private ParticleSystem slamEffect;
        [SerializeField] private ParticleSystem attackEffect;
        [SerializeField] private ParticleSystem attackEffect2;

        private Animator anim;
        private bool checkCombo;
        private bool attack2;
        private bool isAttacking;

        private void Start()
        {
            anim = GetComponent<Animator>();
            updateMoveSpeed = playerMovement.moveSpeed;
            rotationSpeed = playerMovement.rotationSpeed;
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

        public void StopMovespeed()
        {
            playerMovement.moveSpeed = 0;
            playerMovement.rotationSpeed = 0;
        }

        public void StartMovespeed()
        {
            playerMovement.moveSpeed = updateMoveSpeed;
            playerMovement.rotationSpeed = rotationSpeed;
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

            // print("end melee");
        }

        public void Attack2Start()
        {
            attack2 = true;
            isAttacking = true;
        }

        public void Attack2End()
        {
            attack2 = false;
            isAttacking = false;
        }

        public void ComboStart()
        {
            checkCombo = true;
            isAttacking = true;
            print("combostart");
        }

        public void ComboEnd()
        {
            checkCombo = false;
            isAttacking = false;
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

        public void SlamEnd()
        {
            anim.ResetTrigger("Attack");
            anim.ResetTrigger("Combo");
            print("combo");
        }

        public void AttackEffect()
        {
            attackEffect.Play();
        }

        public void Attack2Effect() 
        {
            attackEffect2.Play();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, comboRadius);

        }
    }

}