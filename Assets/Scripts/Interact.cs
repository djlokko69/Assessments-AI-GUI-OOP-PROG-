using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character Set UP/Interact")]
public class Interact : MonoBehaviour
{
    public string labName;

    [Header("Player and Camera")]
    public GameObject player;
    public GameObject mainCam;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if(Physics.Raycast(interact,out hitInfo, 10))
            {

                if (hitInfo.collider.CompareTag("NPC"))
                {
                    Debug.Log("Hit the NPC");
                    Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>();
                    ;
                    if (dlg != null)
                    {
                        dlg.showDlg = true;
                        player.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                        player.GetComponent<PlayerCon>().enabled = false;
                        mainCam.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                
               }
            }
        }
        Ray interactName;
        interactName = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitIn;
        if(Physics.Raycast(interactName, out hitIn, 10))
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
    }
    void OnGUI()
    {
        if (labName != "")
        {
            GUI.Box(new Rect(100, 100, 150, 50), labName);
        }
    }

}
