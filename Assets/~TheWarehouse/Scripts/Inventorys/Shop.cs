using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [AddComponentMenu("Shops/Shop")]
    public class Shop : MonoBehaviour
    {
        #region Variables
        [Header("Shop Inventory")]
        public List<Item> inv = new List<Item>();
        public bool showShop;
        public Item selectItem;

        public Vector2 scrollPos = Vector2.zero;

        public float sW = Screen.width / 16;
        public float sH = Screen.height / 9;

        public int reale;

        public string sortType;
        //public Handler charH;
        //public CharacterController charCo;
        public GameObject player;
        public GameObject mainCam;

        public Inventory invent;
        #endregion
        #region Start
        // Use this for initialization
        void Start()
        {
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            //charH = GetComponent<Handler>();

            //charCo = this.GetComponent<CharacterController>();
            player = GameObject.FindGameObjectWithTag("Player");
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        #endregion
        #region Update
        // Update is called once per frame
        void Update()
        {

        }
        #endregion
        #region Toggle
        public bool TogShop()
        {
            if (showShop)
            {
                showShop = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;
                player.GetComponent<FinalCharacter>().enabled = true;
                mainCam.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = true;

                return false;
            }
            else
            {
                showShop = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                player.GetComponent<FinalCharacter>().enabled = false;
                mainCam.GetComponent<Player.Camera.FirstPerson.MouseLook>().enabled = false;
                return true;
            }
        }
        #endregion
        #region GUI
        void OnGUI()
        {
            sW = Screen.width / 16;
            sH = Screen.height / 9;
            if(showShop)
            {
                GUI.Box(new Rect(6 * sW, 2 * sH, 6 * sW, 6 * sH), "Shop");

                if (GUI.Button(new Rect(8f * sW, 2.3f * sH, 1 * sW, 0.25f * sH), "All")) { sortType = "All"; }
                if (GUI.Button(new Rect(9f * sW, 2.3f * sH, 1 * sW, 0.25f * sH), "Food")) { sortType = "Food"; }
                if (GUI.Button(new Rect(14 * sW, 6 * sH, sW, 0.25f * sH), "Exit Shop"))
                {
                    showShop = false;
                    invent.showInv = false;
                }

                DisplayShop(sortType);

                if (selectItem != null)
                {
                    GUI.DrawTexture(new Rect(13.5f * sW, 0.15f * sH, 1 * sW, 1 * sH), selectItem.Icon);
                    GUI.Box(new Rect(13.5f * sW, 1.17f * sH, 1 * sW, 1 * sH), selectItem.Amount.ToString());
                    GUI.Box(new Rect(13.5f * sW, 2.17f * sH, 2 * sW, 2 * sH), selectItem.Description.ToString());

                    if (GUI.Button(new Rect(13 * sW, 6f * sH, sW, 0.25f * sH), "Buy"))
                    {
                        Debug.Log("Buy Item");
                        if (invent.reale >= selectItem.Value)
                        {
                            invent.reale -= selectItem.Value;
                            invent.inv.Add(selectItem);
                            selectItem = null;
                            return;
                        }
                    }
                }
            }
            
        }
        #endregion
        #region Display
        void DisplayShop(string sortType)
        {
            if (!(sortType == "All" || sortType == ""))
            {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
                int h = 0;
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        if (h <= 35)
                        {
                            // 35 of less show buttons
                            if (GUI.Button(new Rect(6.1f * sW, 2.1f * sH + h * (0.25f * sH), 1.5f * sW, 0.25f * sH), inv[i].Name))
                            {
                                selectItem = inv[i];
                                Debug.Log(selectItem.Name);
                            }
                            h++;
                        }
                    }
                    else
                    {
                        // more than 35 show buttons in scollview
                        scrollPos = GUI.BeginScrollView(new Rect(6f * sW, 2.05f * sH, 2 * sW, 8.75f * sH), scrollPos, new Rect(0, 0, 0, 8.75f * sH + ((inv.Count - 35) * 0.25f * sH)), false, true);
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(6.1f * sW, 2.1f * sH + h * (0.25f * sH), 1.5f * sW, 0.25f * sH), inv[i].Name))
                            {
                                selectItem = inv[i];
                                Debug.Log(selectItem.Name);
                            }
                            h++;
                        }
                        GUI.EndScrollView();
                    }

                }
            }
            else
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv.Count <= 35)
                    {
                        // 35 or less show buttons
                        if (GUI.Button(new Rect(6.1f * sW, 2.1f * sH + i * (0.25f * sH), 1.5f * sW, 0.25f * sH), inv[i].Name))
                        {
                            selectItem = inv[i];
                            Debug.Log(selectItem.Name);
                        }
                    }
                    else
                    {
                        // more than 35 show buttons in scroll view
                        scrollPos = GUI.BeginScrollView(new Rect(6.5f * sW, 0.25f * sH, 5 * sW, 8.75f * sH), scrollPos, new Rect(0, 0, 0, 8.75f * sH + ((inv.Count - 35) * 0.25f * sH)), false, true);
                        if (GUI.Button(new Rect(6.5f * sW, 2.1f * sH + i * (0.25f * sH), 1.5f * sW, 0.25f * sH), inv[i].Name))
                        {
                            selectItem = inv[i];
                            Debug.Log(selectItem.Name);
                        }
                        GUI.EndScrollView();
                    }
                }
            }
        }
        #endregion
    }
}
