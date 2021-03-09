﻿using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
   
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance { get; private set; }

        [Header("Movement config")]
        [SerializeField] private float moveSpeed = 10f;

        [Header("Rotation config")]
        [SerializeField] private float rotationSpeed = 1f;

        [Header("Gravity")]
        [SerializeField] private float gravityScale = 1;

        [Header("References")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Animator anim;
        [SerializeField] private Damageable damageable;

        [Header("Player Options")]
        [SerializeField] private int health = 100;

        private Camera mainCamera;
        private float gravity;
        public void DoDamage()
        {
            anim.SetTrigger("Death");
        }

        private void Start()
        {
            mainCamera = Camera.main;
            damageable.OnRecieveDamage += ReciveDamage;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = mainCamera.transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 moveDirection = forward * inputV + right * inputH;

            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }

            if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
            {
                anim.SetBool("Running", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
            }
            else
            {
                anim.SetBool("Running", false);
            }

            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;
            moveDirection.y = gravity;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void ReciveDamage(int damage)
        {
            health -= damage;
            print(health);
        }
    }

}
