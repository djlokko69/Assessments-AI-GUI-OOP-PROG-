using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    #region
    public Transform canvas;
    public GameObject player;
    //public Player.Camera.FirstPerson.MouseLook mainCam;

    #endregion

    // Use this for initialization
    void Start()
    {
        
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

    
}
