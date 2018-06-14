using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [AddComponentMenu("NPC/Dialogue")]
    public class Dialogue : MonoBehaviour
    {
        #region Variables
        [Header("References")]
        public bool showDlg;
        public int index, optionsIndex;
        public GameObject player;
        public Player.Camera.FirstPerson.MouseLook mainCam;
        [Header("Texture")]
        public GUIStyle dlgBx, nextBut, opBut1, opbut2, exitBut;
        [Header("NPC Name and Dialogue")]
        public string npcName;

        private string[] text;

        public string[] negText, neutext, posText;
        public int approval;
        public string response1, response2;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {
            #region GameObject
            player = GameObject.FindGameObjectWithTag("Player");
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player.Camera.FirstPerson.MouseLook>();
            #endregion
            #region Text
            text = new string[5];
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = neutext[i];
            }
            #endregion
        }
        #endregion
        #region Convo
        void Convo()
        {
            // Index
            if (approval <= -1) { text = negText; }
            if (approval == 0) { text = neutext; }
            if (approval >= 1) { text = posText; }
        }
        #endregion
        #region Update
        // Update is called once per frame
        void Update()
        {

        }
        #endregion
        #region OnGUI
        void OnGUI()
        {
            #region ShowDLG
            if (showDlg)
            {
                #region Screen Res
                float scrW = Screen.width / 16;
                float scrH = Screen.height / 9;
                #endregion
                #region Elements
                GUI.Box(new Rect(0, 6 * scrH, Screen.width, 3 * scrH), npcName + ":" + text[index], dlgBx);
                if (!(index + 1 >= text.Length || index == optionsIndex))
                {
                    if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "", nextBut))
                    {
                        index++;
                    }
                }
                else if (index == optionsIndex)
                {
                    if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), response1, opBut1))
                    {
                        approval++;
                        Convo();
                        index++;
                    }
                    if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), response2, opbut2))
                    {
                        approval--;
                        Convo();
                        index++;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "", exitBut))
                    {
                        showDlg = false;
                        index = 0;

                        player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;
                        player.GetComponent<FinalCharacter>().enabled = true;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;


                    }
                }
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
