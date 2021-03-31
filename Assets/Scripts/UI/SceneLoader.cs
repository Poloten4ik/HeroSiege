using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class SceneLoader : MonoBehaviour
    {
      
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
            Time.timeScale = 1;
        }
    }
}
