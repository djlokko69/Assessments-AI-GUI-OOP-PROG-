using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [RequireComponent(typeof(Dialogue))]
    [RequireComponent(typeof(Patrol))]
    public class Halt : MonoBehaviour
    {
        #region Variables
        public Dialogue dialogue;
        public Patrol patrol;
        public WaitUntil wait;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {


            dialogue = GetComponent<Dialogue>();
            patrol = GetComponent<Patrol>();
        }
        #endregion
        #region Update
        // Update is called once per frame
        void Update()
        {
            RunPatrol();

        }
        #endregion
        #region RunPatrol
        void RunPatrol()
        {

            if (dialogue != null)
            {
                Debug.Log("NOT RUNNING");
                //dialogue.showDlg = true;

                patrol.enabled = false;

            }
            else
            {



            }
        }
        #endregion
    }
}
