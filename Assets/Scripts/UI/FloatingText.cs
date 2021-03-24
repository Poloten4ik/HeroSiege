using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 3f;
        [SerializeField] private Vector3 randomizeIntensity = new Vector3 (2, 2, 0);

        void Start()
        {
            Destroy(gameObject, destroyTime);
          
            transform.localPosition += new Vector3
            (
            Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            (randomizeIntensity.y),
            (randomizeIntensity.z)
            );


        }    

    }
}
