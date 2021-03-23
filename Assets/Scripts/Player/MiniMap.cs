using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField] private float distance = 100f;

        PlayerManager player;
        void Start()
        {
            player = PlayerManager.Instance;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = player.transform.position + Vector3.up * distance;
        }

    }
}
