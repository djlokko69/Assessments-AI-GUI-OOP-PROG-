using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player.Camera.FirstPerson.MouseLook))]
[AddComponentMenu("RPG/Character Handler")]
public class Handler : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InteractHandler();
    }
    void InteractHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitinfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitinfo, 10))
            {
                #region NPC tag
                //and that hits info is tagged NPC
                if (hitinfo.collider.CompareTag("NPC"))
                {
                    //Debug that we hit a NPC
                    Debug.Log("Hit the NPC");
                    #endregion

                }
            }
        }
    }
    
}
