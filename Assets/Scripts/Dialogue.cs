using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NPC/Dialogue")]
public class Dialogue : MonoBehaviour
{
    [Header("References")]
    public bool showDlg;
    public int index, optionsIndex;
    public GameObject player;
    public Player.Camera.FirstPerson.MouseLook mainCam;
    [Header("NPC Name and Dialogue")]
    public string npcName;

    private string[] text;

    public string[] negText, neutext, posText;
    public int approval;
    public string response1, response2;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player.Camera.FirstPerson.MouseLook>();

        text = new string[5];
        for (int i = 0; i < text.Length; i++)
        {
            text[i] = neutext[i];
        }
    }
    void Convo()
    {
        if (approval <= -1) { text = negText; }
        if (approval == 0) { text = neutext; }
        if (approval >= 1) { text = posText; }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        if (showDlg)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            GUI.Box(new Rect(0, 6 * scrH, Screen.width, 3 * scrH), npcName + ":" + text[index]);
            if (!(index + 1 >= text.Length || index == optionsIndex))
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Next"))
                {
                    index++;
                }
            }
            else if (index == optionsIndex)
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), response1))
                {
                    approval++;
                    Convo();
                    index++;
                }
                if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), response2))
                {
                    approval--;
                    Convo();
                    index++;
                }
            }
            else
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Bye."))
                {
                    showDlg = false;
                    index = 0;
                    mainCam.enabled = true;
                    player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;
                    player.GetComponent<PlayerCon>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}
