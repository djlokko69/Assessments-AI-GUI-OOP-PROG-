using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    [Space(3)]
    [Header("Levels")]
    public int levelLoad;
    public Scene nowScene;
    public bool ifPaused;
    [Space(5)]
    [Header("Options")]
    [Space(3)]
    public bool showOptions;
    [Header("Resolution")]
    public int index;
    public Vector2[] res;
    public bool fullScreen;
    [Header("Reference")]
    public AudioSource music;
    public float tempMusic;
    public bool muted;
    public Light brightness;
    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    // crouch
    // interact
    // sprint
    public KeyCode tempKey;
    [Header("Screen Elements")]
    public GameObject mainMenu;
    public GameObject options;
    
    public Dropdown resDropDown;
    public Toggle fullScreenTog;
    public Slider volumeSlider, brightnessSlider;
    public Text forwardText, backwardText, leftText, rightText, jumpText;


    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        nowScene = SceneManager.GetActiveScene();
        if(options != null)
        {
            #region Sliders
            brightness = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
            music = GameObject.Find("Music").GetComponent<AudioSource>();
            volumeSlider.value = music.volume;
            brightnessSlider.value = brightness.intensity;
            #endregion
            #region Keys
            // Forward
            forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
            forwardText.text = forward.ToString();
            // Backward
            backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
            backwardText.text = backward.ToString();
            // Left
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
            leftText.text = left.ToString();
            // Right
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
            rightText.text = right.ToString();
            // Jump
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
            jumpText.text = jump.ToString();
            #endregion
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nowScene.name == "Level_01")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
    #region Bound Keys
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None ||
            jump == KeyCode.None))
        {
            tempKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    public void Backward()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None ||
            jump == KeyCode.None))
        {
            tempKey = backward;
            backward = KeyCode.None;
            backwardText.text = backward.ToString();
        }
    }
    public void Left()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None ||
            jump == KeyCode.None))
        {
            tempKey = left;
            left = KeyCode.None;
            leftText.text = left.ToString();
        }
    }
    public void Right()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None ||
            jump == KeyCode.None))
        {
            tempKey = right;
            right = KeyCode.None;
            rightText.text = right.ToString();
        }
    }
    public void Jump()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None ||
            left == KeyCode.None))
        {
            tempKey = jump;
            jump = KeyCode.None;
            jumpText.text = jump.ToString();
        }
    }
    #endregion

    public void ChangeVolume()
    {
        music.volume = volumeSlider.value;
    }
   
    public void ChangeBright()
    {
        brightness.intensity = brightnessSlider.value;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(levelLoad);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
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
    public void ResolutionChange()
    {
        index = resDropDown.value;
        Screen.SetResolution((int)res[index].x, (int)res[index].y, fullScreen);
    }

    public void ToggleFullScreen()
    {
        if (fullScreen)
        {
            fullScreen = false;
            Screen.fullScreen = false;
            fullScreenTog.isOn = false;
        }
        else
        {
            fullScreen = true;
            Screen.fullScreen = true;
            fullScreenTog.isOn = true;
        }
    }
    public void FullscreenToggle()
    {
        ToggleFullScreen();
    }
    void OnGUI()
    {
        if (options != null)
        {
            #region NewKey
            Event e = Event.current;

            if(forward== KeyCode.None)
            {
                if (e.isKey)
                {
                    Debug.Log("Key Code:" + e.keyCode);
                    if (!(e.keyCode == backward || e.keyCode == left ||
                        e.keyCode == right || e.keyCode == jump))
                    {
                        forward = e.keyCode;
                        tempKey = KeyCode.None;
                        forwardText.text = forward.ToString();
                    }
                    else
                    {
                        forward = tempKey;
                        tempKey = KeyCode.None;
                        forwardText.text = forward.ToString();
                    }
                }
            }
            if (backward == KeyCode.None)
            {
                if (e.isKey)
                {
                    Debug.Log("Key Code:" + e.keyCode);
                    if (!(e.keyCode == forward || e.keyCode == left ||
                        e.keyCode == right || e.keyCode == jump))
                    {
                        backward = e.keyCode;
                        tempKey = KeyCode.None;
                        backwardText.text = backward.ToString();
                    }
                    else
                    {
                        backward = tempKey;
                        tempKey = KeyCode.None;
                        backwardText.text = backward.ToString();
                    }
                }
            }
            if (left == KeyCode.None)
            {
                if (e.isKey)
                {
                    Debug.Log("Key Code:" + e.keyCode);
                    if (!(e.keyCode == forward || e.keyCode == backward ||
                        e.keyCode == right || e.keyCode == jump))
                    {
                        left = e.keyCode;
                        tempKey = KeyCode.None;
                        leftText.text = left.ToString();
                    }
                    else
                    {
                        left = tempKey;
                        tempKey = KeyCode.None;
                        leftText.text = left.ToString();
                    }
                }
            }
            if (right == KeyCode.None)
            {
                if (e.isKey)
                {
                    Debug.Log("Key Code:" + e.keyCode);
                    if (!(e.keyCode == forward || e.keyCode == backward ||
                        e.keyCode == left || e.keyCode == jump))
                    {
                        right = e.keyCode;
                        tempKey = KeyCode.None;
                        rightText.text = right.ToString();
                    }
                    else
                    {
                        right = tempKey;
                        tempKey = KeyCode.None;
                        rightText.text = right.ToString();
                    }
                }
            }
            if (jump == KeyCode.None)
            {
                if (e.isKey)
                {
                    Debug.Log("Key Code:" + e.keyCode);
                    if (!(e.keyCode == forward || e.keyCode == backward ||
                        e.keyCode == right || e.keyCode == left))
                    {
                        jump = e.keyCode;
                        tempKey = KeyCode.None;
                        jumpText.text = jump.ToString();
                    }
                    else
                    {
                        jump = tempKey;
                        tempKey = KeyCode.None;
                        jumpText.text = jump.ToString();
                    }
                }
            }
            #endregion
        }
    }
    public void SaveOp()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());

    }
    public void TogglePauseMenu()
    {
        TogglePause();
    }
    bool TogglePause()
    {
        if (ifPaused == true)
        {
            ifPaused = false;
            mainMenu.SetActive(false);
            Time.timeScale = 1;
            return false;
        }
        else
        {
            ifPaused = true;
            mainMenu.SetActive(true);
            Time.timeScale = 0;
            return true;
        }
    }
}
