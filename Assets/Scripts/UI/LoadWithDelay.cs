using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI
{
    public class LoadWithDelay : MonoBehaviour
    {
        public Animator animator;
        public void LoadSceneDelay()
        {
            StartCoroutine(LoadSceneWithDelay());
            animator.SetTrigger("Start");
        }

        public IEnumerator LoadSceneWithDelay()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }
}

