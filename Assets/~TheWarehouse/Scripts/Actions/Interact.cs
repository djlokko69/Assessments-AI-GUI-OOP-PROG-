using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [AddComponentMenu("Character Set UP/Interact")]
    public class Interact : MonoBehaviour
    {
        #region Variables
        public string labName;// label Name

        [Header("Player and Camera")]
        public GameObject player;
        public GameObject mainCam;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {
            #region Cursor
            // Lock And not Visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            #endregion
            player = GameObject.FindGameObjectWithTag("Player");


        }
        #endregion
        #region Update
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))// Key Command to active
            {
                Ray interact;
                interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hitInfo;
                if (Physics.Raycast(interact, out hitInfo, 10))
                {
                    #region NPC
                    if (hitInfo.collider.CompareTag("NPC"))
                    {
                        Debug.Log("Hit the NPC");
                        Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>();
                        //Patrol move = hitInfo.transform.GetComponent<Patrol>();
                        //TotemDialogue tDlg = hitInfo.transform.GetComponent<TotemDialogue>();
                        #region DLG
                        if (dlg != null)
                        {
                            dlg.showDlg = true;
                            //tDlg.showDlg = false;
                            player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                            player.GetComponent<FinalCharacter>().enabled = false;

                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                        #endregion
                    }
                }

            }
            #region InteractName
            Ray interactName;
            interactName = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitIn;
            if (Physics.Raycast(interactName, out hitIn, 10))
            {
                #region NPCtag
                if (hitIn.collider.CompareTag("NPC"))
                {
                    Debug.Log("Hit the NPC NAME");
                    labName = hitIn.transform.GetComponent<Dialogue>().npcName;
                }
                else
                {
                    labName = "";
                }
                #endregion
            }
            #endregion
            #endregion
        }
        #endregion
        #region OnGUI
        void OnGUI()
        {
            if (labName != "")
            {
                GUI.Box(new Rect(100, 100, 150, 50), labName);
            }
        }
        #endregion
    }
}
