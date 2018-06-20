using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheWarehouse
{
    public class PauseMenu : MonoBehaviour
    {
        #region Variables
        public Transform canvas;
        public GameObject player;
        //public Player.Camera.FirstPerson.MouseLook mainCam;
        // public GameManager main;
        public bool showOptions;
        public GameObject mainMenu;
        public GameObject options;
        #endregion

        // Use this for initialization
        void Start()
        {
           // main = GameObject.FindGameObjectWithTag("Menu").GetComponent<GameManager>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
                
            }
        }

        public void TogglePause()
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;// hides mouse and looks it in middle of screen
                Cursor.visible = true;
                
            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;
                Cursor.lockState = CursorLockMode.None;// shows mouse and lets it run free
                Cursor.visible = false;
            }
        }
        #region ToggleOp
        public void ToggleOp()
        {
            OpToggle();
        }
        bool OpToggle()
        {
            if (showOptions)
            {
                showOptions = false;
                mainMenu.SetActive(true);
                options.SetActive(false);

                return false;
            }
            else
            {
                showOptions = true;
                mainMenu.SetActive(false);
                options.SetActive(true);
                return true;
            }
        }
        #endregion
        #region Save
        public void SaveOp()
        {
            /*PlayerPrefs.SetString("Forward", forward.ToString());
            PlayerPrefs.SetString("Backward", backward.ToString());
            PlayerPrefs.SetString("Left", left.ToString());
            PlayerPrefs.SetString("Right", right.ToString());
            PlayerPrefs.SetString("Jump", jump.ToString());


            PlayerPrefs.SetFloat("Music", volumeSlider.value);*/

        }
        #endregion
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
