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
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");


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
                            mainCam.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                        #endregion
                    }
                    #region Item
                    if (hitInfo.collider.CompareTag("Item"))
                    {
                        //Debug
                        Debug.Log("Hit Item");
                        ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                        if (handler != null)
                        {
                            handler.OnCollection();
                        }
                    }
                    #endregion
                    #region Shop
                    if (hitInfo.collider.CompareTag("Shops"))
                    {
                        Debug.Log("Hit Shops");
                        if (hitInfo.transform.GetComponent<Shop>() != null)
                        {
                            Shop shop = hitInfo.transform.GetComponent<Shop>();
                            player.GetComponent<Inventory>().shop = shop;

                            shop.showShop = true;
                            player.GetComponent<Inventory>().showInv = true;
                            player.GetComponent<Inventory>().inShop = true;
                            
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            Debug.Log("Open Shops");
                        }
                    }
                    #endregion
                    #region Chest
                    if (hitInfo.collider.CompareTag("Chest"))
                    {
                        Debug.Log("Hit Chest");
                        if (hitInfo.transform.GetComponent<Chest>() != null)
                        {
                            Chest chest = hitInfo.transform.GetComponent<Chest>();
                            player.GetComponent<Inventory>().chest = chest;
                            chest.showChest = true;
                            player.GetComponent<Inventory>().showInv = true;
                            player.GetComponent<Inventory>().inChest = true;
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            Debug.Log("Open Chest");
                        }
                    }
                    #endregion
                    #region Door
                    if (hitInfo.collider.CompareTag("Door"))
                    {
                        Debug.Log("Hit Door");
                        if (hitInfo.transform.GetComponent<Doors>() != null)
                        {
                            Doors door = hitInfo.transform.GetComponent<Doors>();
                            
                            Debug.Log("Used Door");
                        }
                    }
                    #endregion
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
