using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Castle
{
    public class CastleUI : MonoBehaviour
    {
        public Slider castleHealth;

        CastleManager castleManager;
        void Start()
        {
            castleManager = GetComponent<CastleManager>();
            castleManager.OnHealthChange += UpdateHealth;

            castleHealth.maxValue = castleManager.health;
            castleHealth.value = castleManager.health;
        }

        private void Update()
        {
            castleHealth.transform.LookAt(Camera.main.transform);
        }

        public void UpdateHealth()
        {
            castleHealth.value = castleManager.health;
        }
    }
}

