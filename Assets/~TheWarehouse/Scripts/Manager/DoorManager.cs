using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheWarehouse
{
    public class DoorManager : MonoBehaviour
    {
        public void NextLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        

    }
}
