using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Castle
{
    public class CastleUI : MonoBehaviour
    {
        public Slider castleHealth;
        public Text maxHpText;
        public Text currentHPText;

        CastleManager castleManager;
        void Start()
        {
            castleManager = GetComponent<CastleManager>();
            castleManager.OnHealthChange += UpdateHealth;

            castleHealth.maxValue = castleManager.health;
            castleHealth.value = castleManager.health;

            maxHpText.text = castleHealth.maxValue.ToString();
            currentHPText.text = castleHealth.value.ToString();
        }
        public void UpdateHealth()
        {
            castleHealth.value = castleManager.health;
            currentHPText.text = castleHealth.value.ToString();
        }
    }
}

