using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [AddComponentMenu("NPC/Dialogue")]
    public class TotemDialogue : MonoBehaviour
    {
        #region Variables
        [Header("Ref")]
        public bool showDlg;
        public int index, optionIndex;
        [Space(3)]
        [Header("Player")]
        public GameObject player;
        public Player.Camera.FirstPerson.MouseLook mainCam;
        [Space(3)]
        [Header("NPC/Dialogue")]
        public string npcName;

        private string[] text;

        public string[] negTex, neuTex, posTex;
        public int approval;
        public string response1, response2;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player.Camera.FirstPerson.MouseLook>();
            text = new string[5];
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = neuTex[i];
            }
        }
        #endregion
        #region Convo
        void Convo()
        {
            if (approval <= -1) { text = negTex; }
            if (approval == 0) { text = neuTex; }
            if (approval >= 1) { text = posTex; }
        }
        #endregion
        #region ONGUI
        void OnGUI()
        {
            if (showDlg)
            {
                float sW = Screen.width / 16;
                float sH = Screen.height / 9;

                GUI.Box(new Rect(0, 6 * sH, Screen.width, 3 * sH), npcName + ": " + text[index]);

                if (!(index + 1 >= text.Length || index == optionIndex))
                {
                    if (GUI.Button(new Rect(15 * sW, 8.5f * sH, sW, 0.5f * sH), "Next")) { index++; }
                }
                else if (index == optionIndex)
                {
                    if (GUI.Button(new Rect(15 * sW, 8.5f * sH, sW, 0.5f * sH), response1))
                    {
                        approval++;
                        Convo();
                        index++;
                    }
                    if (GUI.Button(new Rect(14 * sW, 8.5f * sH, sW, 0.5f * sH), response2))
                    {
                        approval--;
                        Convo();
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(15 * sW, 8.5f * sH, sW, 0.5f * sH), "Bye."))
                    {
                        showDlg = false;
                        index = 0;

                        player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;
                        player.GetComponent<FinalCharacter>().enabled = true;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
        }
        #endregion
    }
}

